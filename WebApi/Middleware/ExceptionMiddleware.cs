using Domain.Exceptions;
using Newtonsoft.Json;
using Persistence.Files;
using System.Net;

namespace WebApi.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _requestDelegate;

        public ExceptionMiddleware(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }

        public async Task InvokeAsync(HttpContext httpContext, ILogErrorFile fileTxtService)
        {
            try
            {
                await _requestDelegate(httpContext);
            }
            catch(UnauthorizedException ex)
            {
                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await httpContext.Response.WriteAsJsonAsync(new
                {
                    Status = HttpStatusCode.Unauthorized.ToString(),
                    StatusCode = StatusCodes.Status401Unauthorized,
                    ex.Message
                });
            }
            catch(BadRequestException ex)
            {
                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;                
                if (ex.Message.Contains("[") && ex.Message.Contains("]"))
                {
                    await httpContext.Response.WriteAsJsonAsync(new
                    {
                        Status = HttpStatusCode.BadRequest,
                        StatusCode = HttpStatusCode.BadRequest,
                        Errors = JsonConvert.DeserializeObject<List<string>>(ex.Message)
                    });
                }
                else
                {
                    await httpContext.Response.WriteAsJsonAsync(new
                    {
                        Status = HttpStatusCode.BadRequest.ToString(),
                        StatusCode = StatusCodes.Status400BadRequest,
                        ex.Message
                    });
                }
            }
            catch (Exception exception)
            {
                await fileTxtService.FileLogError(exception);
                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await httpContext.Response.WriteAsJsonAsync(new
                {
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = "Error interno del servidor, por favor contacte a su administrador"
                });
            }
        }
    }
}
