using MediatR;

namespace Application.Prestamos.ObtenerPrestamoDetalle;

public class ObtenerPrestamoDetalleRequest : IRequest<List<ObtenerPrestamoDetalleResponse>>
{
    public long PrestamoId {  get; set; }
}
