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
        Prestamo? prestamo = await _context.Prestamo
            .Include(x => x.Cliente).ThenInclude(x => x.Estado)
            .FirstOrDefaultAsync(x => x.PrestamoId == request.PrestamoId && x.Cliente.UsuarioId == request.UsuarioId);

        if (prestamo == null)
            throw new BadRequestException(string.Format("No existe un prestamo con el ID {0} para el usuario logueado.", request.PrestamoId));

        if (request.CodigoEstado.Equals(Constants.CodigoEstado_Prestamo_Pendiente) && !prestamo.Cliente.Estado.Codigo.Equals(Constants.CodigoEstado_Cliente_Activo))
            throw new BadRequestException("El prestamo no puede activarse, porque el cliente no se encuentra activo.");

        long estadoId = await _tablaDetalleRepository.ObtenerTablaDetalleId(Constants.TablaId_EstadosPrestamos, request.CodigoEstado);

        prestamo.EstadoId = estadoId;
        prestamo.FechaAnulado = request.CodigoEstado.Equals(Constants.CodigoEstado_Prestamo_Pendiente) || request.CodigoEstado.Equals(Constants.CodigoEstado_Prestamo_Finalizado) ? null : DateTime.Now;
        _context.Update(prestamo);

        await _context.SaveChangesAsync();

        return true;
    }
}
