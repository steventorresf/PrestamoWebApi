using Domain.Exceptions;
using Newtonsoft.Json;
using Persistence.Files;

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
                    Title = "Acceso denegado",
                    Message = ex.Message
                });
            }
            catch(BadRequestException ex)
            {
                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                await httpContext.Response.WriteAsJsonAsync(new
                {
                    Title = "Parametros incorrectos",
                    Message = "Revise los datos de entrada y vuelva a ejecutar el servicio.",
                    Errors = JsonConvert.DeserializeObject<List<string>>(ex.Message)
                });
            }
            catch (Exception exception)
            {
                await fileTxtService.FileLogError(exception);
                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await httpContext.Response.WriteAsJsonAsync(new
                {
                    Title = "Error",
                    Message = "Error interno del servidor, por favor contacte a su administrador"
                });
            }
        }
    }
}
