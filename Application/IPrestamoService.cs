using Domain.DTO;
using Domain.Response;

namespace Application
{
    public interface IPrestamoService
    {
        Task<ResponseData<ResponseListItem<PrestamoDTO>>> GetPrestamosByClienteId(long clienteId);
    }
}
