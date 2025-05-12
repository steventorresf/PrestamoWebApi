using MediatR;

namespace Application.Clientes.ObtenerClientes
{
    public class ObtenerClientesRequest : IRequest<List<ObtenerClientesResponse>>
    {
        public int UsuarioId { get; set; }
        public string CodigoEstado { get; set; } = string.Empty;
        public string? TextoFiltro {  get; set; }
    }
}
