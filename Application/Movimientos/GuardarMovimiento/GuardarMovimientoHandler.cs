using MediatR;
using Persistence;
using Persistence.Entities;
using Persistence.Interfaces;
using Persistence.Utilities;

namespace Application.Movimientos.GuardarMovimiento;

public class GuardarMovimientoHandler : IRequestHandler<GuardarMovimientoRequest, bool>
{
    private readonly BaseContext _context;
    private readonly ITablaDetalleRepository _tablaDetalleRepository;

    public GuardarMovimientoHandler(BaseContext context, ITablaDetalleRepository tablaDetalleRepository)
    {
        this._context = context;
        this._tablaDetalleRepository = tablaDetalleRepository;
    }

    public async Task<bool> Handle(GuardarMovimientoRequest request, CancellationToken cancellationToken)
    {
        long descripcionId = await _tablaDetalleRepository.ObtenerTablaDetalleId(Constants.TablaId_DescripcionesMovimientos, Constants.CodigoDescripcion_Movimiento_AC);
        Movimiento movimiento = new()
        {
            UsuarioId = request.UsuarioId,
            ClienteId = request.ClienteId,
            PrestamoId = request.PrestamoId,
            FechaPago = Convert.ToDateTime(request.FechaPago),
            Capital = request.Capital,
            Intereses = request.Intereses,
            DescripcionId = descripcionId
        };

        await _context.Movimiento.AddAsync(movimiento);
        await _context.SaveChangesAsync();
        
        return true;
    }
}
