namespace Domain.DTO
{
    public class ClienteRequestDTO
    {
        public long ClienteId { get; set; }
        public long UsuarioId { get; set; }
        public long TipoId { get; set; }
        public string Identificacion { get; set; } = string.Empty;
        public string NombreCompleto { get; set; } = string.Empty;
        public long GeneroId { get; set; }
        public string Direccion { get; set; } = string.Empty;
        public string TelCel { get; set; } = string.Empty;
        public long EstadoId { get; set; }
    }
}
