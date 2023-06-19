using Domain.DTO;
using Domain.Response;

namespace Application
{
    public interface ITablaDetalleService
    {
        Task<ResponseData<List<TablaDetalleItemDTO>>> GetTablaDetallePorCodigos(string codigos);
    }
}
