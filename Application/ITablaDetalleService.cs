using Domain.DTO;

namespace Application
{
    public interface ITablaDetalleService
    {
        Task<IEnumerable<TablaDetalleItemDTO>> GetTablaDetallePorCodigos(IList<long> tablasIds);
    }
}
