using Domain.DTO;
using Domain.Response;

namespace Application
{
    public interface IPrestamoService
    {
        Task<ResponseListItem<PrestamoDTO>> GetPrestamosByClienteId(long clienteId);
    }
}
