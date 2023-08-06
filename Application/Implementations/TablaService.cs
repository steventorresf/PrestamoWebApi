using Domain.DTO;
using Entities;
using Persistence.Repositories;

namespace Application.Implementations
{
    public class TablaService : ITablaService
    {
        private readonly ITablaRepository _tablaRepository;

        public TablaService(ITablaRepository tablaRepository)
        {
            _tablaRepository = tablaRepository;
        }

        public async Task<TablaDTO> GetTablaDetallePorCodigo(string codigo)
        {
            Tabla tabla = await _tablaRepository.GetTablaByCodigo(codigo);
            return new TablaDTO
            {
                Id = tabla.Id,
                Codigo = tabla.Codigo,
                Descripcion = tabla.Descripcion,
                Detalle = tabla.Detalle
                .Where(x => x.Estado)
                .Select(x => new TablaDetalleDTO
                {
                    Id = x.Id,
                    Codigo = x.Codigo,
                    Descripcion = x.Descripcion
                }).ToList()
            };
        }

    }
}
