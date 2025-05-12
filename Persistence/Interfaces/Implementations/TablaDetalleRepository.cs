using Microsoft.EntityFrameworkCore;
using Persistence.Entities;

namespace Persistence.Interfaces.Implementations;

public class TablaDetalleRepository : ITablaDetalleRepository
{
    private readonly BaseContext _context;

    public TablaDetalleRepository(BaseContext context)
    {
        this._context = context;
    }

    public async Task<List<TablaDetalle>> ObtenerTablaDetalles(long tablaId)
    {
        List<TablaDetalle> listaPeriodos =
            await _context.TablaDetalle
            .Where(x => x.TablaId == tablaId)
            .ToListAsync();

        return listaPeriodos;
    }

    public async Task<long> ObtenerTablaDetalleId(long tablaId, string codigo)
    {
        TablaDetalle? tablaDetalle =
            await _context.TablaDetalle
            .FirstOrDefaultAsync(x => x.TablaId == tablaId && x.Codigo.Equals(codigo));

        if (tablaDetalle == null)
            throw new Exception(string.Format("No existe el código '{0}' en la TablaId {1}", codigo, tablaId));

        return tablaDetalle.TablaDetalleId;
    }

    public long ObtenerTablaDetalleId(List<TablaDetalle> lista, string codigo, long tablaId)
    {
        TablaDetalle? tablaDetalle = lista.FirstOrDefault(x => x.Codigo.Equals(codigo));

        if (tablaDetalle == null)
            throw new Exception(string.Format("No existe el código '{0}' en la TablaId {1}", codigo, tablaId));

        return tablaDetalle.TablaDetalleId;
    }
}
