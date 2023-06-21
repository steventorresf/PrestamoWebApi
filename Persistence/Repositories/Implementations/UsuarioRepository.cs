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

        public async Task<Usuario?> ObtenerUsuarioByLogin(string nombreUsuario, string clave)
        {
            Usuario? user = await _context.Usuario
                .FirstOrDefaultAsync(x => x.NombreUsuario.Equals(nombreUsuario));
            
            if(user != null)
            {
                bool userValid = BCrypt.Net.BCrypt.Verify(clave, user.Clave);
                if (!userValid)
                    return null;
            }

            return user;
        }
    }
}
