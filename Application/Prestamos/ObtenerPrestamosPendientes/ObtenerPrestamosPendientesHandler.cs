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
            .Include(x => x.Estado)
            .Include(x => x.Periodo)
            .Where(x => x.Cliente.UsuarioId == request.UsuarioId &&
                        x.Estado.Codigo.Equals(Constants.CodigoEstado_Prestamo_Pendiente));

        List<ObtenerPrestamosPendientesResponse> Resultado =
            await prestamos
            .Select(x => new ObtenerPrestamosPendientesResponse
            {
                Id = x.PrestamoId,
                NomCliente = x.Cliente.NombreCompleto,
                FechaPrestamo = x.FechaPrestamo,
                Capital = x.Periodo.Codigo.Equals(Constants.CodigoPeriodo_PorAbonos) ? x.ValorPrestamo - x.Movimientos.Sum(s => s.Capital) : x.PrestamoDetalles.Where(x => !x.Pagado).Sum(s => s.Capital),
                Intereses = x.Periodo.Codigo.Equals(Constants.CodigoPeriodo_PorAbonos) ? 0 : x.PrestamoDetalles.Where(x => !x.Pagado).Sum(s => s.Intereses)
            }).ToListAsync();

        return Resultado;
    }
}