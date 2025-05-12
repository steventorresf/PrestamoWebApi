using Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Entities;
using Persistence.Interfaces;
using Persistence.Utilities;

namespace Application.Clientes.ModificarEstadoCliente;

public class ModificarEstadoClienteHandler : IRequestHandler<ModificarEstadoClienteRequest, bool>
{
    private readonly BaseContext _context;
    private readonly ITablaDetalleRepository _tablaDetalleRepository;

    public ModificarEstadoClienteHandler(BaseContext context, ITablaDetalleRepository tablaDetalleRepository)
    {
        this._context = context;
        this._tablaDetalleRepository = tablaDetalleRepository;
    }

    public async Task<bool> Handle(ModificarEstadoClienteRequest request, CancellationToken cancellationToken)
    {
        long estadoId = await _tablaDetalleRepository.ObtenerTablaDetalleId(Constants.TablaId_EstadosClientes, request.CodigoEstado);

        Cliente? cliente =
            await _context.Cliente
            .FirstOrDefaultAsync(x => x.ClienteId == request.ClienteId &&
                                      x.UsuarioId == request.UsuarioId);

        if (cliente == null)
            throw new BadRequestException(string.Format("No existe un cliente con el ID {0} para el usuario logueado.", request.ClienteId));

        cliente.EstadoId = estadoId;
        _context.Update(cliente);

        await _context.SaveChangesAsync();

        return true;
    }
}
