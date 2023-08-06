using Entities;

namespace Persistence.Repositories
{
    public interface ITablaRepository
    {
        Task<Tabla> GetTablaByCodigo(string codigo);
    }
}
