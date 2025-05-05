namespace Application.TablaDetalles.ObtenerTablaDetallesPorCodigos;

public class ObtenerTablaDetallesPorCodigosResponse
{
    public long TablaId { get; set; }
    public List<TablaDetalleDTO> Listado { get; set; } = new();
}

public class TablaDetalleDTO
{
    public long TablaDetalleId { get; set; }
    public string Codigo { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
}
