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
        public async Task<ActionResult> ObtenerUsuarioPorLogin([FromBody] LoginRequest request)
        {
            try
            {
                LoginResultDTO? result = await _service.ObtenerUsuarioPorLogin(request);
                if(result == null)
                {
                    var response = new ResponseData<LoginResultDTO>(HttpStatusCode.OK, "No existe un usuario con esas credenciales");
                    return StatusCode((int)response.StatusCode, response);
                }
                else
                {
                    var response = new ResponseData<LoginResultDTO>(result);
                    return StatusCode((int)response.StatusCode, response);
                }
            }
            catch (Exception ex)
            {
                var response = new ResponseData<LoginResultDTO>(HttpStatusCode.InternalServerError, ex.Message);
                return StatusCode((int)response.StatusCode, response);
            }
        }
    }
}
