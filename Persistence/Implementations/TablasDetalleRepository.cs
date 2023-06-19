using Microsoft.EntityFrameworkCore;
using Persistence.Entities;

namespace Persistence.Implementations
{
    public class TablasDetalleRepository : ITablasDetalleRepository
    {
        private readonly BaseContext _context;

        public TablasDetalleRepository(BaseContext context)
        {
            _context = context;
        }

        public async Task<List<TablaDetalle>> GetTablaDetalle(long tablaId)
        {
            List<TablaDetalle> lista = await _context.TablaDetalle
                .Where(t => t.TablaId == tablaId && t.Visible)
                .OrderBy(c => c.Descripcion)
                .OrderBy(c => c.Orden)
                .ToListAsync();

            return lista;
        }
    }
}
