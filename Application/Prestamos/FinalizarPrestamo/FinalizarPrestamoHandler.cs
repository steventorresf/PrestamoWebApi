using Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Entities;
using Persistence.Interfaces;
using Persistence.Utilities;

namespace Application.Prestamos.FinalizarPrestamo;

public class FinalizarPrestamoHandler : IRequestHandler<FinalizarPrestamoRequest, bool>
{
    private readonly BaseContext _context;
    private readonly ITablaDetalleRepository _tablaDetalleRepository;

    public FinalizarPrestamoHandler(BaseContext context, ITablaDetalleRepository tablaDetalleRepository)
    {
        this._context = context;
        this._tablaDetalleRepository = tablaDetalleRepository;
    }

    public async Task<bool> Handle(FinalizarPrestamoRequest request, CancellationToken cancellationToken)
    {
        Prestamo? prestamo =
            await _context.Prestamo
            .Include(p => p.Cliente)
            .FirstOrDefaultAsync(x => x.PrestamoId == request.PrestamoId &&
                                      x.Cliente.UsuarioId == request.UsuarioId);

        if (prestamo == null)
            throw new BadRequestException("Este prestamo NO se encuentra asociado a su usuario.");

        long descripcionIdFinPrestamo = await _tablaDetalleRepository.ObtenerTablaDetalleId(Constants.TablaId_DescripcionesMovimientos, Constants.CodigoDescripcion_Movimiento_FinPrestamo);
        long estadoIdFinPrestamo = await _tablaDetalleRepository.ObtenerTablaDetalleId(Constants.TablaId_EstadosPrestamos, Constants.CodigoEstado_Prestamo_Finalizado);

        DateTime fechaPago = Convert.ToDateTime(request.FechaPago);

        PrestamoDetalle prestamoDetalle = new()
        {
            PrestamoId = request.PrestamoId,
            Capital = request.Capital,
            Intereses = request.Intereses,
            FechaCuota = fechaPago,
            FechaPago = fechaPago,
            AbonoCapital = 0,
            AbonoIntereses = 0,
            Pagado = true
        };

        Movimiento movimiento = new()
        {
            ClienteId = prestamo.ClienteId,
            UsuarioId = request.UsuarioId,
            PrestamoId = request.PrestamoId,
            Capital = request.Capital,
            Intereses = request.Intereses,
            FechaPago = fechaPago,
            DescripcionId = descripcionIdFinPrestamo
        };

        await _context.PrestamoDetalle.AddAsync(prestamoDetalle);
        await _context.Movimiento.AddAsync(movimiento);

        prestamo.EstadoId = estadoIdFinPrestamo;
        _context.Update(prestamo);

        await _context.SaveChangesAsync();

        return true;
    }
}
