using Application;
using Domain.DTO;
using Domain.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(string? textFilter, int pageNumber, int pageSize)
        {
            try
            {
                var result = await _clienteService.GetClientes(1, textFilter, pageNumber, pageSize);

                var response = new ResponseData<ResponseListItem<ClienteDTO>>(result);
                return StatusCode((int)response.StatusCode, response);
            }
            catch(Exception ex)
            {
                var response = new ResponseData<string>(HttpStatusCode.InternalServerError, ex.Message);
                return StatusCode((int)response.StatusCode, response);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostCliente([FromBody] ClienteRequestDTO request)
        {
            try
            {
                var result = await _clienteService.PostCliente(1, request);

                var response = new ResponseData<ClienteRequestDTO>(result);
                return StatusCode((int)response.StatusCode, response);
            }
            catch (Exception ex)
            {
                var response = new ResponseData<string>(HttpStatusCode.InternalServerError, ex.Message);
                return StatusCode((int)response.StatusCode, response);
            }
        }
    }
}
