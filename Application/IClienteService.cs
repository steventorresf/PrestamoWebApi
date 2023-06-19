using Domain.DTO;
using Domain.Response;

namespace Application
{
    public interface IClienteService
    {
        Task<ResponseData<ResponseListItem<ClienteDTO>>> GetClientes(int uid, string? textFilter, int pageNumber, int pageSize);
        Task<ResponseData<ClienteRequestDTO>> PostCliente(ClienteRequestDTO request);
    }
}
