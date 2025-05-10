using Application.Clientes.GuardarCliente;
using Application.Clientes.ObtenerClientes;
using Domain.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Filters;

namespace WebApi.Controllers
{
    [Route("api/cliente")]
    [ApiController]
    //[ServiceFilter(typeof(UserValidationFilter))]
    public class ClienteController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ClienteController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("obtener-por-usuario-id")]
        public async Task<ActionResult<ResponseData<List<ObtenerClientesResponse>>>> ObtenerTodos(string? textoFiltro)
        {
            ObtenerClientesRequest request = new()
            {
                UsuarioId = Convert.ToInt32(HttpContext.Request.Headers["uid"]),
                TextoFiltro = textoFiltro,
            };

            ResponseData<List<ObtenerClientesResponse>> Response = new()
            {
                Data = await _mediator.Send(request),
                Message = "Registros Ok."
            };
            return Response;
        }

        [HttpPost("guardar-cliente")]
        public async Task<ActionResult<ResponseData<GuardarClienteResponse>>> GuardarCliente([FromBody] GuardarClienteRequest request)
        {
            request.UsuarioId = Convert.ToInt32(HttpContext.Request.Headers["uid"]);
            ResponseData<GuardarClienteResponse> Response = new()
            {
                Data = await _mediator.Send(request),
                Message = "El cliente ha sido guardado exitosamente."
            };
            return Response;
        }
    }
}
