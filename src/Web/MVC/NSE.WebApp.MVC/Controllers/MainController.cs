using Microsoft.AspNetCore.Mvc;
using NSE.WebApp.MVC.Models.Errors;

namespace NSE.WebApp.MVC.Controllers
{
    public class MainController : Controller
    {
        public bool HasErrors(ResponseResult response)
        {
            if (response is not null && response.Errors.Message.Any())
            {
                foreach (var message in response.Errors.Message)
                {
                    ModelState.AddModelError(string.Empty, message);
                }
               
                return true;
            }
            return false;
        }
    }
}