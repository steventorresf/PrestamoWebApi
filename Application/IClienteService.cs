using Domain.DTO;
using Domain.Response;

namespace Application
{
    public interface IClienteService
    {
        Task<ResponseListItem<ClienteDTO>> GetClientes(int uid, string? textFilter, int pageNumber, int pageSize);
        Task<ClienteRequestDTO> PostCliente(ClienteRequestDTO request);
    }
}
