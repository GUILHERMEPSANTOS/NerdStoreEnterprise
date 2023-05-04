using Core.Messages;
using FluentValidation.Results;
using MediatR;
using NSE.Pedido.API.Application.DTO;
using NSE.Pedido.Domain.Orders;
using NSE.Pedido.Domain.Vouchers;
using NSE.Pedido.Domain.Vouchers.Specs;
using NSE.Pedido.API.Application.Events;
using NSE.Pedido.Domain.Orders.Interfaces;
using NSE.WebApi.Core.User;

namespace NSE.Pedido.API.Application.Commands
{
    public class OrderCommandHandler : CommandHandler, IRequestHandler<AddOrderCommand, ValidationResult>
    {
        private readonly IVoucherRepository _voucherRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IAspNetUser _user;

        public OrderCommandHandler(IVoucherRepository voucherRepository, IOrderRepository orderRepository, IAspNetUser user)
        {
            _voucherRepository = voucherRepository;
            _orderRepository = orderRepository;
            _user = user;
        }

        public async Task<ValidationResult> Handle(AddOrderCommand message, CancellationToken cancellationToken)
        {
            var validMessage = message.IsValid();

            if (!validMessage) return message.ValidationResult;

            var order = MapOrder(message);

            var voucherAppliedSuccessfully = await ApplyVoucher(message, order);

            if (!voucherAppliedSuccessfully) return ValidationResult;

            var validOrder = ValidateOrder(order);

            if (validOrder) return ValidationResult;

            var paymentExecutedSuccessfully = DoPayment(order, message);

            if (!paymentExecutedSuccessfully) return ValidationResult;

            order.Authorize();

            order.AddEvent(new OrderDoneEvent(order.Id, order.CustomerId));

            _orderRepository.Add(order);

            return await PersistData(_orderRepository.UnitOfWork);
        }

        private Order MapOrder(AddOrderCommand message)
        {
            var customerId = _user.GetUserId();

            var address = new Address
            {
                City = message.Address.City,
                BuildingNumber = message.Address.BuildingNumber,
                Neighborhood = message.Address.Neighborhood,
                SecondaryAddress = message.Address.SecondaryAddress,
                State = message.Address.State,
                StreetAddress = message.Address.StreetAddress,
                ZipCode = message.Address.ZipCode
            };

            var orderItem = message.OrderItems.Select(OrderItemDTO.ToOrderItem).ToList();
            var order = new Order(customerId, (decimal)message.Amount, orderItem);

            order.SetAddress(address);

            return order;
        }

        private async Task<bool> ApplyVoucher(AddOrderCommand message, Order order)
        {
            if (!(bool)message.HasVoucher) return true;

            var voucher = await _voucherRepository.GetVoucherByCode(message.VoucherCode);

            if (voucher is null)
            {
                AddError("O voucher informado não existe");
                return false;
            }

            var voucherValidation = ValidateVoucher(voucher);

            if (!voucherValidation.IsValid)
            {
                AddVoucherErrors(voucherValidation.Errors);
                return false;
            }

            order.SetVoucher(voucher);
            DebitVoucherAmount(voucher);
            UpdateVoucherRepository(voucher);

            return true;
        }

        private ValidationResult ValidateVoucher(Voucher voucher)
        {
            return new VoucherValidation().Validate(voucher);
        }

        private void AddVoucherErrors(List<ValidationFailure> errors) => errors.ForEach(error => AddError(error.ErrorCode));

        private void DebitVoucherAmount(Voucher voucher)
        {
            voucher.DebitAmount();
        }

        private void UpdateVoucherRepository(Voucher voucher)
        {
            _voucherRepository.Update(voucher);
        }

        private bool ValidateOrder(Order order)
        {
            var orderAmount = order.Amount;
            var orderDiscount = order.Discount;

            order.CalculateOrderAmount();

            if (order.Amount != orderAmount)
            {
                AddError("O valor total do pedido não confere com o cálculo do pedido");
                return false;
            }

            if (order.Discount != orderDiscount)
            {
                AddError("O valor total do desconto não confere com o cálculo do pedido");
                return false;
            }

            return true;
        }
        private bool DoPayment(Order order, AddOrderCommand message)
        {
            return true;
        }
    }
}