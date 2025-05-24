using MediatR;

namespace Application.PrestamosDetalles.EliminarPrestamoDetalle;

public class EliminarPrestamoDetalleRequest : IRequest<bool>
{
    public long UsuarioId { get; set; }
    public long PrestamoDetalleId { get; set; }
}
