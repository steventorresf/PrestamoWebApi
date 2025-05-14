using MediatR;

namespace Application.Movimientos.ObtenerMovimientosPorPrestamo
{
    public class ObtenerMovimientosPorPrestamoRequest : IRequest<List<ObtenerMovimientosPorPrestamoResponse>>
    {
        public int UsuarioId {  get; set; }
        public long PrestamoId {  get; set; }
    }
}
