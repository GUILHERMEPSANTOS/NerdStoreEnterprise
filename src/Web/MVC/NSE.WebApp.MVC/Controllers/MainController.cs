using Core.Communication;
using Microsoft.AspNetCore.Mvc;

namespace NSE.WebApp.MVC.Controllers
{
    public abstract class MainController : Controller
    {
        protected bool HasErrors(ResponseResult response)
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
        protected void AddValidationError(string message)
        {
            ModelState.AddModelError(string.Empty, message);
        }

        protected bool IsValid()
        {
            return ModelState.Count == 0;
        }
    }
}

