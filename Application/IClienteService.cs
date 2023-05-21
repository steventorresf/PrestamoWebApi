using Domain.DTO;
using Domain.Response;

namespace Application
{
    public interface IClienteService
    {
        Task<ResponseListItem<ClienteDTO>> GetClientes(int IdUsuario, string? textFilter, int pageNumber, int pageSize);
        Task<ClienteRequestDTO> PostCliente(int IdUsuario, ClienteRequestDTO request);
    }
}
