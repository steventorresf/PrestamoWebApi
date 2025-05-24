using Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Entities;

namespace Application.PrestamosDetalles.EliminarPrestamoDetalle;

public class EliminarPrestamoDetalleHandler : IRequestHandler<EliminarPrestamoDetalleRequest, bool>
{
    private readonly BaseContext _context;

    public EliminarPrestamoDetalleHandler(BaseContext context)
    {
        this._context = context;
    }

    public async Task<bool> Handle(EliminarPrestamoDetalleRequest request, CancellationToken cancellationToken)
    {
        PrestamoDetalle? prestamoDetalle =
            await _context.PrestamoDetalle
            .Include(x => x.Prestamo).ThenInclude(x => x.Cliente)
            .FirstOrDefaultAsync(x => x.PrestamoDetalleId == request.PrestamoDetalleId &&
                                      x.Prestamo.Cliente.UsuarioId == request.UsuarioId);

        if (prestamoDetalle == null)
            throw new BadRequestException("No existe una coincidencia con el parametro enviado.");

        _context.PrestamoDetalle.Remove(prestamoDetalle);
        await _context.SaveChangesAsync();

        return true;
    }
}
