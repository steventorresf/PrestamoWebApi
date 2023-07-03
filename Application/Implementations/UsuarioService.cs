using Domain.DTO;
using Domain.Exceptions;
using Domain.Request;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Persistence.Entities;
using Persistence.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Implementations
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _repository;
        private readonly IOptions<AppSettings> _options;

        public UsuarioService(IUsuarioRepository repository, IOptions<AppSettings> options)
        {
            _repository = repository;
            _options = options;
        }

        public async Task<LoginResultDTO> ObtenerUsuarioPorLogin(LoginRequest request)
        {
            //Usuario? usuario = await _repository.ObtenerUsuarioByLogin(request.UserName, request.Password);
            //if (usuario == null)
            //{
            //    List<string> errors = new List<string>() { "El nombre de usuario y/o contraseña son incorrectos." };
            //    throw new BadRequestException(JsonConvert.SerializeObject(errors));
            //}

            //var authClaims = new List<Claim>
            //{
            //    new Claim(ClaimTypes.Name, usuario.NombreUsuario),
            //    new Claim(ClaimTypes.GivenName, usuario.NombreCompleto),
            //    new Claim(ClaimTypes.Sid, usuario.UsuarioId.ToString())
            //};

            //var symetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Value.Jwt.SigningKey));

            //var signingCredentials = new SigningCredentials(symetricSecurityKey, SecurityAlgorithms.HmacSha256);

            //var header = new JwtHeader(signingCredentials);

            //DateTime fechaExpiracion = DateTime.Now.AddMinutes(_options.Value.Jwt.ExpiredTimeMinutes);
            //var payload = new JwtPayload(
            //    issuer: _options.Value.Jwt.Issuer,
            //    audience: _options.Value.Jwt.Audience,
            //    claims: authClaims,
            //    notBefore: DateTime.Now,
            //    expires: fechaExpiracion);

            //var token = new JwtSecurityToken(header, payload);

            //return new LoginResultDTO
            //{
            //    UsuarioId = usuario.UsuarioId,
            //    NombreCompleto = usuario.NombreCompleto,
            //    NombreUsuario = usuario.NombreUsuario,
            //    Avatar = usuario.Avatar,
            //    Token = new JwtSecurityTokenHandler().WriteToken(token),
            //    FechaExpiracion = fechaExpiracion
            //};
            return new LoginResultDTO();
        }
    }
}
