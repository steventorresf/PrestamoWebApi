using Domain.DTO;
using Persistence;
using Persistence.Entities;

namespace Application.Implementations
{
    public class TablaDetalleService : ITablaDetalleService
    {
        private readonly ITablasDetalleRepository _tablaDetalleRepository;

        public TablaDetalleService(ITablasDetalleRepository tablaDetalleRepository)
        {
            _tablaDetalleRepository = tablaDetalleRepository;
        }

        public async Task<IEnumerable<TablaDetalleItemDTO>> GetTablaDetallePorCodigos(string codigos)
        {
            string[] arrayCodigos = codigos.Split(',', StringSplitOptions.RemoveEmptyEntries);
            IList<TablaDetalleItemDTO> response = new List<TablaDetalleItemDTO>();
            foreach (string item in arrayCodigos)
            {
                long tablaId = 0;
                if(long.TryParse(item, out tablaId))
                {
                    IEnumerable<TablaDetalle> lista = await _tablaDetalleRepository.GetTablaDetalle(tablaId);
                    response.Add(new()
                    {
                        TablaId = tablaId,
                        Listado = lista.Select(x => new TablaDetalleDTO()
                        {
                            TablaDetalleId = x.TablaDetalleId,
                            Codigo = x.Codigo,
                            Descripcion = x.Descripcion
                        })
                    });
                }
            }
            return response;
        }

    }
}
