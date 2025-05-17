using MediatR;

namespace Application.DiasNoHabiles.ObtenerDiasNoHabilesPorUsuario;

public class ObtenerDiasNoHabilesPorUsuarioRequest : IRequest<List<ObtenerDiasNoHabilesPorUsuarioResponse>>
{
    public long UsuarioId {  get; set; }
    public int Anio {  get; set; }
}
