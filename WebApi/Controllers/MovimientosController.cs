using Application.Movimientos.ObtenerMovimientosPorPrestamo;
using Domain.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/movimientos")]
    [ApiController]
    public class MovimientosController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MovimientosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("obtener-movimientos-por-prestamo/{prestamoId}")]
        public async Task<ActionResult<ResponseData<List<ObtenerMovimientosPorPrestamoResponse>>>> GuardarCliente(long prestamoId)
        {
            ObtenerMovimientosPorPrestamoRequest request = new()
            {
                UsuarioId = Convert.ToInt32(HttpContext.Request.Headers["uid"]),
                PrestamoId = prestamoId
            };

            ResponseData<List<ObtenerMovimientosPorPrestamoResponse>> Response = new()
            {
                Data = await _mediator.Send(request),
                Message = "Registros OK."
            };
            return Response;
        }
    }
}
