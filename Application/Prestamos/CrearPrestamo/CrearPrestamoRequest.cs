using MediatR;

namespace Application.Prestamos.CrearPrestamo;

public class CrearPrestamoRequest : IRequest<bool>
{
    public long ClienteId { get; set; }
    public decimal ValorPrestamo { get; set; }
    public decimal Porcentaje { get; set; }
    public int Dias { get; set; }
    public string FechaPrestamo { get; set; } = string.Empty;
    public string FechaInicio { get; set; } = string.Empty;
    public int NoCuotas { get; set; }
    public long PeriodoId { get; set; }
    public List<CrearPrestamoDetalleRequest> PrestamoDetalle { get; set; } = new();
}

public class CrearPrestamoDetalleRequest
{
    public int Capital { get; set; }
    public int Intereses { get; set; }
    public string FechaCuota { get; set; } = string.Empty;
}
