using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Entities;
using Persistence.Interfaces;
using Persistence.Utilities;

namespace Application.Prestamos.ObtenerPrestamosPendientes;

public class ObtenerPrestamosPendientesHandler : IRequestHandler<ObtenerPrestamosPendientesRequest, List<ObtenerPrestamosPendientesResponse>>
{
    private readonly BaseContext _context;
    private readonly ITablaDetalleRepository _tablaDetalleRepository;

    public ObtenerPrestamosPendientesHandler(BaseContext context, ITablaDetalleRepository tablaDetalleRepository)
    {
        this._context = context;
        this._tablaDetalleRepository = tablaDetalleRepository;
    }

    public async Task<List<ObtenerPrestamosPendientesResponse>> Handle(ObtenerPrestamosPendientesRequest request, CancellationToken cancellationToken)
    {
        long estadoIdPrestamoPendiente = await _tablaDetalleRepository.ObtenerTablaDetalleId(Constants.TablaId_EstadosPrestamos, Constants.CodigoEstado_Prestamo_Pendiente);
        long periodoIdAbonoPrestamo = await _tablaDetalleRepository.ObtenerTablaDetalleId(Constants.TablaId_PeriodosPrestamos, Constants.CodigoPeriodo_PorAbonos);

        IQueryable<Prestamo> prestamos = _context.Prestamo
            .Include(x => x.Cliente)
            .Where(x => x.Cliente.UsuarioId == request.UsuarioId &&
                        x.EstadoId == estadoIdPrestamoPendiente);

        List<ObtenerPrestamosPendientesResponse> Resultado =
            await prestamos
            .Select(x => new ObtenerPrestamosPendientesResponse
            {
                Id = x.PrestamoId,
                NomCliente = x.Cliente.NombreCompleto,
                FechaPrestamo = x.FechaPrestamo,
                Capital = x.PeriodoId == periodoIdAbonoPrestamo ? x.ValorPrestamo - x.Movimientos.Sum(s => s.Capital) : x.PrestamoDetalles.Sum(s => s.Capital),
                Intereses = x.PeriodoId == periodoIdAbonoPrestamo ? 0 : x.PrestamoDetalles.Sum(s => s.Intereses)
            }).ToListAsync();

        return Resultado;
    }
}