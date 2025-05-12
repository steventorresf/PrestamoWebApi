using MediatR;
using Persistence;
using Persistence.Entities;
using Persistence.Interfaces;
using Persistence.Utilities;

namespace Application.Prestamos.CrearPrestamo
{
    public class CrearPrestamoHandler : IRequestHandler<CrearPrestamoRequest, bool>
    {
        private readonly BaseContext _context;
        private readonly ITablaDetalleRepository _tablaDetalleRepository;

        public CrearPrestamoHandler(BaseContext context, ITablaDetalleRepository tablaDetalleRepository)
        {
            this._context = context;
            this._tablaDetalleRepository = tablaDetalleRepository;
        }

        public async Task<bool> Handle(CrearPrestamoRequest request, CancellationToken cancellationToken)
        {
            long estadoIdPendientePrestamo = await _tablaDetalleRepository.ObtenerTablaDetalleId(Constants.TablaId_EstadosPrestamos, Constants.CodigoEstado_Prestamo_Pendiente);
            Prestamo entity = new()
            {
                PrestamoId = 0,
                ClienteId = request.ClienteId,
                PeriodoId = request.PeriodoId,
                Dias = request.Dias,
                EstadoId = estadoIdPendientePrestamo,
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
