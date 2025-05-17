namespace Application.Movimientos.ObtenerGanancias;

public class ObtenerGananciasResponse
{
    public DateTime FechaPago { get; set; }
    public string NombreCliente {  get; set; } = string.Empty;
    public string Descripcion {  get; set; } = string.Empty;
    public decimal Capital {  get; set; }
    public decimal Intereses {  get; set; }
    public decimal ValorTotal {  get; set; }
}
