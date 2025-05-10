using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Entities;
using Persistence.Utilities;

namespace Application.Prestamos.ObtenerPrestamosAnulados;

public class ObtenerPrestamosAnuladosHandler : IRequestHandler<ObtenerPrestamosAnuladosRequest, List<ObtenerPrestamosAnuladosResponse>>
{
    private readonly BaseContext _context;

    public ObtenerPrestamosAnuladosHandler(BaseContext context)
    {
        this._context = context;
    }

    public async Task<List<ObtenerPrestamosAnuladosResponse>> Handle(ObtenerPrestamosAnuladosRequest request, CancellationToken cancellationToken)
    {
        IQueryable<Prestamo> prestamos = _context.Prestamo
                    .Include(e => e.Cliente)
                    .Where(x => x.Cliente.UsuarioId == request.UsuarioId && x.EstadoId == Constants.IdEst_Prestamo_Anulado);

        List<ObtenerPrestamosAnuladosResponse> Resultado =
            await prestamos.Select(x => new ObtenerPrestamosAnuladosResponse
            {
                Id = x.PrestamoId,
                IdCliente = x.ClienteId,
                NomCliente = x.Cliente.NombreCompleto,
                FechaPrestamo = x.FechaPrestamo,
                FechaAnulado = x.FechaAnulado,
                ValorTotal = x.ValorPrestamo + ((x.ValorPrestamo * x.Porcentaje / 100) / 30 * x.Dias),
                EstadoId = x.EstadoId
            }).ToListAsync();

        return Resultado;
    }
}
