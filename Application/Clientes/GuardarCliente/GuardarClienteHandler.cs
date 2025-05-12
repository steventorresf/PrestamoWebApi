using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Entities;
using Persistence.Interfaces;
using Persistence.Utilities;

namespace Application.Clientes.GuardarCliente;

public class GuardarClienteHandler : IRequestHandler<GuardarClienteRequest, GuardarClienteResponse>
{
    private readonly BaseContext _context;
    private readonly ITablaDetalleRepository _tablaDetalleRepository;

    public GuardarClienteHandler(BaseContext context, ITablaDetalleRepository tablaDetalleRepository)
    {
        this._context = context;
        this._tablaDetalleRepository = tablaDetalleRepository;
    }

    public async Task<GuardarClienteResponse> Handle(GuardarClienteRequest request, CancellationToken cancellationToken)
    {
        GuardarClienteResponse Resultado =
            request.ClienteId > 0 ?
            await ActualizarCliente(request) :
            await InsertarCliente(request);

        return Resultado;
    }

    private async Task<GuardarClienteResponse> InsertarCliente(GuardarClienteRequest request)
    {
        long estadoIdActivo = await _tablaDetalleRepository.ObtenerTablaDetalleId(Constants.TablaId_EstadosClientes, Constants.CodigoEstado_Cliente_Activo);
        Cliente entity = new()
        {
            ClienteId = 0,
            TipoId = request.TipoId,
            Identificacion = request.Identificacion,
            NombreCompleto = request.NombreCompleto,
            GeneroId = request.GeneroId,
            TelCel = request.TelCel,
            Direccion = request.Direccion,
            EstadoId = estadoIdActivo,
            UsuarioId = request.UsuarioId
        };

        var response = await _context.Cliente.AddAsync(entity);
        await _context.SaveChangesAsync();

        entity = response.Entity;

        GuardarClienteResponse Resultado = new()
        {
            ClienteId = entity.ClienteId,
            TipoId = entity.TipoId,
            Identificacion = entity.Identificacion,
            NombreCompleto = entity.NombreCompleto,
            GeneroId = entity.GeneroId,
            TelCel = entity.TelCel,
            Direccion = entity.Direccion,
            EstadoId = entity.EstadoId
        };
        return Resultado;
    }

    private async Task<GuardarClienteResponse> ActualizarCliente(GuardarClienteRequest request)
    {
        Cliente entity = await _context.Cliente.FirstAsync(x => x.ClienteId == request.ClienteId);
        entity.TipoId = request.TipoId;
        entity.Identificacion = request.Identificacion;
        entity.NombreCompleto = request.NombreCompleto;
        entity.GeneroId = request.GeneroId;
        entity.TelCel = request.TelCel;
        entity.Direccion = request.Direccion;

        await _context.SaveChangesAsync();

        GuardarClienteResponse Resultado = new()
        {
            ClienteId = entity.ClienteId,
            TipoId = entity.TipoId,
            Identificacion = entity.Identificacion,
            NombreCompleto = entity.NombreCompleto,
            GeneroId = entity.GeneroId,
            TelCel = entity.TelCel,
            Direccion = entity.Direccion,
            EstadoId = entity.EstadoId
        };
        return Resultado;
    }
}
