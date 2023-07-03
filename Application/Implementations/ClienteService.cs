using Domain.DTO;
using Domain.Response;
using Entities;
using MongoDB.Bson;
using Persistence.Repositories;

namespace Application.Implementations
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteService(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<ResponseListItem<ClienteDTO>> GetClientes(string uid, string? textFilter, int pageNumber, int pageSize)
        {
            var result = await _clienteRepository.GetClientes(uid, textFilter, pageNumber, pageSize);
            ResponseListItem<ClienteDTO> list = new()
            {
                CountItems = result.CountItems,
                ListItems = result.ListItems.Select(x=>new ClienteDTO()
                {
                    ClienteId = x.Id,
                    TipoId = x.TipoId,
                    TipoIdentificacion = string.Empty,
                    Identificacion = x.Identificacion,
                    NombreCompleto = x.NombreCompleto,
                    Genero = x.GeneroId,
                    TelCel = x.TelCel,
                    Direccion = x.Direccion
                })
            };
            return list;
        }

        public async Task PostCliente(ClienteRequestDTO request)
        {
            Cliente entity = new()
            {
                TipoId = request.TipoId,
                Identificacion = request.Identificacion,
                NombreCompleto = request.NombreCompleto,
                GeneroId = request.GeneroId,
                TelCel = request.TelCel,
                Direccion = request.Direccion,
                EstadoId = request.EstadoId,
                UsuarioId = request.UsuarioId
            };

            await _clienteRepository.PostCliente(entity);
        }
    }
}
