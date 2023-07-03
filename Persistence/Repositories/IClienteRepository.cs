using Domain.Response;
using Entities;

namespace Persistence.Repositories
{
    public interface IClienteRepository
    {
        Task<ResponseListItem<Cliente>> GetClientes(string uid, string? textFilter, int pageNumber, int pageSize);
        Task PostCliente(Cliente cliente);
    }
}
