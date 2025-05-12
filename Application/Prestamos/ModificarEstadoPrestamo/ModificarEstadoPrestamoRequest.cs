using MediatR;

namespace Application.Prestamos.ModificarEstadoPrestamo;

public class ModificarEstadoPrestamoRequest : ModificarEstadoPrestamoDTO, IRequest<bool>
{
    public int UsuarioId {  get; set; }
}

public class ModificarEstadoPrestamoDTO
{
    public long PrestamoId { get; set; }
    public string CodigoEstado { get; set; } = string.Empty;
}
