using MediatR;

namespace Application.PrestamosDetalles.GuardarCobro;

public class GuardarCobroRequest : GuardarCobroDTO, IRequest<bool>
{
    public long UsuarioId {  get; set; }    
}

public class GuardarCobroDTO
{
    public long PrestamoDetalleId { get; set; }
    public string FechaPago { get; set; } = string.Empty;
}