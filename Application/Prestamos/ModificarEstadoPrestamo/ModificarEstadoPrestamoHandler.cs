using Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Entities;
using Persistence.Utilities;

namespace Application.Prestamos.ModificarEstadoPrestamo;

public class ModificarEstadoPrestamoHandler : IRequestHandler<ModificarEstadoPrestamoRequest, bool>
{
    private readonly BaseContext _context;

    public ModificarEstadoPrestamoHandler(BaseContext context)
    {
        this._context = context;
    }

    public async Task<bool> Handle(ModificarEstadoPrestamoRequest request, CancellationToken cancellationToken)
    {
        TablaDetalle? tablaDetalle = await _context.TablaDetalle.FirstOrDefaultAsync(x => x.TablaId == Constants.TablaId_EstadosPrestamos && x.Codigo.Equals(request.CodigoEstado));
        if (tablaDetalle == null)
            throw new BadRequestException(string.Format("No existe un código '{0}' de estado de prestamo.", request.CodigoEstado));

        Prestamo? prestamo = await _context.Prestamo
            .Include(x => x.Cliente)
            .FirstOrDefaultAsync(x => x.PrestamoId == request.PrestamoId && x.Cliente.UsuarioId == request.UsuarioId);

        if (prestamo == null)
            throw new BadRequestException(string.Format("No existe un prestamo con el ID {0} para el usuario logueado.", request.PrestamoId));

        prestamo.EstadoId = tablaDetalle.TablaDetalleId;
        prestamo.FechaAnulado = DateTime.Now;
        _context.Update(prestamo);

        await _context.SaveChangesAsync();

        return true;
    }
}
