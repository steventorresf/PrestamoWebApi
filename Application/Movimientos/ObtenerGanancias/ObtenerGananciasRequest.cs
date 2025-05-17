using MediatR;

namespace Application.Movimientos.ObtenerGanancias;

public class ObtenerGananciasRequest : IRequest<List<ObtenerGananciasResponse>>
{
    public long UsuarioId {  get; set; }
    public string FechaInicio {  get; set; } = string.Empty;
    public string FechaFin {  get; set; } = string.Empty;
}
