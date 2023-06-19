using Persistence.Entities;

namespace Persistence
{
    public interface IPrestamoRepository
    {
        Task<List<Prestamo>> GetPrestamosByClienteId(long clienteId);
    }
}
