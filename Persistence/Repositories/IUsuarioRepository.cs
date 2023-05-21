using Persistence.Entities;

namespace Persistence.Repositories
{
    public interface IUsuarioRepository
    {
        Task<Usuario?> ObtenerUsuarioByLogin(string nombreUsuario);
    }
}
