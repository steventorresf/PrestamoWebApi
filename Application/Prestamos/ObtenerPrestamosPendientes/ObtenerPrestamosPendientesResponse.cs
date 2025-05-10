namespace Application.Prestamos.ObtenerPrestamosPendientes;

public class ObtenerPrestamosPendientesResponse
{
    public long Id { get; set; }
    public string NomCliente { get; set; } = string.Empty;
    public DateTime FechaPrestamo { get; set; }
    public decimal Capital { get; set; }
    public decimal Intereses { get; set; }
}
