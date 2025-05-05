using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.TablaDetalles.ObtenerTablaDetallesPorCodigos;

public class ObtenerTablaDetallesPorCodigosHandler : IRequestHandler<ObtenerTablaDetallesPorCodigosRequest, List<ObtenerTablaDetallesPorCodigosResponse>>
{
    private readonly BaseContext _context;

    public ObtenerTablaDetallesPorCodigosHandler(BaseContext context)
    {
        this._context = context;
    }
    public async Task<List<ObtenerTablaDetallesPorCodigosResponse>> Handle(ObtenerTablaDetallesPorCodigosRequest request, CancellationToken cancellationToken)
    {
        string[] arrayCodigos = request.Codigos.Split(',', StringSplitOptions.RemoveEmptyEntries);
        List<ObtenerTablaDetallesPorCodigosResponse> Resultado = new();
        foreach (string item in arrayCodigos)
        {
            long tablaId = 0;
            if (long.TryParse(item, out tablaId))
            {
                List<TablaDetalleDTO> listaDetalles =
                    await _context.TablaDetalle
                    .Where(t => t.TablaId == tablaId && t.Visible)
                    .OrderBy(c => c.Descripcion)
                    .OrderBy(c => c.Orden)
                    .Select(x => new TablaDetalleDTO
                    {
                        TablaDetalleId = x.TablaDetalleId,
                        Codigo = x.Codigo,
                        Descripcion = x.Descripcion
                    }).ToListAsync();

                Resultado.Add(new()
                {
                    TablaId = tablaId,
                    Listado = listaDetalles
                });
            }
        }
        return Resultado;
    }
}
