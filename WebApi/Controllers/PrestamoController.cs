using Application;
using Domain.DTO;
using Domain.Response;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrestamoController : ControllerBase
    {
        private readonly IPrestamoService _prestamoService;

        public PrestamoController(IPrestamoService prestamoService)
        {
            _prestamoService = prestamoService;
        }

        [HttpGet("{clienteId}")]
        public async Task<IActionResult> GetAll(long clienteId)
        {
            try
            {
                var result = await _prestamoService.GetPrestamosByClienteId(clienteId);

                var response = new ResponseData<IEnumerable<PrestamoDTO>>(result);
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
