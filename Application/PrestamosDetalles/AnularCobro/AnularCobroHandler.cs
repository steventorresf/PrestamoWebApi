using Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Entities;

namespace Application.PrestamosDetalles.AnularCobro;

public class AnularCobroHandler : IRequestHandler<AnularCobroRequest, bool>
{
    private readonly BaseContext _context;

    public AnularCobroHandler(BaseContext context)
    {
        this._context = context;
    }

    public async Task<bool> Handle(AnularCobroRequest request, CancellationToken cancellationToken)
    {
        PrestamoDetalle? prestamoDetalle =
            await _context.PrestamoDetalle
            .Include(x => x.Prestamo).ThenInclude(x => x.Cliente)
            .FirstOrDefaultAsync(x => x.PrestamoDetalleId == request.PrestamoDetalleId &&
                                      x.Prestamo.Cliente.UsuarioId == request.UsuarioId);

        if (prestamoDetalle == null)
            throw new BadRequestException("No existe una coincidencia con el parametro enviado.");

        prestamoDetalle.AbonoIntereses = 0;
        prestamoDetalle.AbonoCapital = 0;
        prestamoDetalle.Pagado = false;
        prestamoDetalle.FechaPago = null;
        _context.PrestamoDetalle.Update(prestamoDetalle);

        List<Movimiento> movimientos =
            await _context.Movimiento
            .Where(x => x.PrestamoDetalleId == request.PrestamoDetalleId &&
                        x.UsuarioId == request.UsuarioId)
            .ToListAsync();
        _context.Movimiento.RemoveRange(movimientos);

        await _context.SaveChangesAsync();

        return true;
    }
}
