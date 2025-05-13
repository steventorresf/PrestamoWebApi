using Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Entities;
using Persistence.Interfaces;
using Persistence.Utilities;

namespace Application.Clientes.ModificarEstadoCliente;

public class ModificarEstadoClienteHandler : IRequestHandler<ModificarEstadoClienteRequest, ModificarEstadoClienteResponse>
{
    private readonly BaseContext _context;
    private readonly ITablaDetalleRepository _tablaDetalleRepository;

    public ModificarEstadoClienteHandler(BaseContext context, ITablaDetalleRepository tablaDetalleRepository)
    {
        this._context = context;
        this._tablaDetalleRepository = tablaDetalleRepository;
    }

    public async Task<ModificarEstadoClienteResponse> Handle(ModificarEstadoClienteRequest request, CancellationToken cancellationToken)
    {
        long estadoId = await _tablaDetalleRepository.ObtenerTablaDetalleId(Constants.TablaId_EstadosClientes, request.CodigoEstado);

        Cliente? cliente =
            await _context.Cliente
            .Include(x => x.Estado)
            .Include(x => x.Prestamos).ThenInclude(x => x.Estado)
            .FirstOrDefaultAsync(x => x.ClienteId == request.ClienteId &&
                                      x.UsuarioId == request.UsuarioId);

        if (cliente == null)
            throw new BadRequestException(string.Format("No existe un cliente con el ID {0} para el usuario logueado.", request.ClienteId));

        if (cliente.Estado.Codigo.Equals(Constants.CodigoEstado_Cliente_Activo))
        {
            int cantPendiente = cliente.Prestamos
                .Where(x => x.Estado.Codigo.Equals(Constants.CodigoEstado_Prestamo_Pendiente))
                .Count();

            if (cantPendiente > 0)
            {
                return new ModificarEstadoClienteResponse
                {
                    Result = false,
                    Message = "No puede modificar el estado del cliente <b>\"" + cliente.NombreCompleto + "\"</b>, porque tiene " + cantPendiente.ToString() + " prestamos pendiente(s)."
                };
            }
        }

        cliente.EstadoId = estadoId;
        _context.Update(cliente);

        await _context.SaveChangesAsync();

        return new ModificarEstadoClienteResponse
        {
            Result = true,
            Message = "El estado del cliente ha sido modificado exitosamente."
        };
    }
}
