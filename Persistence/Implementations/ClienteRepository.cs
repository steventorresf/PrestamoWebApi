using Domain.Response;
using Microsoft.EntityFrameworkCore;
using Persistence.Entities;

namespace Persistence.Implementations
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly BaseContext _context;

        public ClienteRepository(BaseContext context)
        {
            _context = context;
        }

        public async Task<ResponseListItem<Cliente>> GetClientes(int uid, string? textFilter, int pageNumber, int pageSize)
        {
            IQueryable<Cliente> lista = _context.Cliente
                .Include(ti => ti.TipoIdentificacion)
                .Include(ge => ge.Genero)
                .Where(c => c.UsuarioId == uid && c.NombreCompleto.Contains(textFilter ?? string.Empty))
                .OrderBy(c => c.NombreCompleto);

            ResponseListItem<Cliente> response = new()
            {
                CountItems = await lista.CountAsync(),
                ListItems = await lista
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync()
            };

            return response;
        }

        public async Task<Cliente> PostCliente(Cliente entity)
        {
            if (entity.ClienteId == 0)
            {
                var response = await _context.Cliente.AddAsync(entity);
                _context.SaveChanges();
                return response.Entity;
            }
            else
            {
                var response = _context.Update(entity);
                _context.SaveChanges();
                return response.Entity;
            }
        }
    }
}
