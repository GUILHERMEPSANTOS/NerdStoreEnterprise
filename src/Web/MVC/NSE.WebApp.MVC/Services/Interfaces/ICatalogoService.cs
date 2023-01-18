 using NSE.WebApp.MVC.Models.Catalogo;
using Refit;

namespace NSE.WebApp.MVC.Interfaces
{
    public interface ICatalogoService
    {
        Task<IEnumerable<ProdutoViewModel>> GetAll();
        Task<ProdutoViewModel> GetById(Guid id);
    }

    public interface ICatalogoServiceRefit
    {
        [Get("/catalogo/produtos")]
        Task<IEnumerable<ProdutoViewModel>> GetAll();

        [Get("/catalogo/produto/{id}")]
        Task<ProdutoViewModel> GetById(Guid id);
    }
}