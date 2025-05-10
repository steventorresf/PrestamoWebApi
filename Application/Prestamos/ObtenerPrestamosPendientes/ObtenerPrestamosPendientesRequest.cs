using MediatR;

namespace Application.Prestamos.ObtenerPrestamosPendientes;

public class ObtenerPrestamosPendientesRequest : IRequest<List<ObtenerPrestamosPendientesResponse>>
{
    public int UsuarioId {  get; set; }
}
