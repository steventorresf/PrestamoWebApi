using Domain.DTO;
using Domain.Response;
using Persistence;
using Persistence.Entities;

namespace Application.Implementations
{
    public class PrestamoService : IPrestamoService
    {
        private readonly IPrestamoRepository _prestamoRepository;

        public PrestamoService(IPrestamoRepository prestamoRepository)
        {
            _prestamoRepository = prestamoRepository;
        }

        public async Task<ResponseListItem<PrestamoDTO>> GetPrestamosByClienteId(long clienteId)
        {
            List<Prestamo> lista = await _prestamoRepository.GetPrestamosByClienteId(clienteId);
            ResponseListItem<PrestamoDTO> list = new()
            {
                CountItems = Convert.ToInt64(lista.Count),
                ListItems = lista.Select(x => new PrestamoDTO
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
                    Periodo = x.Periodo?.Descripcion ?? string.Empty,
                    Estado = x.Estado?.Descripcion ?? string.Empty,
                    ValorTotal = x.ValorPrestamo + ((x.ValorPrestamo * x.Porcentaje / 100) / 30 * x.Dias),
                })
            };
            return list;
        }
    }
}
