using MediatR;

namespace Application.TablaDetalles.ObtenerTablaDetallesPorCodigos;

public class ObtenerTablaDetallesPorCodigosRequest : IRequest<List<ObtenerTablaDetallesPorCodigosResponse>>
{
    public string Codigos { get; set; } = string.Empty;
}
