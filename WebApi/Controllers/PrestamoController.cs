using Application.Prestamos.CrearPrestamo;
using Application.Prestamos.FinalizarPrestamo;
using Application.Prestamos.ModificarEstadoPrestamo;
using Application.Prestamos.ObtenerBalanceGeneral;
using Application.Prestamos.ObtenerCalculoCuotas;
using Application.Prestamos.ObtenerPrestamoDetalle;
using Application.Prestamos.ObtenerPrestamosAnulados;
using Application.Prestamos.ObtenerPrestamosCongelados;
using Application.Prestamos.ObtenerPrestamosPendientes;
using Application.Prestamos.ObtenerPrestamosPorClienteId;
using Domain.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/prestamos")]
    [ApiController]
    public class PrestamoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PrestamoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("crear-prestamo")]
        public async Task<ActionResult<ResponseData<bool>>> CrearPrestamo([FromBody] CrearPrestamoRequest request)
        {
            ResponseData<bool> Response = new()
            {
                Data = await _mediator.Send(request),
                Message = "El prestamo ha sido creado correctamente."
            };
            return Response;
        }

        [HttpGet("obtener-prestamos-por-cliente-id/{clienteId}")]
        public async Task<ActionResult<ResponseData<List<ObtenerPrestamosPorClienteIdResponse>>>> ObtenerPrestamosPorClienteId(long clienteId)
        {
            ObtenerPrestamosPorClienteIdRequest request = new() { ClienteId = clienteId };

            ResponseData<List<ObtenerPrestamosPorClienteIdResponse>> Response = new()
            {
                Data = await _mediator.Send(request),
                Message = "Registros Ok"
            };
            return Response;
        }

        [HttpGet("obtener-prestamo-por-id/{prestamoId}")]
        public async Task<ActionResult<ResponseData<List<ObtenerPrestamoDetalleResponse>>>> ObtenerPrestamoDetalle(long prestamoId)
        {
            ObtenerPrestamoDetalleRequest request = new() { PrestamoId = prestamoId };

            ResponseData<List<ObtenerPrestamoDetalleResponse>> Response = new()
            {
                Data = await _mediator.Send(request),
                Message = "Registros Ok"
            };
            return Response;
        }

        [HttpGet("obtener-prestamos-pendiente-por-usuario")]
        public async Task<ActionResult<ResponseData<List<ObtenerPrestamosPendientesResponse>>>> ObtenerPrestamosPendientes()
        {
            ObtenerPrestamosPendientesRequest request = new()
            { UsuarioId = Convert.ToInt32(HttpContext.Request.Headers["uid"]) };

            ResponseData<List<ObtenerPrestamosPendientesResponse>> Response = new()
            {
                Data = await _mediator.Send(request),
                Message = "Registros Ok"
            };
            return Response;
        }

        [HttpGet("obtener-prestamos-congelados-por-usuario")]
        public async Task<ActionResult<ResponseData<List<ObtenerPrestamosCongeladosResponse>>>> ObtenerPrestamosCongelados()
        {
            ObtenerPrestamosCongeladosRequest request = new()
            { UsuarioId = Convert.ToInt32(HttpContext.Request.Headers["uid"]) };

            ResponseData<List<ObtenerPrestamosCongeladosResponse>> Response = new()
            {
                Data = await _mediator.Send(request),
                Message = "Registros Ok"
            };
            return Response;
        }

        [HttpGet("obtener-prestamos-anulados-por-usuario")]
        public async Task<ActionResult<ResponseData<List<ObtenerPrestamosAnuladosResponse>>>> ObtenerPrestamosAnulados()
        {
            ObtenerPrestamosAnuladosRequest request = new()
            { UsuarioId = Convert.ToInt32(HttpContext.Request.Headers["uid"]) };

            ResponseData<List<ObtenerPrestamosAnuladosResponse>> Response = new()
            {
                Data = await _mediator.Send(request),
                Message = "Registros Ok"
            };
            return Response;
        }

        [HttpGet("obtener-balance-general-por-usuario")]
        public async Task<ActionResult<ResponseData<ObtenerBalanceGeneralResponse>>> ObtenerBalanceGeneral(string fechaInicial, string fechaFinal)
        {
            ObtenerBalanceGeneralRequest request = new()
            {
                UsuarioId = Convert.ToInt32(HttpContext.Request.Headers["uid"]),
                FechaInicial = fechaInicial,
                FechaFinal = fechaFinal
            };

            ResponseData<ObtenerBalanceGeneralResponse> Response = new()
            {
                Data = await _mediator.Send(request),
                Message = "El balance se ha obtenido satisfactoriamente."
            };
            return Response;
        }

        [HttpPost("calcular-cuotas-prestamo")]
        public async Task<ActionResult<ResponseData<List<ObtenerCalculoCuotasResponse>>>> ObtenerCalculoCuotas(ObtenerCalculoCuotasRequest request)
        {
            ResponseData<List<ObtenerCalculoCuotasResponse>> Response = new()
            {
                Data = await _mediator.Send(request),
                Message = "El cálculo se ha realizado exitosamente."
            };
            return Response;
        }

        [HttpPost("modificar-estado-prestamo")]
        public async Task<ActionResult<ResponseData<bool>>> ModificarEstadoPrestamo([FromBody] ModificarEstadoPrestamoDTO requestDto)
        {
            ModificarEstadoPrestamoRequest request = new()
            {
                UsuarioId = Convert.ToInt32(HttpContext.Request.Headers["uid"]),
                PrestamoId = requestDto.PrestamoId,
                CodigoEstado = requestDto.CodigoEstado
            };

            ResponseData<bool> Response = new()
            {
                Data = await _mediator.Send(request),
                Message = "El estado del prestamo ha sido modificado exitosamente."
            };
            return Response;
        }

        [HttpPost("finalizar-prestamo")]
        public async Task<ActionResult<ResponseData<bool>>> FinalizarPrestamo(FinalizarPrestamoDTO requestDto)
        {
            FinalizarPrestamoRequest request = new()
            {
                UsuarioId = Convert.ToInt32(HttpContext.Request.Headers["uid"]),
                PrestamoId = requestDto.PrestamoId,
                Capital = requestDto.Capital,
                FechaPago = requestDto.FechaPago,
                Intereses = requestDto.Intereses
            };

            ResponseData<bool> Response = new()
            {
                Data = await _mediator.Send(request),
                Message = "El prestamo ha sido finalizado exitosamente."
            };
            return Response;
        }
    }
}
