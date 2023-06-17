 using NSE.WebApp.MVC.Models.Catalogo;
using NSE.WebApp.MVC.Models.Paginacao;
using Refit;

namespace NSE.WebApp.MVC.Interfaces
{
    public interface ICatalogoService
    {
        Task<PagedResult<ProdutoViewModel>> GetProductsWithPagination(int pageIndex, int pageSize, string query);
        Task<ProdutoViewModel> GetById(Guid productId);
    }

    public interface ICatalogoServiceRefit
    {
        [Get("/catalogo/produtos")]
        Task<IEnumerable<ProdutoViewModel>> GetAll();

        [Get("/catalogo/produto/{id}")]
        Task<ProdutoViewModel> GetById(Guid id);
    }
}