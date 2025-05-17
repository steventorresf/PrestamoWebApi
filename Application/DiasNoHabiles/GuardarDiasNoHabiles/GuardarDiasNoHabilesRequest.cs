using MediatR;

namespace Application.DiasNoHabiles.GuardarDiasNoHabiles;

public class GuardarDiasNoHabilesRequest : GuardarDiasNoHabilesDTO, IRequest<bool>
{
    public long UsuarioId { get; set; }
}

public class GuardarDiasNoHabilesDTO
{
    public int Anio { get; set; }
    public List<string> Fechas { get; set; } = new();
}