using NSE.Pedido.Domain.Orders;

namespace NSE.Pedido.API.Application.DTO
{
    public class AddressDTO
    {
        public string StreetAddress { get; set; }
        public string BuildingNumber { get; set; }
        public string SecondaryAddress { get; set; }
        public string Neighborhood { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string State { get; set; }

        public static AddressDTO ToAddressDTO(Order order)
        {
            return new AddressDTO
            {
                StreetAddress = order.Address.StreetAddress,
                BuildingNumber = order.Address.BuildingNumber,
                SecondaryAddress = order.Address.SecondaryAddress,
                Neighborhood = order.Address.Neighborhood,
                ZipCode = order.Address.ZipCode,
                City = order.Address.City,
                State = order.Address.State,
            };
        }
    }
}