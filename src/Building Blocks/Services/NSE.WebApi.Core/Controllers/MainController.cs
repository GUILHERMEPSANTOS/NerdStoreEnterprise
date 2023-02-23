using Core.Communication;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace NSE.WebApi.Core.Controllers
{
    [ApiController]
    public abstract class MainController : ControllerBase
    {
        protected ICollection<string> Errors = new List<string>();

        protected IActionResult CustomResponse(object result = null)
        {
            if (ValidOperation())
            {
                return Ok(result);
            }

            return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
            {
                {"Message", Errors.ToArray()}
            }));
        }

        protected IActionResult CustomResponse(ModelStateDictionary modelState)
        {
            var errors = modelState.Values.SelectMany(e => e.Errors);

            foreach (var error in errors)
            {
                AddErrorsProcessing(error.ErrorMessage);
            }

            return CustomResponse();
        }
        protected IActionResult CustomResponse(ValidationResult validationResult)
        {

            foreach (var error in validationResult.Errors)
            {
                AddErrorsProcessing(error.ErrorMessage);
            }

            return CustomResponse();
        }

        protected IActionResult CustomResponse(ResponseResult response)
        {
            ResponseHasErrors(response);

            return CustomResponse();
        }
 
        protected bool ResponseHasErrors(ResponseResult response)
        {
            if (ProcessSuccess(response)) return false;

            foreach (var error in response.Errors.Message)
            {
                AddErrorsProcessing(error);
            }

            return true;
        }

        private bool ProcessSuccess(ResponseResult response)
        {
            var responseIsNull = response is null;
            var hasMessageErrors = response.Errors.Message.Any();

            return responseIsNull || !hasMessageErrors;
        }

        protected bool ValidOperation()
        {
            return !Errors.Any();
        }

        protected void AddErrorsProcessing(string error)
        {
            Errors.Add(error);
        }

        protected void ClearErrors()
        {
            Errors.Clear();
        }
    }
}
