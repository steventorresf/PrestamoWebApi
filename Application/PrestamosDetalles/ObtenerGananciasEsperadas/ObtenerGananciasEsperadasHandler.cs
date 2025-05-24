using Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Entities;
using Persistence.Utilities;

namespace Application.PrestamosDetalles.ObtenerGananciasEsperadas;

public class ObtenerGananciasEsperadasHandler : IRequestHandler<ObtenerGananciasEsperadasRequest, List<ObtenerGananciasEsperadasResponse>>
{
    private readonly BaseContext _context;

    public ObtenerGananciasEsperadasHandler(BaseContext context)
    {
        this._context = context;
    }

    public async Task<List<ObtenerGananciasEsperadasResponse>> Handle(ObtenerGananciasEsperadasRequest request, CancellationToken cancellationToken)
    {
        if (!DateTime.TryParse(request.FechaInicial, out DateTime fechaInicial) ||
            !DateTime.TryParse(request.FechaFinal, out DateTime fechaFinal))
            throw new BadRequestException("La fecha inicial y/o fecha final son incorrectas.");

        IQueryable<PrestamoDetalle> prestamosDetalles =
            _context.PrestamoDetalle
            .Include(x => x.Prestamo).ThenInclude(x => x.Cliente)
            .Include(x => x.Prestamo).ThenInclude(x => x.Estado)
            .Where(x => x.Prestamo.Cliente.UsuarioId == request.UsuarioId &&
                        x.Prestamo.Estado.Codigo.Equals(Constants.CodigoEstado_Prestamo_Pendiente) &&
                        !x.Pagado && x.FechaCuota >= fechaInicial &&
                        x.FechaCuota <= fechaFinal)
            .OrderBy(x => x.FechaCuota);

        List<ObtenerGananciasEsperadasResponse> Resultado =
            await prestamosDetalles
            .Select(x => new ObtenerGananciasEsperadasResponse
            {
                FechaCuota = x.FechaCuota,
                NombreCliente = x.Prestamo.Cliente.NombreCompleto,
                Capital = x.Capital,
                Intereses = x.Intereses,
                ValorTotal = x.Capital - x.AbonoCapital + x.Intereses - x.AbonoIntereses
            }).ToListAsync();

        return Resultado;
    }
}
