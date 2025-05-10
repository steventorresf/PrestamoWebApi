namespace Application.Prestamos.ObtenerPrestamosCongelados;

public class ObtenerPrestamosCongeladosResponse
{
    public long Id { get; set; }
    public long IdCliente { get; set; }
    public string NomCliente { get; set; } = string.Empty;
    public DateTime FechaPrestamo { get; set; }
    public DateTime? FechaAnulado { get; set; }
    public decimal ValorTotal { get; set; }
    public decimal ValorRestante { get; set; }
    public decimal ValorPagado { get; set; }
}
