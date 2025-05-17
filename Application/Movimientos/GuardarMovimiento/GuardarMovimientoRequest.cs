using MediatR;

namespace Application.Movimientos.GuardarMovimiento;

public class GuardarMovimientoRequest : GuardarMovimientoDTO, IRequest<bool>
{
    public int UsuarioId {  get; set; }
}

public class GuardarMovimientoDTO
{
    public string FechaPago { get; set; } = string.Empty;
    public long ClienteId {  get; set; }
    public long PrestamoId {  get; set; }
    public decimal Capital {  get; set; }
    public decimal Intereses {  get; set; }
}