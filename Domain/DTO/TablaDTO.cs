namespace Domain.DTO
{
    public class TablaDTO
    {
        public string Id { get; set; } = string.Empty;
        public string Codigo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public List<TablaDetalleDTO> Detalle { get; set; } = new();
    }

    public class TablaDetalleDTO
    {
        public string Id { get; set; } = string.Empty;
        public string Codigo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
    }
}
