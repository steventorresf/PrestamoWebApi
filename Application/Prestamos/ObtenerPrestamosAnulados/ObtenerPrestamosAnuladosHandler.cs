using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Entities;
using Persistence.Interfaces;
using Persistence.Utilities;

namespace Application.Prestamos.ObtenerPrestamosAnulados;

public class ObtenerPrestamosAnuladosHandler : IRequestHandler<ObtenerPrestamosAnuladosRequest, List<ObtenerPrestamosAnuladosResponse>>
{
    private readonly BaseContext _context;
    private readonly ITablaDetalleRepository _tablaDetalleRepository;

    public ObtenerPrestamosAnuladosHandler(BaseContext context, ITablaDetalleRepository tablaDetalleRepository)
    {
        this._context = context;
        this._tablaDetalleRepository = tablaDetalleRepository;
    }

    public async Task<List<ObtenerPrestamosAnuladosResponse>> Handle(ObtenerPrestamosAnuladosRequest request, CancellationToken cancellationToken)
    {
        long estadoIdPrestamoAnulado = await _tablaDetalleRepository.ObtenerTablaDetalleId(Constants.TablaId_EstadosPrestamos, Constants.CodigoEstado_Prestamo_Anulado);
        IQueryable<Prestamo> prestamos = _context.Prestamo
                    .Include(e => e.Cliente)
                    .Where(x => x.Cliente.UsuarioId == request.UsuarioId && x.EstadoId == estadoIdPrestamoAnulado);

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
