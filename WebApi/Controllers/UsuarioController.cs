using Application;
using Domain.DTO;
using Domain.Request;
using Domain.Response;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _service;

        public UsuarioController(IUsuarioService service)
        {
            _service = service;
        }

        [HttpPost("login")]
        public async Task<ActionResult<ResponseData<LoginResultDTO>>> ObtenerUsuarioPorLogin([FromBody] LoginRequest request)
        {
            try
            {
                var response = await _service.ObtenerUsuarioPorLogin(request);
                return StatusCode((int)response.StatusCode, response.StatusCode == HttpStatusCode.OK ? response : response.Message);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
