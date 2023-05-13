using System.Net;

namespace API.Models;

public class ErrorModel
{
    public string ErrorMessage { get; }
    public HttpStatusCode StatusCode { get; }

    /// <summary>
    /// Constructor for creating error object with given parametrs
    /// </summary>
    /// <param name="errorMessage">The exception message</param>
    /// <param name="code">The exception status code</param>
    public ErrorModel(string errorMessage, HttpStatusCode code)
    {
        ErrorMessage = errorMessage;
        StatusCode = code;
    }
}