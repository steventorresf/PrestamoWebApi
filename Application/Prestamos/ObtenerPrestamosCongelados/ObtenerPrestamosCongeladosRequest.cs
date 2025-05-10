using MediatR;

namespace Application.Prestamos.ObtenerPrestamosCongelados;

public class ObtenerPrestamosCongeladosRequest : IRequest<List<ObtenerPrestamosCongeladosResponse>>
{
    public int UsuarioId {  get; set; }
}
