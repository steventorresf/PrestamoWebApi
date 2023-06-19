using Domain.Response;
using Persistence.Entities;

namespace Persistence
{
    public interface IClienteRepository
    {
        Task<ResponseListItem<Cliente>> GetClientes(int uid, string? textFilter, int pageNumber, int pageSize);
        Task<Cliente> PostCliente(Cliente entity);
    }
}
