using MediatR;

namespace Application.Prestamos.FinalizarPrestamo;

public class FinalizarPrestamoRequest : FinalizarPrestamoDTO, IRequest<bool>
{
    public int UsuarioId {  get; set; }
}

public class FinalizarPrestamoDTO
{
    public long PrestamoId { get; set; }
    public decimal Capital { get; set; }
    public decimal Intereses { get; set; }
    public string FechaPago { get; set; } = string.Empty;
}