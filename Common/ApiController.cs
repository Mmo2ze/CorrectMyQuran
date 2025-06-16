using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CorectMyQuran.Common;

[ApiController]
public class ApiController : ControllerBase
{
	protected IActionResult Problem(List<Error> errors)
	{
		if (errors.Count == 0)
		{
			return Problem();
		}
		if(errors.All(error=> error.Type == ErrorType.Validation))
		{
			return ValidationProblem(errors);
		}
		//not sure what this does
		HttpContext.Items["Errors"] = errors;
            
		var firstError = errors[0];
            
		return Problem(firstError);
	}

	private IActionResult Problem(Error firstError)
	{
		var statusCode = firstError.Type switch
		{
			ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
			ErrorType.Forbidden => StatusCodes.Status403Forbidden,
			ErrorType.NotFound => StatusCodes.Status404NotFound,
			ErrorType.Conflict => StatusCodes.Status409Conflict,
			ErrorType.Validation => StatusCodes.Status400BadRequest,
			_ => StatusCodes.Status500InternalServerError
		};
		return Problem(statusCode: statusCode, title: firstError.Code, detail: firstError.Description);
	}

	private IActionResult ValidationProblem(List<Error> errors)
	{
		var modelStateDictionary = new ModelStateDictionary();
		foreach (var error in errors)
		{
			modelStateDictionary.AddModelError(error.Code, error.Description);
		}

		return ValidationProblem(modelStateDictionary);
	}
}