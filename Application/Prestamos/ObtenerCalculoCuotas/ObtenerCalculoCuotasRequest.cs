using MediatR;

namespace Application.Prestamos.ObtenerCalculoCuotas;

public class ObtenerCalculoCuotasRequest : IRequest<List<ObtenerCalculoCuotasResponse>>
{
    public int UsuarioId { get; set; }
    public string FechaInicio { get; set; } = string.Empty;
    public long PeriodoId {  get; set; }
    public int NoCuotas { get; set; }
    public decimal ValorPrestamo { get; set; }
    public decimal ValorTotal { get; set; }
}
