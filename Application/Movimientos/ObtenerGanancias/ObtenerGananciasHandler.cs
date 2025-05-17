using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Entities;
using Persistence.Utilities;

namespace Application.Movimientos.ObtenerGanancias;

public class ObtenerGananciasHandler : IRequestHandler<ObtenerGananciasRequest, List<ObtenerGananciasResponse>>
{
    private readonly BaseContext _context;

    public ObtenerGananciasHandler(BaseContext context)
    {
        this._context = context;
    }

    public async Task<List<ObtenerGananciasResponse>> Handle(ObtenerGananciasRequest request, CancellationToken cancellationToken)
    {
        string[] estados = new string[]
        {
            Constants.CodigoEstado_Prestamo_Pendiente,
            Constants.CodigoEstado_Prestamo_Finalizado,
            Constants.CodigoEstado_Prestamo_Congelado
        };

        IQueryable<Movimiento> movimientos =
            _context.Movimiento
            .Include(x => x.Cliente)
            .Include(x => x.Prestamo).ThenInclude(x => x.Estado)
            .Where(x => x.UsuarioId == request.UsuarioId &&
                        x.FechaPago >= Convert.ToDateTime(request.FechaInicio) &&
                        x.FechaPago <= Convert.ToDateTime(request.FechaFin) &&
                        estados.Contains(x.Prestamo.Estado.Codigo));

        List<ObtenerGananciasResponse> Resultado =
            await movimientos
            .Select(x => new ObtenerGananciasResponse
            {
                FechaPago = x.FechaPago,
                NombreCliente = x.Cliente.NombreCompleto,
                Descripcion = x.Descripcion.Descripcion,
                Capital = x.Capital,
                Intereses = x.Intereses,
                ValorTotal = x.Capital + x.Intereses,
            }).OrderBy(x => x.FechaPago).ToListAsync();

        return Resultado;
    }
}
