using MediatR;

namespace Application.PrestamosDetalles.GuardarAbono;

public class GuardarAbonoRequest : GuardarAbonoDTO, IRequest<bool>
{
    public long UsuarioId {  get; set; }
}

public class GuardarAbonoDTO
{
    public long PrestamoDetalleId {  get; set; }
    public decimal AbonoCapital {  get; set; }
    public decimal AbonoIntereses {  get; set; }
    public string FechaPago {  get; set; } = string.Empty;
}