using Persistence.Entities;

namespace Persistence
{
    public interface IPrestamoRepository
    {
        Task<IEnumerable<Prestamo>> GetPrestamosByClienteId(long clienteId);
    }
}
