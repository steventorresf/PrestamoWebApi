using Persistence.Entities;

namespace Persistence
{
    public interface ITablasDetalleRepository
    {
        Task<List<TablaDetalle>> GetTablaDetalle(long tablaId);
    }
}
