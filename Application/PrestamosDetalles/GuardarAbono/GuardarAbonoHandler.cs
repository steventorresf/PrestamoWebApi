using Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Entities;
using Persistence.Interfaces;
using Persistence.Utilities;

namespace Application.PrestamosDetalles.GuardarAbono;

public class GuardarAbonoHandler : IRequestHandler<GuardarAbonoRequest, bool>
{
    private readonly BaseContext _context;
    private readonly ITablaDetalleRepository _tablaDetalleRepository;

    public GuardarAbonoHandler(BaseContext context, ITablaDetalleRepository tablaDetalleRepository)
    {
        _context = context;
        this._tablaDetalleRepository = tablaDetalleRepository;
    }

    public async Task<bool> Handle(GuardarAbonoRequest request, CancellationToken cancellationToken)
    {
        PrestamoDetalle? prestamoDetalle =
            await _context.PrestamoDetalle
            .Include(x => x.Prestamo)
            .FirstOrDefaultAsync(x => x.PrestamoDetalleId == request.PrestamoDetalleId);
        if (prestamoDetalle == null)
            throw new BadRequestException("No existe un detalle con ese valor.");

        prestamoDetalle.FechaPago = Convert.ToDateTime(request.FechaPago);
        prestamoDetalle.AbonoCapital = request.AbonoCapital;
        prestamoDetalle.AbonoIntereses = request.AbonoIntereses;
        _context.PrestamoDetalle.Update(prestamoDetalle);

        long descripcionId = await _tablaDetalleRepository.ObtenerTablaDetalleId(
                    Constants.TablaId_DescripcionesMovimientos,
                    Constants.CodigoDescripcion_Movimiento_AbonoPrestamo);
        Movimiento movimiento = new()
        {
            UsuarioId = request.UsuarioId,
            ClienteId = prestamoDetalle.Prestamo.ClienteId,
            PrestamoId = prestamoDetalle.PrestamoId,
            PrestamoDetalleId = request.PrestamoDetalleId,
            Capital = request.AbonoCapital,
            Intereses = request.AbonoIntereses,
            FechaPago = Convert.ToDateTime(request.FechaPago),
            DescripcionId = descripcionId
        };
        await _context.Movimiento.AddAsync(movimiento);

        await _context.SaveChangesAsync();

        return true;
    }
}
