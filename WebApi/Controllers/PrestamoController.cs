using Application.Prestamos.ObtenerPrestamosPorClienteId;
using Domain.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrestamoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PrestamoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{clienteId}")]
        public async Task<ActionResult<ResponseData<List<ObtenerPrestamosPorClienteIdResponse>>>> ObtenerPrestamosPorClienteId(long clienteId)
        {
            ObtenerPrestamosPorClienteIdRequest request = new()
            {
                ClienteId = clienteId,
                UsuarioId = Convert.ToInt32(HttpContext.Request.Headers["uid"])
            };

            ResponseData<List<ObtenerPrestamosPorClienteIdResponse>> Response = new()
            {
                Data = await _mediator.Send(request),
                Message = "Registros Ok"
            };

            return Response;
        }
    }
}
