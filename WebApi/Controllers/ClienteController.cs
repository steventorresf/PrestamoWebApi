using Application;
using Application.Clientes.ObtenerClientes;
using Domain.DTO;
using Domain.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApi.Filters;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[ServiceFilter(typeof(UserValidationFilter))]
    public class ClienteController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ClienteController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
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

        //[HttpPost]
        //public async Task<ActionResult<ResponseData<ClienteRequestDTO>>> PostCliente([FromBody] ClienteRequestDTO request)
        //{
        //    var response = await _clienteService.PostCliente(request);
        //    return new ResponseData<ClienteRequestDTO>(response, "El cliente ha sido creado exitosamente.");
        //}
    }
}
