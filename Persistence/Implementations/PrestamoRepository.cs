using Microsoft.EntityFrameworkCore;
using Persistence.Entities;

namespace Persistence.Implementations
{
    public class PrestamoRepository : IPrestamoRepository
    {
        private readonly BaseContext _context;

        public PrestamoRepository(BaseContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Prestamo>> GetPrestamosByClienteId(long clienteId)
        {
            IEnumerable<Prestamo> lista = await _context.Prestamo
                    .Include(p => p.Periodo)
                    .Include(e => e.Estado)
                    .Where(x => x.ClienteId == clienteId)
                    .OrderByDescending(x => x.PrestamoId)
                    .OrderByDescending(x => x.FechaPrestamo)
                    .ToListAsync();

            return lista;
        }
    }
}
