namespace Application.Clientes.ObtenerClientes
{
    public class ObtenerClientesResponse
    {
        public long ClienteId { get; set; }
        public long TipoId { get; set; }
        public string TipoIdentificacion { get; set; } = string.Empty;
        public string Identificacion { get; set; } = string.Empty;
        public string NombreCompleto { get; set; } = string.Empty;
        public long GeneroId { get; set; }
        public string Genero { get; set; } = string.Empty;
        public string TelCel { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
    }
}
