using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using NSE.WebApp.MVC.Models;

namespace NSE.WebApp.MVC.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [Route("error/{id:length(3,3)}")]
    public IActionResult Error(int id)
    {
        var modelError = GenerateInstanceErrorViewModel(id);

        if (id == 500)
        {
            HandleErrorViewModelToInternalServerError(modelError);
        }
        else if (id == 404)
        {
            HandleErrorViewModelToNotFoundError(modelError);
        }
        else if (id == 403)
        {
            HandleErrorViewModelToForbiddenError(modelError);
        }
        else
        {
            return StatusCode(404);
        }

        return View("Error", modelError);
    }
    private ErrorViewModel GenerateInstanceErrorViewModel(int id)
    {
        var modelError = new ErrorViewModel();

        modelError.ErrorCode = id;

        return modelError;
    }

    private void HandleErrorViewModelToInternalServerError(ErrorViewModel modelError)
    {
        modelError.Message = "Ocorreu um erro! Tente novamente mais tarde ou contate nosso suporte.";
        modelError.Title = "Ocorreu um erro!";
    }

    private void HandleErrorViewModelToNotFoundError(ErrorViewModel modelError)
    {
        modelError.Message = "A página que está procurando não existe! <br /> Em caso de dúvidas entre em contato com nosso suporte.";
        modelError.Title = "Ops! Página não encontrada.";
    }
    private void HandleErrorViewModelToForbiddenError(ErrorViewModel modelError)
    {
        modelError.Message = "Você não tem permissão para fazer isto.";
        modelError.Title = "Acesso Negado!.";
    }

}
