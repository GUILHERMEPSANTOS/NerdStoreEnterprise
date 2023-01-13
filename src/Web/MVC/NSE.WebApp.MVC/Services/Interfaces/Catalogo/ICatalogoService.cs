using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NSE.WebApp.MVC.Models.Catalogo;

namespace NSE.WebApp.MVC.Interfaces.Catalogo
{
    public interface ICatalogoService
    {
        Task<IEnumerable<ProdutoViewModel>> GetAll();
        Task<ProdutoViewModel> GetById(Guid id);
    }
}