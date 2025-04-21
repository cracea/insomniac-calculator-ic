using System.Net;

namespace Insomniac.Calculator.API.Models.HttpResponse
{
    public record ExceptionResponse(HttpStatusCode StatusCode, string Description);
}
