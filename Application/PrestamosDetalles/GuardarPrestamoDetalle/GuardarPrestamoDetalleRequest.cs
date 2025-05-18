using MediatR;

namespace Application.PrestamosDetalles.GuardarPrestamoDetalle;

public class GuardarPrestamoDetalleRequest : IRequest<bool>
{
    public long PrestamoId {  get; set; }
    public decimal Capital {  get; set; }
    public decimal Intereses {  get; set; }
    public string FechaCuota {  get; set; } = string.Empty;
}
