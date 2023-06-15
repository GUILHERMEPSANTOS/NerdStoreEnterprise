using Core.Data;
using Dapper;
using Microsoft.EntityFrameworkCore;
using NSE.Catalogo.API.Domain.Enitites;
using NSE.Catalogo.API.Domain.Entities;
using NSE.Catalogo.API.Domain.Interfaces;

namespace NSE.Catalogo.API.Data.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly CatalogoContext _catalogoContext;

        public IUnitOfWork UnitOfWork => _catalogoContext;
        public ProductRepository(CatalogoContext catalogoContext)
        {
            _catalogoContext = catalogoContext;
        }

        public async Task<PagedResult<Product>> GetPagedProducts(int pagedSize, int pagedIndex, string query)
        {
            using var connection = _catalogoContext.Database.GetDbConnection();

            DynamicParameters parameters = new();

            parameters.Add("@Name", query);
            parameters.Add("@PagedSize", pagedSize);
            parameters.Add("@PagedIndex", pagedIndex);

            var pagedProducts = await connection.QueryAsync<Product>(
                sql: @"SELECT Id               = Id
                             ,Name             = Name
                             ,Description      = Description
                             ,Active           = Active
                             ,Price            = Price
                             ,RegistrationDate = RegistrationDate
                             ,Image            = Image
                             ,StockQuantity    = StockQuantity
                       FROM Produtos
                       WHERE (@Name IS NULL OR Name LIKE '%' + @Name + '%')
                       ORDER BY Name
                       OFFSET @PagedSize * (@PagedIndex - 1) ROWS
                       FETCH NEXT @PagedSize  ROWS ONLY;
                       ",
                param: parameters,
                commandType: System.Data.CommandType.Text
            );

            var quantityProductsWithQuery = await GetToTalProductsWith(query);

            return new PagedResult<Product>
            {
                List = pagedProducts,
                PageIndex = pagedIndex,
                PageSize = pagedSize,
                Query = query,
                TotalResults = quantityProductsWithQuery
            };

        }

        public async Task<int> GetToTalProductsWith(string query)
        {
            using var connection = _catalogoContext.Database.GetDbConnection();

            DynamicParameters parameters = new();

            parameters.Add("@Name", query);


            var quantityProductsWithQuery = await connection.QueryFirstOrDefaultAsync<int>(
                sql: @"SELECT Count(Id)
                       FROM Produtos
                       WHERE (@Name IS NULL OR Name LIKE '%' + @Name + '%')",
                param: parameters,
                commandType: System.Data.CommandType.Text
            );

            return quantityProductsWithQuery;
        }


        public async Task<Product> GetById(Guid id)
        {
            return await _catalogoContext.Produtos.FindAsync(id);
        }
        public void Add(Product produto)
        {
            _catalogoContext.Produtos.Add(produto);
        }

        public async void Update(Product produto)
        {
            _catalogoContext.Update(produto);
        }

        public async Task<IEnumerable<Product>> GetProducts(IEnumerable<Guid> ids)
        {
            return await _catalogoContext.Produtos.Where(product => ids.Contains(product.Id) && product.Active).ToListAsync();
        }

        public void Dispose()
        {
            _catalogoContext?.Dispose();
        }
    }
}