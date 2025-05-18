using MediatR;
using Persistence;
using Persistence.Entities;

namespace Application.PrestamosDetalles.GuardarPrestamoDetalle;

public class GuardarPrestamoDetalleHandler : IRequestHandler<GuardarPrestamoDetalleRequest, bool>
{
    private readonly BaseContext _context;

    public GuardarPrestamoDetalleHandler(BaseContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(GuardarPrestamoDetalleRequest request, CancellationToken cancellationToken)
    {
        PrestamoDetalle prestamoDetalle = new()
        {
            PrestamoId = request.PrestamoId,
            Capital = request.Capital,
            Intereses = request.Intereses,
            FechaCuota = Convert.ToDateTime(request.FechaCuota),
            AbonoCapital = 0,
            AbonoIntereses = 0,
            FechaPago = null,
            Pagado = false
        };

        await _context.PrestamoDetalle.AddAsync(prestamoDetalle);
        await _context.SaveChangesAsync();

        return true;
    }
}
