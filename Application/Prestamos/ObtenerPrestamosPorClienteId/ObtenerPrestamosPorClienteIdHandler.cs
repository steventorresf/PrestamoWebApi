using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Entities;
using Persistence.Utilities;

namespace Application.Prestamos.ObtenerPrestamosPorClienteId;

public class ObtenerPrestamosPorClienteIdHandler : IRequestHandler<ObtenerPrestamosPorClienteIdRequest, List<ObtenerPrestamosPorClienteIdResponse>>
{
    private readonly BaseContext _context;

    public ObtenerPrestamosPorClienteIdHandler(BaseContext context)
    {
        this._context = context;
    }

    public async Task<List<ObtenerPrestamosPorClienteIdResponse>> Handle(ObtenerPrestamosPorClienteIdRequest request, CancellationToken cancellationToken)
    {
        string[] codigos = new string[]
        {
            Constants.CodigoEstado_Prestamo_Pendiente,
            Constants.CodigoEstado_Prestamo_Finalizado
        };

        IQueryable<Prestamo> prestamos = _context.Prestamo
                    .Include(p => p.Periodo)
                    .Include(e => e.Estado)
                    .Where(x => x.ClienteId == request.ClienteId &&
                                codigos.Contains(x.Estado.Codigo))
                    .OrderByDescending(x => x.PrestamoId);

        List<ObtenerPrestamosPorClienteIdResponse> Resultado =
            await prestamos.Select(x => new ObtenerPrestamosPorClienteIdResponse
            {
                PrestamoId = x.PrestamoId,
                ClienteId = x.ClienteId,
                Dias = x.Dias,
                EstadoId = x.EstadoId,
                FechaAnulado = x.FechaAnulado,
                FechaInicio = x.FechaInicio,
                FechaPrestamo = x.FechaPrestamo,
                NoCuotas = x.NoCuotas,
                PeriodoId = x.PeriodoId,
                Porcentaje = x.Porcentaje,
                ValorPrestamo = x.ValorPrestamo,
                Periodo = x.Periodo.Descripcion,
                Estado = x.Estado.Descripcion,
                ValorTotal = x.ValorPrestamo + ((x.ValorPrestamo * x.Porcentaje / 100) / 30 * x.Dias),
            }).ToListAsync();
        
        return Resultado;
    }
}
