using MediatR;

namespace Application.Prestamos.ModificarEstadoPrestamo;

public class ModificarEstadoPrestamoRequest : IRequest<bool>
{
    public int UsuarioId {  get; set; }
    public long PrestamoId {  get; set; }
    public string CodigoEstado {  get; set; } = string.Empty;
}
