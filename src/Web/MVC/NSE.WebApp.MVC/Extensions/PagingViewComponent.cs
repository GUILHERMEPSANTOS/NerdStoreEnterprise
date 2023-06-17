using Microsoft.AspNetCore.Mvc;
using NSE.WebApp.MVC.Models.Paginacao;

namespace NSE.WebApp.MVC.Extensions
{
    public class PagingViewComponent : ViewComponent
    {

        public IViewComponentResult Invoke(IPagedList pagingModel)
        {
            return View(pagingModel);
        }
    }
}