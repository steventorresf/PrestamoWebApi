using Application.Clientes.GuardarCliente;
using Application.Clientes.ModificarEstadoCliente;
using Application.Clientes.ObtenerClientes;
using Domain.Exceptions;
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

        [HttpGet("obtener-clientes-por-usuario")]
        public async Task<ActionResult<ResponseData<List<ObtenerClientesResponse>>>> ObtenerTodos(string estado, string? textoFiltro)
        {
            if (string.IsNullOrEmpty(estado))
                throw new BadRequestException("El parametro 'estado' es obligatorio.");

            ObtenerClientesRequest request = new()
            {
                UsuarioId = Convert.ToInt32(HttpContext.Request.Headers["uid"]),
                CodigoEstado = estado,
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
        public async Task<ActionResult<ResponseData<GuardarClienteResponse>>> GuardarCliente([FromBody] GuardarClienteDTO requestDto)
        {
            GuardarClienteRequest request = new()
            {
                UsuarioId = Convert.ToInt32(HttpContext.Request.Headers["uid"]),
                ClienteId = requestDto.ClienteId,
                TipoId = requestDto.TipoId,
                Direccion = requestDto.Direccion,
                GeneroId = requestDto.GeneroId,
                Identificacion = requestDto.Identificacion,
                NombreCompleto = requestDto.NombreCompleto,
                TelCel = requestDto.TelCel
            };

            ResponseData<GuardarClienteResponse> Response = new()
            {
                Data = await _mediator.Send(request),
                Message = "El cliente ha sido guardado exitosamente."
            };
            return Response;
        }

        [HttpPost("modificar-estado-cliente")]
        public async Task<ActionResult<ResponseData<bool>>> ModificarEstadoCliente([FromBody] ModificarEstadoClienteDTO requestDto)
        {
            ModificarEstadoClienteRequest request = new()
            {
                UsuarioId = Convert.ToInt32(HttpContext.Request.Headers["uid"]),
                ClienteId = requestDto.ClienteId,
                CodigoEstado = requestDto.CodigoEstado
            };

            ModificarEstadoClienteResponse Resultado = await _mediator.Send(request);
            ResponseData<bool> Response = new()
            {
                Success = Resultado.Result,
                Data = Resultado.Result,
                Message = Resultado.Message
            };
            return Response;
        }
    }
}
