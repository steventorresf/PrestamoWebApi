using Application;
using Domain.DTO;
using Domain.Response;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebApi.Filters;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ServiceFilter(typeof(UserValidationFilter))]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseData<ResponseListItem<ClienteDTO>>>> GetAll(string? textFilter, int pageNumber, int pageSize)
        {
            try
            {
                int uid = Convert.ToInt32(HttpContext.Request.Headers["uid"]);
                var response = await _clienteService.GetClientes(uid, textFilter, pageNumber, pageSize);
                return StatusCode((int)response.StatusCode, response.StatusCode == HttpStatusCode.OK ? response : response.Message);
            }
            catch(Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<ResponseData<ClienteRequestDTO>>> PostCliente([FromBody] ClienteRequestDTO request)
        {
            try
            {
                var response = await _clienteService.PostCliente(request);
                return StatusCode((int)response.StatusCode, response.StatusCode == HttpStatusCode.OK ? response : response.Message);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
