using Application;
using Domain.DTO;
using Domain.Response;
using Microsoft.AspNetCore.Mvc;

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
            var response = await _prestamoService.GetPrestamosByClienteId(clienteId);
            return new ResponseData<ResponseListItem<PrestamoDTO>>(response, "Registros Ok");
        }
    }
}
