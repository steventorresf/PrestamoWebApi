using Persistence.Entities;

namespace Persistence.Interfaces;

public interface ITablaDetalleRepository
{
    public Task<List<TablaDetalle>> ObtenerTablaDetalles(long tablaId);
    public Task<long> ObtenerTablaDetalleId(long tablaId, string codigo);
    public long ObtenerTablaDetalleId(List<TablaDetalle> lista, string codigo, long tablaId);
}
