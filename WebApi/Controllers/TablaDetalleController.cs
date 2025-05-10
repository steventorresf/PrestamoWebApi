using Application.TablaDetalles.ObtenerTablaDetallesPorCodigos;
using Domain.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/tabla-detalle")]
    [ApiController]
    public class TablaDetalleController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TablaDetalleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("obtener-por-codigos")]
        public async Task<ActionResult<ResponseData<List<ObtenerTablaDetallesPorCodigosResponse>>>> ObtenerTablaDetallesPorCodigos(string codigos)
        {
            ObtenerTablaDetallesPorCodigosRequest request = new() { Codigos = codigos };

            ResponseData<List<ObtenerTablaDetallesPorCodigosResponse>> Response = new()
            {
                Data = await _mediator.Send(request),
                Message = "Registros Ok."
            };
            return Response;
        }
    }
}
