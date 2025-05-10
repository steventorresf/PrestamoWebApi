using MediatR;

namespace Application.Prestamos.ObtenerPrestamosAnulados;

public class ObtenerPrestamosAnuladosRequest : IRequest<List<ObtenerPrestamosAnuladosResponse>>
{
    public int UsuarioId {  get; set; }
}
