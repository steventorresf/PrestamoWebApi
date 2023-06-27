using Domain.DTO;
using Domain.Response;
using Persistence;
using Persistence.Entities;
using System.Net;

namespace Application.Implementations
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteService(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<ResponseListItem<ClienteDTO>> GetClientes(int uid, string? textFilter, int pageNumber, int pageSize)
        {
            var result = await _clienteRepository.GetClientes(uid, textFilter, pageNumber, pageSize);
            ResponseListItem<ClienteDTO> list = new()
            {
                CountItems = result.CountItems,
                ListItems = result.ListItems.Select(x=>new ClienteDTO()
                {
                    ClienteId = x.ClienteId,
                    TipoId = x.TipoId,
                    TipoIdentificacion = x.TipoIdentificacion?.Codigo ?? string.Empty,
                    Identificacion = x.Identificacion,
                    NombreCompleto = x.NombreCompleto,
                    GeneroId = x.GeneroId,
                    Genero = x.Genero?.Descripcion ?? string.Empty,
                    TelCel = x.TelCel,
                    Direccion = x.Direccion
                })
            };
            return list;
        }

        public async Task<ClienteRequestDTO> PostCliente(ClienteRequestDTO request)
        {
            Cliente entity = new()
            {
                ClienteId = request.ClienteId,
                TipoId = request.TipoId,
                Identificacion = request.Identificacion,
                NombreCompleto = request.NombreCompleto,
                GeneroId = request.GeneroId,
                TelCel = request.TelCel,
                Direccion = request.Direccion,
                EstadoId = request.EstadoId,
                UsuarioId = request.UsuarioId
            };

            entity = await _clienteRepository.PostCliente(entity);

            ClienteRequestDTO element = new()
            {
                ClienteId = entity.ClienteId,
                TipoId = entity.TipoId,
                Identificacion = entity.Identificacion,
                NombreCompleto = entity.NombreCompleto,
                GeneroId = entity.GeneroId,
                TelCel = entity.TelCel,
                Direccion = entity.Direccion,
                EstadoId = entity.EstadoId,
                UsuarioId = entity.UsuarioId
            };
            return element;
        }
    }
}
