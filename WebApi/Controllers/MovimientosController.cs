using Application.Movimientos.EliminarMovimiento;
using Application.Movimientos.GuardarMovimiento;
using Application.Movimientos.ObtenerGanancias;
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
        public async Task<ActionResult<ResponseData<List<ObtenerMovimientosPorPrestamoResponse>>>> ObtenerMovimientosporPrestamo(long prestamoId)
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

        [HttpGet("obtener-ganancias-movimientos")]
        public async Task<ActionResult<ResponseData<List<ObtenerGananciasResponse>>>> ObtenerGananciasMovimientos(string fechaInicio, string fechaFin)
        {
            ObtenerGananciasRequest request = new()
            {
                UsuarioId = Convert.ToInt32(HttpContext.Request.Headers["uid"]),
                FechaInicio = fechaInicio,
                FechaFin = fechaFin
            };

            ResponseData<List<ObtenerGananciasResponse>> Response = new()
            {
                Data = await _mediator.Send(request),
                Message = "Registros OK."
            };
            return Response;
        }

        [HttpPost("guardar-movimiento")]
        public async Task<ActionResult<ResponseData<bool>>> GuardarMovimiento([FromBody] GuardarMovimientoDTO requestDto)
        {
            GuardarMovimientoRequest request = new()
            {
                UsuarioId = Convert.ToInt32(HttpContext.Request.Headers["uid"]),
                ClienteId = requestDto.ClienteId,
                PrestamoId = requestDto.PrestamoId,
                FechaPago = requestDto.FechaPago,
                Capital = requestDto.Capital,
                Intereses = requestDto.Intereses
            };

            ResponseData<bool> Response = new()
            {
                Data = await _mediator.Send(request),
                Message = "El movimiento ha sido guardado exitosamente."
            };
            return Response;
        }

        [HttpDelete("eliminar-movimiento/{movimientoId}")]
        public async Task<ActionResult<ResponseData<bool>>> EliminarMovimiento(long movimientoId)
        {
            EliminarMovimientoRequest request = new()
            {
                UsuarioId = Convert.ToInt32(HttpContext.Request.Headers["uid"]),
                MovimientoId = movimientoId
            };

            ResponseData<bool> Response = new()
            {
                Data = await _mediator.Send(request),
                Message = "El movimiento ha sido eliminado exitosamente."
            };
            return Response;
        }
    }
}
