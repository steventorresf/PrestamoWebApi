using Persistence.Entities;

namespace Persistence
{
    public interface ITablasDetalleRepository
    {
        Task<IEnumerable<TablaDetalle>> GetTablaDetalle(long tablaId);
    }
}
