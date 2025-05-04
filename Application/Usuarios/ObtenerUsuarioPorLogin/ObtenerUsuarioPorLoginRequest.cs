using MediatR;

namespace Application.Usuarios.ObtenerUsuarioPorLogin;

public class ObtenerUsuarioPorLoginRequest : IRequest<ObtenerUsuarioPorLoginResponse>
{
    public string NombreUsuario { get; set; } = string.Empty;
    public string Contrasena { get; set; } = string.Empty;
}
