namespace Persistence.Entities
{
    public class Usuario
    {
        public long UsuarioId { get; set; }
        public string NombreCompleto { get; set; } = string.Empty;
        public string NombreUsuario { get; set; } = string.Empty;
        public string Clave { get; set; } = string.Empty;
        public string Avatar { get; set; } = string.Empty;

        public IEnumerable<Cliente> Clientes { get; set; } = new List<Cliente>();
        public IEnumerable<Movimiento> Movimientos { get; set; } = new List<Movimiento>();
    }
}
