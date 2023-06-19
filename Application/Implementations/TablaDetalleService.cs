using Domain.DTO;
using Domain.Response;
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

        public async Task<ResponseData<List<TablaDetalleItemDTO>>> GetTablaDetallePorCodigos(string codigos)
        {
            string[] arrayCodigos = codigos.Split(',', StringSplitOptions.RemoveEmptyEntries);
            List<TablaDetalleItemDTO> listado = new();
            foreach (string item in arrayCodigos)
            {
                long tablaId = 0;
                if(long.TryParse(item, out tablaId))
                {
                    List<TablaDetalle> lista = await _tablaDetalleRepository.GetTablaDetalle(tablaId);
                    listado.Add(new()
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
            return new ResponseData<List<TablaDetalleItemDTO>>(listado, "Operación realizada correctamente.");
        }

    }
}
