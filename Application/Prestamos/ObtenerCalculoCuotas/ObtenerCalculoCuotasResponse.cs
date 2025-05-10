namespace Application.Prestamos.ObtenerCalculoCuotas;

public class ObtenerCalculoCuotasResponse
{
    public DateTime FechaCuota {  get; set; }
    public decimal Capital {  get; set; }
    public decimal Intereses { get; set;}
}
