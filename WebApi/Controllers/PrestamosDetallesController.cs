using Application.PrestamosDetalles.GuardarAbono;
using Application.PrestamosDetalles.GuardarCobro;
using Application.PrestamosDetalles.GuardarPrestamoDetalle;
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
    }
}
