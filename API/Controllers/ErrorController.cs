using API.Models;
using BLL.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers;

[ApiController]
[ApiExplorerSettings(IgnoreApi = true)]
public class ErrorController : ControllerBase
{
    [Route("/error")]
    public ActionResult Error()
    {
        var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
        var exception = context?.Error;

        return exception switch
        {
            ViolationException => BadRequest(new ErrorModel(exception.Message, HttpStatusCode.BadRequest)),
            NotFoundException => BadRequest(new ErrorModel(exception.Message, HttpStatusCode.NotFound)),
            _ => new JsonResult(new ErrorModel(exception.Message, HttpStatusCode.InternalServerError))
        };
    }
}