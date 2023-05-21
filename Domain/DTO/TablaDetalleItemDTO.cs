namespace Domain.DTO
{
    public class TablaDetalleItemDTO
    {
        public long TablaId { get; set; }
        public IEnumerable<TablaDetalleDTO> Listado { get; set; } = new List<TablaDetalleDTO>();
    }
}
