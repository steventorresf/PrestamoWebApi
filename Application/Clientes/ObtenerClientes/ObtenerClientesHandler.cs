using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Entities;

namespace Application.Clientes.ObtenerClientes;

public class ObtenerClientesHandler : IRequestHandler<ObtenerClientesRequest, List<ObtenerClientesResponse>>
{
    private readonly BaseContext _context;

    public ObtenerClientesHandler(BaseContext context)
    {
        this._context = context;
    }

    public async Task<List<ObtenerClientesResponse>> Handle(ObtenerClientesRequest request, CancellationToken cancellationToken)
    {
        IQueryable<Cliente> clientes = _context.Cliente
                .Include(x => x.TipoIdentificacion)
                .Include(x => x.Genero)
                .Include(x => x.Estado)
                .Where(x => x.UsuarioId == request.UsuarioId &&
                            x.Estado.Codigo.Equals(request.CodigoEstado));

        if (!string.IsNullOrEmpty(request.TextoFiltro))
            clientes = clientes.Where(x => x.NombreCompleto.Contains(request.TextoFiltro));

        List<ObtenerClientesResponse> Resultado = await clientes
            .Select(x => new ObtenerClientesResponse
            {
                ClienteId = x.ClienteId,
                TipoId = x.TipoId,
                TipoIdentificacion = x.TipoIdentificacion.Codigo,
                Identificacion = x.Identificacion,
                NombreCompleto = x.NombreCompleto,
                GeneroId = x.GeneroId,
                Genero = x.Genero.Descripcion,
                TelCel = x.TelCel,
                Direccion = x.Direccion
            }).ToListAsync();

        return Resultado;
    }
}
