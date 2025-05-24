using Domain.DTO;
using Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Persistence;
using Persistence.Entities;
using Persistence.Utilities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Usuarios.ObtenerUsuarioPorLogin;

public class ObtenerUsuarioPorLoginHandler : IRequestHandler<ObtenerUsuarioPorLoginRequest, ObtenerUsuarioPorLoginResponse>
{
    private readonly BaseContext _context;
    private readonly IOptions<AppSettings> _options;

    public ObtenerUsuarioPorLoginHandler(BaseContext context, IOptions<AppSettings> options)
    {
        this._context = context;
        _options = options;
    }

    public async Task<ObtenerUsuarioPorLoginResponse> Handle(ObtenerUsuarioPorLoginRequest request, CancellationToken cancellationToken)
    {
        Usuario usuario = await ObtenerUsuario(request);

        var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, usuario.NombreUsuario),
                new Claim(ClaimTypes.GivenName, usuario.NombreCompleto),
                new Claim(ClaimTypes.Sid, usuario.UsuarioId.ToString())
            };

        var symetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Value.Jwt.SigningKey));

        var signingCredentials = new SigningCredentials(symetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var header = new JwtHeader(signingCredentials);

        DateTime fechaExpiracion = DateTime.Now.AddMinutes(_options.Value.Jwt.ExpiredTimeMinutes);
        var payload = new JwtPayload(
            issuer: _options.Value.Jwt.Issuer,
            audience: _options.Value.Jwt.Audience,
            claims: authClaims,
            notBefore: DateTime.Now,
            expires: fechaExpiracion);

        var token = new JwtSecurityToken(header, payload);

        ObtenerUsuarioPorLoginResponse Resultado = new()
        {
            UsuarioId = usuario.UsuarioId,
            NombreCompleto = usuario.NombreCompleto,
            NombreUsuario = usuario.NombreUsuario,
            Avatar = usuario.Avatar,
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            FechaExpiracion = fechaExpiracion
        };

        return Resultado;
    }

    private async Task<Usuario> ObtenerUsuario(ObtenerUsuarioPorLoginRequest request)
    {
        Usuario? usuario = await _context.Usuario
                .FirstOrDefaultAsync(x => x.NombreUsuario.Equals(request.NombreUsuario));

        if (usuario == null)            
            throw new BadRequestException(Constants.MensajeUsuarioIncorrecto);

        bool usuarioValido = BCrypt.Net.BCrypt.Verify(request.Contrasena, usuario.Clave);
        if (!usuarioValido)
            throw new BadRequestException(Constants.MensajeUsuarioIncorrecto);

        return usuario;
    }
}
