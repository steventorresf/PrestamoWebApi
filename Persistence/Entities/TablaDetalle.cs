namespace Persistence.Entities
{
    public class TablaDetalle
    {
        public long TablaDetalleId { get; set; }
        public long TablaId { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public int Orden { get; set; }
        public bool Visible { get; set; }

        public IEnumerable<Cliente> TiposIdentificacion { get; set; } = new List<Cliente>();
        public IEnumerable<Cliente> Generos { get; set; } = new List<Cliente>();
        public IEnumerable<Cliente> Estados { get; set; } = new List<Cliente>();
    }
}
