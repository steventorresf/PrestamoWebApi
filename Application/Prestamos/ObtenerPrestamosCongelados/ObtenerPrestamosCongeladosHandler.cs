using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Entities;
using Persistence.Utilities;

namespace Application.Prestamos.ObtenerPrestamosCongelados;

public class ObtenerPrestamosCongeladosHandler : IRequestHandler<ObtenerPrestamosCongeladosRequest, List<ObtenerPrestamosCongeladosResponse>>
{
    private readonly BaseContext _context;

    public ObtenerPrestamosCongeladosHandler(BaseContext context)
    {
        this._context = context;
    }

    public async Task<List<ObtenerPrestamosCongeladosResponse>> Handle(ObtenerPrestamosCongeladosRequest request, CancellationToken cancellationToken)
    {
        IQueryable<Prestamo> prestamos = _context.Prestamo
                    .Include(x => x.Cliente)
                    .Include(x => x.Estado)
                    .Where(x => x.Cliente.UsuarioId == request.UsuarioId &&
                                x.Estado.Codigo.Equals(Constants.CodigoEstado_Prestamo_Congelado));

        List<ObtenerPrestamosCongeladosResponse> Resultado =
            await prestamos.Select(x => new ObtenerPrestamosCongeladosResponse
            {
                Id = x.PrestamoId,
                IdCliente = x.ClienteId,
                NomCliente = x.Cliente.NombreCompleto,
                FechaPrestamo = x.FechaPrestamo,
                FechaAnulado = x.FechaAnulado,
                ValorTotal = x.ValorPrestamo + ((x.ValorPrestamo * x.Porcentaje / 100) / 30 * x.Dias),
                ValorRestante = x.PrestamoDetalles.Where(x => !x.Pagado).Sum(x => x.Capital - x.AbonoCapital + x.Intereses - x.AbonoIntereses),
                ValorPagado = x.PrestamoDetalles.Sum(x => x.Pagado ? x.Capital + x.Intereses : x.AbonoCapital + x.AbonoIntereses)
            }).ToListAsync();

        return Resultado;
    }
}
