namespace Application.PrestamosDetalles.ObtenerGananciasEsperadas;

public class ObtenerGananciasEsperadasResponse
{
    public DateTime FechaCuota { get; set; }
    public string NombreCliente { get; set; } = string.Empty;
    public decimal Capital { get; set; }
    public decimal Intereses { get; set; }
    public decimal ValorTotal { get; set; }
}
