using MediatR;

namespace Application.Prestamos.ObtenerPrestamosPorClienteId;

public class ObtenerPrestamosPorClienteIdRequest : IRequest<List<ObtenerPrestamosPorClienteIdResponse>>
{
    public long ClienteId { get; set; }
    public int UsuarioId { get; set; }
}
