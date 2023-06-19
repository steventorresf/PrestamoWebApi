using Domain.DTO;
using Domain.Request;
using Domain.Response;

namespace Application
{
    public interface IUsuarioService
    {
        Task<ResponseData<LoginResultDTO>> ObtenerUsuarioPorLogin(LoginRequest request);
    }
}
