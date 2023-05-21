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

        public async Task<IEnumerable<TablaDetalle>> GetTablaDetalle(long tablaId)
        {
            IEnumerable<TablaDetalle> lista = await _context.TablaDetalle
                .Where(t => t.TablaId == tablaId && t.Visible)
                .OrderBy(c => c.Descripcion)
                .OrderBy(c => c.Orden)
                .ToListAsync();

            return lista;
        }
    }
}
