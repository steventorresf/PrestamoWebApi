using MediatR;

namespace Application.Prestamos.ObtenerPrestamosPorClienteId;

public class ObtenerPrestamosPorClienteIdRequest : IRequest<List<ObtenerPrestamosPorClienteIdResponse>>
{
    public long ClienteId { get; set; }
}
