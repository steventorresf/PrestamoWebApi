using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Entities;

namespace Application.DiasNoHabiles.ObtenerDiasNoHabilesPorUsuario;

public class ObtenerDiasNoHabilesPorUsuarioHandler : IRequestHandler<ObtenerDiasNoHabilesPorUsuarioRequest, List<ObtenerDiasNoHabilesPorUsuarioResponse>>
{
    private readonly BaseContext _context;

    public ObtenerDiasNoHabilesPorUsuarioHandler(BaseContext context)
    {
        this._context = context;
    }

    public async Task<List<ObtenerDiasNoHabilesPorUsuarioResponse>> Handle(ObtenerDiasNoHabilesPorUsuarioRequest request, CancellationToken cancellationToken)
    {
        DateTime fechaInicial = Convert.ToDateTime(string.Format("{0}-01-01", request.Anio));
        DateTime fechaFinal = Convert.ToDateTime(string.Format("{0}-12-31", request.Anio));

        IQueryable<DiaNoHabil> diasNoHabiles =
            _context.DiaNoHabil
            .Where(x => x.UsuarioId == request.UsuarioId &&
                        x.FechaDiaNoHabil >= fechaInicial &&
                        x.FechaDiaNoHabil <= fechaFinal);

        List<ObtenerDiasNoHabilesPorUsuarioResponse> Resultado =
            await diasNoHabiles
            .Select(x => new ObtenerDiasNoHabilesPorUsuarioResponse
            {
                FechaNoHabil = x.FechaDiaNoHabil,
                Anio = request.Anio,
                Mes = x.FechaDiaNoHabil.Month,
                Dia = x.FechaDiaNoHabil.Day
            }).ToListAsync();

        return Resultado;
    }
}