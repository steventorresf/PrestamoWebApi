using MediatR;

namespace Application.PrestamosDetalles.AnularCobro;

public class AnularCobroRequest : IRequest<bool>
{
    public long UsuarioId {  get; set; }
    public long PrestamoDetalleId {  get; set; }
}
