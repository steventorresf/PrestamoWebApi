namespace Application.Prestamos.ObtenerPrestamoDetalle;

public class ObtenerPrestamoDetalleResponse
{
    public decimal Capital { get; set; }
    public decimal Intereses { get; set; }
    public decimal AbonoCapital { get; set; }
    public decimal AbonoIntereses { get; set; }
    public DateTime FechaCuota { get; set; }
    public DateTime? FechaPago { get; set; }
    public bool Pagado { get; set; }
}
