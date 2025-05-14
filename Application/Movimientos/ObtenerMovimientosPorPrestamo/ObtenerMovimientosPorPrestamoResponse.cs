namespace Application.Movimientos.ObtenerMovimientosPorPrestamo;

public class ObtenerMovimientosPorPrestamoResponse
{
    public long MovimientoId {  get; set; }
    public DateTime FechaPago {  get; set; }
    public decimal Capital {  get; set; }
    public decimal Intereses {  get; set; }
}
