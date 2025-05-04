namespace Application.Usuarios.ObtenerUsuarioPorLogin;

public class ObtenerUsuarioPorLoginResponse
{
    public long UsuarioId { get; set; }
    public string NombreCompleto { get; set; } = string.Empty;
    public string NombreUsuario { get; set; } = string.Empty;
    public string Avatar { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
    public DateTime FechaExpiracion { get; set; }
}
