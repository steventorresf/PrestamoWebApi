using MediatR;

namespace Application.PrestamosDetalles.ObtenerGananciasEsperadas;

public class ObtenerGananciasEsperadasRequest : IRequest<List<ObtenerGananciasEsperadasResponse>>
{
    public long UsuarioId {  get; set; }
    public string FechaInicial { get; set; } = string.Empty;
    public string FechaFinal { get; set; } = string.Empty;
}