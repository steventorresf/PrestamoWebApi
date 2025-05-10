using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Entities;
using Persistence.Utilities;

namespace Application.Prestamos.ObtenerPrestamosPendientes;

public class ObtenerPrestamosPendientesHandler : IRequestHandler<ObtenerPrestamosPendientesRequest, List<ObtenerPrestamosPendientesResponse>>
{
    private readonly BaseContext _context;

    public ObtenerPrestamosPendientesHandler(BaseContext context)
    {
        this._context = context;
    }

    public async Task<List<ObtenerPrestamosPendientesResponse>> Handle(ObtenerPrestamosPendientesRequest request, CancellationToken cancellationToken)
    {
        IQueryable<Prestamo> prestamos = _context.Prestamo
            .Include(x => x.Cliente)
            .Where(x => x.Cliente.UsuarioId == request.UsuarioId &&
                        x.EstadoId == Constants.IdEst_Prestamo_Pendiente);

        List<ObtenerPrestamosPendientesResponse> Resultado =
            await prestamos
            .Select(x => new ObtenerPrestamosPendientesResponse
            {
                Id = x.PrestamoId,
                NomCliente = x.Cliente.NombreCompleto,
                FechaPrestamo = x.FechaPrestamo,
                Capital = x.PeriodoId == Constants.IdPeri_PorAbonos ? x.ValorPrestamo - x.Movimientos.Sum(s => s.Capital) : x.PrestamoDetalles.Sum(s => s.Capital),
                Intereses = x.PeriodoId == Constants.IdPeri_PorAbonos ? 0 : x.PrestamoDetalles.Sum(s => s.Intereses)
            }).ToListAsync();

        return Resultado;
    }
}