using Domain.DTO;
using Domain.Response;

namespace Application
{
    public interface IClienteService
    {
        Task<ResponseListItem<ClienteDTO>> GetClientes(string uid, string? textFilter, int pageNumber, int pageSize);
        Task PostCliente(ClienteRequestDTO request);
    }
}
