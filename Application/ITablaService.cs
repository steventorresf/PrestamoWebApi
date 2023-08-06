using Domain.DTO;

namespace Application
{
    public interface ITablaService
    {
        Task<TablaDTO> GetTablaDetallePorCodigo(string codigo);
    }
}
