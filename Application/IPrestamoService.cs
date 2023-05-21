using Domain.DTO;

namespace Application
{
    public interface IPrestamoService
    {
        Task<IEnumerable<PrestamoDTO>> GetPrestamosByClienteId(long clienteId);
    }
}
