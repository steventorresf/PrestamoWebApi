using Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Entities;
using Persistence.Interfaces;
using Persistence.Utilities;

namespace Application.Prestamos.ModificarEstadoPrestamo;

public class ModificarEstadoPrestamoHandler : IRequestHandler<ModificarEstadoPrestamoRequest, bool>
{
    private readonly BaseContext _context;
    private readonly ITablaDetalleRepository _tablaDetalleRepository;

    public ModificarEstadoPrestamoHandler(BaseContext context, ITablaDetalleRepository tablaDetalleRepository)
    {
        this._context = context;
        this._tablaDetalleRepository = tablaDetalleRepository;
    }

    public async Task<bool> Handle(ModificarEstadoPrestamoRequest request, CancellationToken cancellationToken)
    {
        long estadoId = await _tablaDetalleRepository.ObtenerTablaDetalleId(Constants.TablaId_EstadosPrestamos, request.CodigoEstado);

        Prestamo? prestamo = await _context.Prestamo
            .Include(x => x.Cliente)
            .FirstOrDefaultAsync(x => x.PrestamoId == request.PrestamoId && x.Cliente.UsuarioId == request.UsuarioId);

        if (prestamo == null)
            throw new BadRequestException(string.Format("No existe un prestamo con el ID {0} para el usuario logueado.", request.PrestamoId));

        prestamo.EstadoId = estadoId;
        prestamo.FechaAnulado = DateTime.Now;
        _context.Update(prestamo);

        await _context.SaveChangesAsync();

        return true;
    }
}
