using Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Entities;

namespace Application.Movimientos.EliminarMovimiento;

public class EliminarMovimientoHandler : IRequestHandler<EliminarMovimientoRequest, bool>
{
    private readonly BaseContext _context;

    public EliminarMovimientoHandler(BaseContext context)
    {
        this._context = context;
    }

    public async Task<bool> Handle(EliminarMovimientoRequest request, CancellationToken cancellationToken)
    {
        Movimiento? movimiento =
            await _context.Movimiento
            .FirstOrDefaultAsync(x => x.MovimientoId == request.MovimientoId &&
                                      x.UsuarioId == request.UsuarioId);

        if (movimiento == null)
            throw new BadRequestException("No existe un movimiento con los datos de entrada.");

        _context.Movimiento.Remove(movimiento);
        await _context.SaveChangesAsync();

        return true;
    }
}
