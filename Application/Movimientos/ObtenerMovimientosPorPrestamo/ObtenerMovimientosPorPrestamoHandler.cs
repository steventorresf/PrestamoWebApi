using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Entities;

namespace Application.Movimientos.ObtenerMovimientosPorPrestamo;

public class ObtenerMovimientosPorPrestamoHandler : IRequestHandler<ObtenerMovimientosPorPrestamoRequest, List<ObtenerMovimientosPorPrestamoResponse>>
{
    private readonly BaseContext _context;

    public ObtenerMovimientosPorPrestamoHandler(BaseContext context)
    {
        this._context = context;
    }

    public async Task<List<ObtenerMovimientosPorPrestamoResponse>> Handle(ObtenerMovimientosPorPrestamoRequest request, CancellationToken cancellationToken)
    {
        IQueryable<Movimiento> movimientos =
            _context.Movimiento
            .Where(x => x.PrestamoId == request.PrestamoId &&
                        x.UsuarioId == request.UsuarioId);

        List<ObtenerMovimientosPorPrestamoResponse> Resultado =
            await movimientos
            .Select(x => new ObtenerMovimientosPorPrestamoResponse
            {
                MovimientoId = x.MovimientoId,
                Capital = x.Capital,
                FechaPago = x.FechaPago,
                Intereses = x.Intereses
            }).ToListAsync();

        return Resultado;
    }
}
