using Application.DiasNoHabiles.GuardarDiasNoHabiles;
using Application.DiasNoHabiles.ObtenerDiasNoHabilesPorUsuario;
using Domain.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/dias-no-habiles")]
    [ApiController]
    public class DiasNoHabilesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DiasNoHabilesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("obtener-por-usuario/{anio}")]
        public async Task<ActionResult<ResponseData<List<ObtenerDiasNoHabilesPorUsuarioResponse>>>> ObtenerPorUsuario(int anio)
        {
            ObtenerDiasNoHabilesPorUsuarioRequest request = new()
            {
                UsuarioId = Convert.ToInt32(HttpContext.Request.Headers["uid"]),
                Anio = anio
            };

            ResponseData<List<ObtenerDiasNoHabilesPorUsuarioResponse>> Response = new()
            {
                Data = await _mediator.Send(request),
                Message = "Registros OK."
            };
            return Response;
        }

        [HttpPost("guardar-dias-no-habiles")]
        public async Task<ActionResult<ResponseData<bool>>> GuardarDiasNoHabiles([FromBody] GuardarDiasNoHabilesDTO requestDto)
        {
            GuardarDiasNoHabilesRequest request = new()
            {
                UsuarioId = Convert.ToInt32(HttpContext.Request.Headers["uid"]),
                Anio = requestDto.Anio,
                Fechas = requestDto.Fechas
            };

            ResponseData<bool> Response = new()
            {
                Data = await _mediator.Send(request),
                Message = "Las fechas han sido guardadas exitosamente."
            };
            return Response;
        }
    }
}
