using Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Entities;

namespace Application.DiasNoHabiles.GuardarDiasNoHabiles;

public class GuardarDiasNoHabilesHandler : IRequestHandler<GuardarDiasNoHabilesRequest, bool>
{
    private readonly BaseContext _context;

    public GuardarDiasNoHabilesHandler(BaseContext context)
    {
        this._context = context;
    }

    public async Task<bool> Handle(GuardarDiasNoHabilesRequest request, CancellationToken cancellationToken)
    {
        List<DateTime> fechasNoHabiles = new();
        foreach(string s in request.Fechas)
        {
            if (DateTime.TryParse(s, out DateTime fechaNoHabil))
                fechasNoHabiles.Add(fechaNoHabil.Date);
            else
                throw new BadRequestException(string.Format("El valor {0} no tiene un formato de fecha correcto (yyyy-MM-dd)", s));
        }

        if (!fechasNoHabiles.Any() || fechasNoHabiles.Any(x => x.Year != request.Anio))
            throw new BadRequestException("Alguna(s) fecha(s) no coincide(n) con el año de entrada.");

        DateTime fechaInicial = Convert.ToDateTime(string.Format("{0}-01-01", request.Anio));
        DateTime fechaFinal = Convert.ToDateTime(string.Format("{0}-12-31", request.Anio));

        List<DiaNoHabil> diasNoHabilesBD =
            await _context.DiaNoHabil
            .Where(x => x.UsuarioId == request.UsuarioId &&
                        x.FechaDiaNoHabil >= fechaInicial &&
                        x.FechaDiaNoHabil <= fechaFinal)
            .ToListAsync();
        List<DateTime> fechasActuales = diasNoHabilesBD
            .Select(x => x.FechaDiaNoHabil).ToList();


        List<DiaNoHabil> listaRemove = diasNoHabilesBD
            .Where(x => !fechasNoHabiles.Contains(x.FechaDiaNoHabil)).ToList();

        List<DiaNoHabil> listaInserciones = fechasNoHabiles
            .Where(x => !fechasActuales.Contains(x))
            .Select(x => new DiaNoHabil
            {
                FechaDiaNoHabil = x,
                UsuarioId = request.UsuarioId
            }).ToList();

        _context.DiaNoHabil.RemoveRange(listaRemove);
        await _context.DiaNoHabil.AddRangeAsync(listaInserciones);
        await _context.SaveChangesAsync();

        return true;
    }
}
