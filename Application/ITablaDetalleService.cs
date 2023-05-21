using Domain.DTO;

namespace Application
{
    public interface ITablaDetalleService
    {
        Task<IEnumerable<TablaDetalleItemDTO>> GetTablaDetallePorCodigos(string codigos);
    }
}
