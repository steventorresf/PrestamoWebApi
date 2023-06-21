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
            var response = await _service.ObtenerUsuarioPorLogin(request);
            return new ResponseData<LoginResultDTO>(response, "Usuario logueado");
        }
    }
}
