using Application.PrestamosDetalles.AnularCobro;
using Application.PrestamosDetalles.EliminarPrestamoDetalle;
using Application.PrestamosDetalles.GuardarAbono;
using Application.PrestamosDetalles.GuardarCobro;
using Application.PrestamosDetalles.GuardarPrestamoDetalle;
using Application.PrestamosDetalles.ObtenerGananciasEsperadas;
using Domain.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/prestamos-detalles")]
    [ApiController]
    public class PrestamosDetallesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PrestamosDetallesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("guardar-prestamo-detalle")]
        public async Task<ActionResult<ResponseData<bool>>> GuardarPrestamoDetalle([FromBody] GuardarPrestamoDetalleRequest request)
        {
            ResponseData<bool> Response = new()
            {
                Data = await _mediator.Send(request),
                Message = "El prestamo detalle ha sido guardado correctamente."
            };
            return Response;
        }

        [HttpPost("guardar-cobro")]
        public async Task<ActionResult<ResponseData<bool>>> GuardarCobro([FromBody] GuardarCobroDTO requestDto)
        {
            GuardarCobroRequest request = new()
            {
                UsuarioId = Convert.ToInt32(HttpContext.Request.Headers["uid"]),
                PrestamoDetalleId = requestDto.PrestamoDetalleId,
                FechaPago = requestDto.FechaPago
            };

            ResponseData<bool> Response = new()
            {
                Data = await _mediator.Send(request),
                Message = "El cobro ha sido guardado correctamente."
            };
            return Response;
        }

        [HttpPost("guardar-abono")]
        public async Task<ActionResult<ResponseData<bool>>> GuardarAbono([FromBody] GuardarAbonoDTO requestDto)
        {
            GuardarAbonoRequest request = new()
            {
                UsuarioId = Convert.ToInt32(HttpContext.Request.Headers["uid"]),
                PrestamoDetalleId = requestDto.PrestamoDetalleId,
                AbonoCapital = requestDto.AbonoCapital,
                AbonoIntereses = requestDto.AbonoIntereses,
                FechaPago = requestDto.FechaPago
            };

            ResponseData<bool> Response = new()
            {
                Data = await _mediator.Send(request),
                Message = "El abono ha sido guardado correctamente."
            };
            return Response;
        }

        [HttpPut("anular-cobro/{prestamoDetalleId}")]
        public async Task<ActionResult<ResponseData<bool>>> AnularCobro(long prestamoDetalleId)
        {
            AnularCobroRequest request = new()
            {
                UsuarioId = Convert.ToInt32(HttpContext.Request.Headers["uid"]),
                PrestamoDetalleId = prestamoDetalleId
            };

            ResponseData<bool> Response = new()
            {
                Data = await _mediator.Send(request),
                Message = "El abono ha sido anulado correctamente."
            };
            return Response;
        }

        [HttpDelete("eliminar-prestamo-detalle/{prestamoDetalleId}")]
        public async Task<ActionResult<ResponseData<bool>>> EliminarPrestamoDetalle(long prestamoDetalleId)
        {
            EliminarPrestamoDetalleRequest request = new()
            {
                UsuarioId = Convert.ToInt32(HttpContext.Request.Headers["uid"]),
                PrestamoDetalleId = prestamoDetalleId
            };

            ResponseData<bool> Response = new()
            {
                Data = await _mediator.Send(request),
                Message = "El registro ha sido eliminado permanentemente."
            };
            return Response;
        }

        [HttpGet("obtener-ganancias-esperadas")]
        public async Task<ActionResult<ResponseData<List<ObtenerGananciasEsperadasResponse>>>> ObtenerGananciasEsperadas(string fechaInicial, string fechaFinal)
        {
            ObtenerGananciasEsperadasRequest request = new()
            {
                UsuarioId = Convert.ToInt32(HttpContext.Request.Headers["uid"]),
                FechaInicial = fechaInicial,
                FechaFinal = fechaFinal
            };

            ResponseData<List<ObtenerGananciasEsperadasResponse>> Response = new()
            {
                Data = await _mediator.Send(request),
                Message = "Registros OK."
            };
            return Response;
        }
    }
}
