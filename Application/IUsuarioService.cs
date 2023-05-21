using Domain.DTO;
using Domain.Request;

namespace Application
{
    public interface IUsuarioService
    {
        Task<LoginResultDTO?> ObtenerUsuarioPorLogin(LoginRequest request);
    }
}
