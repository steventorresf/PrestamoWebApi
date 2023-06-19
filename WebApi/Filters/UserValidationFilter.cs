using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;
using System.IdentityModel.Tokens.Jwt;

namespace WebApi.Filters
{
    public class UserValidationFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            // Do something
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            string token = context.HttpContext.Request.Headers["Authorization"];
            if (string.IsNullOrEmpty(token))
                throw new UnauthorizedException("El token es requerido.");

            var handler = new JwtSecurityTokenHandler();
            if (!handler.CanReadToken(token.ToString().Replace("Bearer ", "")))
                throw new UnauthorizedException("El formato del token es incorrecto.");

            var jwtSecurityToken = handler.ReadJwtToken(token.ToString().Replace("Bearer ", ""));
            if (jwtSecurityToken.Payload.ValidTo < DateTime.UtcNow)
                throw new UnauthorizedException("Su sesión ha caducado, por favor inicie sesión nuevamente.");

            string sUid = context.HttpContext.Request.Headers["uid"];
            int.TryParse(sUid, out int iUid);
            if (string.IsNullOrEmpty(sUid) || iUid <= 0)
                throw new UnauthorizedException("El usuario es invalido, por favor inicie sesión.");

            string tokenUid = jwtSecurityToken.Claims.FirstOrDefault(x => x.Type.Contains("/sid"))?.Value ?? "";
            if (!tokenUid.Equals(sUid))
                throw new UnauthorizedException("El token y/o usuario es corrupto.");
        }
    }
}
