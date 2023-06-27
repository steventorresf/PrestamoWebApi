using Domain.DTO;

namespace Application
{
    public interface ITablaDetalleService
    {
        Task<List<TablaDetalleItemDTO>> GetTablaDetallePorCodigos(string codigos);
    }
}
