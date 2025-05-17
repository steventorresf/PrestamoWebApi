using MediatR;

namespace Application.Movimientos.EliminarMovimiento;

public class EliminarMovimientoRequest : IRequest<bool>
{
    public long UsuarioId {  get; set; }
    public long MovimientoId {  get; set; }
}