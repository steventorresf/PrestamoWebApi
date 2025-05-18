using Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Entities;
using Persistence.Interfaces;
using Persistence.Utilities;

namespace Application.PrestamosDetalles.GuardarCobro;

public class GuardarCobroHandler : IRequestHandler<GuardarCobroRequest, bool>
{
    private readonly BaseContext _context;
    private readonly ITablaDetalleRepository _tablaDetalleRepository;

    public GuardarCobroHandler(BaseContext context, ITablaDetalleRepository tablaDetalleRepository)
    {
        _context = context;
        this._tablaDetalleRepository = tablaDetalleRepository;
    }

    public async Task<bool> Handle(GuardarCobroRequest request, CancellationToken cancellationToken)
    {
        PrestamoDetalle? prestamoDetalle =
            await _context.PrestamoDetalle
            .Include(x => x.Prestamo)
            .FirstOrDefaultAsync(x => x.PrestamoDetalleId == request.PrestamoDetalleId);

        if (prestamoDetalle == null)
            throw new BadRequestException("No existe un detalle con ese valor.");

        prestamoDetalle.FechaPago = Convert.ToDateTime(request.FechaPago);
        prestamoDetalle.AbonoCapital = 0;
        prestamoDetalle.AbonoIntereses = 0;
        prestamoDetalle.Pagado = true;

        if (!prestamoDetalle.Prestamo.PrestamoDetalles.Any(x => !x.Pagado))
        {
            long estadoId =
                await _tablaDetalleRepository.ObtenerTablaDetalleId(
                    Constants.TablaId_EstadosPrestamos,
                    Constants.CodigoEstado_Prestamo_Finalizado);
            prestamoDetalle.Prestamo.EstadoId = estadoId;
        }
        _context.PrestamoDetalle.Update(prestamoDetalle);


        long descripcionId =
            await _tablaDetalleRepository.ObtenerTablaDetalleId(
                Constants.TablaId_DescripcionesMovimientos,
                prestamoDetalle.AbonoIntereses == 0 ?
                Constants.CodigoDescripcion_Movimiento_PagoTotalCuota :
                Constants.CodigoDescripcion_Movimiento_ExcedenteCuota);
        Movimiento movimiento = new()
        {
            UsuarioId = request.UsuarioId,
            ClienteId = prestamoDetalle.Prestamo.ClienteId,
            PrestamoId = prestamoDetalle.PrestamoId,
            FechaPago = Convert.ToDateTime(request.FechaPago),
            Capital = prestamoDetalle.Capital,
            Intereses = prestamoDetalle.Intereses,
            DescripcionId = descripcionId
        };
        await _context.Movimiento.AddAsync(movimiento);
        
        await _context.SaveChangesAsync();

        return true;
    }
}
