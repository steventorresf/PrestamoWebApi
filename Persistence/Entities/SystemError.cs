namespace Persistence.Entities
{
    public class SystemError
    {
        public long SystemErrorId { get; set; }
        public long UsuarioId { get; set; }
        public string Metodo { get; set; } = string.Empty;
        public string MensajeError { get; set; } = string.Empty;
        public DateTime FechaError { get; set; }

        public Usuario? Usuario { get; set; }
    }
}
