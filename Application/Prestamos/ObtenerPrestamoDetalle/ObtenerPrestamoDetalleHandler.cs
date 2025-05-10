using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Entities;

namespace Application.Prestamos.ObtenerPrestamoDetalle;

public class ObtenerPrestamoDetalleHandler : IRequestHandler<ObtenerPrestamoDetalleRequest, List<ObtenerPrestamoDetalleResponse>>
{
    private readonly BaseContext _context;

    public ObtenerPrestamoDetalleHandler(BaseContext context)
    {
        this._context = context;
    }

    public async Task<List<ObtenerPrestamoDetalleResponse>> Handle(ObtenerPrestamoDetalleRequest request, CancellationToken cancellationToken)
    {
        IQueryable<PrestamoDetalle> prestamoDetalle = _context.PrestamoDetalle
                    .Where(x => x.PrestamoId == request.PrestamoId)
                    .OrderBy(x => x.FechaCuota);

        List<ObtenerPrestamoDetalleResponse> Resultado =
            await prestamoDetalle.Select(x => new ObtenerPrestamoDetalleResponse
            {
                Capital = x.Capital,
                Intereses = x.Intereses,
                FechaCuota = x.FechaCuota,
                AbonoCapital = x.AbonoCapital,
                AbonoIntereses = x.AbonoIntereses,
                FechaPago = x.FechaPago
            }).ToListAsync();

        return Resultado;
    }
}
