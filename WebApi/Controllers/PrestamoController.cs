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
        public async Task<ActionResult<ResponseData<ResponseListItem<PrestamoDTO>>>> GetAll(long clienteId)
        {
            try
            {
                var response = await _prestamoService.GetPrestamosByClienteId(clienteId);
                return StatusCode((int)response.StatusCode, response.StatusCode == HttpStatusCode.OK ? response : response.Message);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
