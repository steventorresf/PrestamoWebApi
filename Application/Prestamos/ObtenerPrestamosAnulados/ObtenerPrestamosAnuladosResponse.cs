namespace Application.Prestamos.ObtenerPrestamosAnulados;

public class ObtenerPrestamosAnuladosResponse
{
    public long Id { get; set; }
    public long IdCliente { get; set; }
    public string NomCliente { get; set; } = string.Empty;
    public DateTime FechaPrestamo { get; set; }
    public decimal ValorTotal { get; set; }
    public DateTime? FechaAnulado { get; set; }
    public long EstadoId {  get; set; }
}
