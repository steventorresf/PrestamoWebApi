using MediatR;
using Persistence;
using Persistence.Entities;

namespace Application.Prestamos.CrearPrestamo
{
    public class CrearPrestamoHandler : IRequestHandler<CrearPrestamoRequest, bool>
    {
        private readonly BaseContext _context;

        public CrearPrestamoHandler(BaseContext context)
        {
            this._context = context;
        }

        public async Task<bool> Handle(CrearPrestamoRequest request, CancellationToken cancellationToken)
        {
            Prestamo entity = new()
            {
                PrestamoId = 0,
                ClienteId = request.ClienteId,
                PeriodoId = request.PeriodoId,
                Dias = request.Dias,
                EstadoId = request.EstadoId,
                FechaInicio = Convert.ToDateTime(request.FechaInicio),
                FechaPrestamo = Convert.ToDateTime(request.FechaPrestamo),
                NoCuotas = request.NoCuotas,
                ValorPrestamo = request.ValorPrestamo,
                Porcentaje = request.Porcentaje,
                PrestamoDetalles = request.PrestamoDetalle
                .Select(x => new PrestamoDetalle
                {
                    PrestamoDetalleId = 0,
                    Capital = x.Capital,
                    Intereses = x.Intereses,
                    FechaCuota = Convert.ToDateTime(x.FechaCuota)
                }).ToList()
            };

            await _context.Prestamo.AddAsync(entity);
            await _context.SaveChangesAsync();
            
            return true;
        }
    }
}
