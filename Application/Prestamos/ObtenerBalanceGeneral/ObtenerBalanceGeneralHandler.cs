using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Utilities;

namespace Application.Prestamos.ObtenerBalanceGeneral;

public class ObtenerBalanceGeneralHandler : IRequestHandler<ObtenerBalanceGeneralRequest, ObtenerBalanceGeneralResponse>
{
    private readonly BaseContext _context;

    public ObtenerBalanceGeneralHandler(BaseContext context)
    {
        this._context = context;
    }

    public async Task<ObtenerBalanceGeneralResponse> Handle(ObtenerBalanceGeneralRequest request, CancellationToken cancellationToken)
    {
        DateTime fechaInicial = Convert.ToDateTime(request.FechaInicial);
        DateTime fechaFinal = Convert.ToDateTime(request.FechaFinal);
        string[] estados = new string[]
        {
            Constants.CodigoEstado_Prestamo_Pendiente,
            Constants.CodigoEstado_Prestamo_Finalizado,
            Constants.CodigoEstado_Prestamo_Congelado
        };

        decimal totalRecogido =
            await _context.Movimiento
            .Include(x => x.Prestamo).ThenInclude(x => x.Estado)
            .Where(x => x.UsuarioId == request.UsuarioId &&
                        estados.Contains(x.Prestamo.Estado.Codigo) &&
                        x.FechaPago >= fechaInicial &&
                        x.FechaPago <= fechaFinal)
            .SumAsync(x => x.Capital + x.Intereses);

        decimal totalPrestado =
            await _context.Prestamo
            .Include(x => x.Cliente)
            .Include(x => x.Estado)
            .Where(x => x.Cliente.UsuarioId == request.UsuarioId &&
                        estados.Contains(x.Estado.Codigo) &&
                        x.FechaPrestamo >= fechaInicial &&
                        x.FechaPrestamo <= fechaFinal)
            .SumAsync(x => x.ValorPrestamo);

        return new ObtenerBalanceGeneralResponse
        {
            TotalRecogido = totalRecogido,
            TotalPrestado = totalPrestado,
            Total = totalRecogido + totalPrestado
        };
    }
}
