using Application.Usuarios.ObtenerUsuarioPorLogin;
using Domain.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/usuarios")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsuarioController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<ActionResult<ResponseData<ObtenerUsuarioPorLoginResponse>>> ObtenerUsuarioPorLogin([FromBody] ObtenerUsuarioPorLoginRequest request)
        {
            ResponseData<ObtenerUsuarioPorLoginResponse> Response = new()
            {
                Data = await _mediator.Send(request),
                Message = "Usuario logueado"
            };
            return Response;
        }
    }
}
