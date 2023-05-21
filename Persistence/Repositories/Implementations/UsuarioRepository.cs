using Microsoft.EntityFrameworkCore;
using Persistence.Entities;

namespace Persistence.Repositories.Implementations
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly BaseContext _context;

        public UsuarioRepository(BaseContext context)
        {
            _context = context;
        }

        public async Task<Usuario?> ObtenerUsuarioByLogin(string nombreUsuario)
        {
            Usuario? entity = await _context.Usuario
                .FirstOrDefaultAsync(x => x.NombreUsuario.Equals(nombreUsuario));

            return entity;
        }
    }
}
