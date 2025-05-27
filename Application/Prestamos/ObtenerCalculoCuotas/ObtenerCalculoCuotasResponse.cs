namespace Application.Prestamos.ObtenerCalculoCuotas;

public class ObtenerCalculoCuotasResponse
{
    public decimal CapitalTotal { get; set; }
    public decimal InteresesTotal { get; set; }
    public decimal ValorTotal { get; set; }
    public List<ObtenerCalculoCuotasDTO> ListadoCuotas { get; set; } = new();
}

public class ObtenerCalculoCuotasDTO
{
    public string sFechaCuota { get; set; } = string.Empty;
    public DateTime FechaCuota { get; set; }
    public decimal Capital { get; set; }
    public decimal Intereses { get; set; }
    public decimal ValorTotal { get; set; }
}