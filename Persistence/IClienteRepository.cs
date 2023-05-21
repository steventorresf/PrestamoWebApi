using Domain.Response;
using Persistence.Entities;

namespace Persistence
{
    public interface IClienteRepository
    {
        Task<ResponseListItem<Cliente>> GetClientes(int UsuarioId, string? textFilter, int pageNumber, int pageSize);
        Task<Cliente> PostCliente(int UsuarioId, Cliente entity);
    }
}
