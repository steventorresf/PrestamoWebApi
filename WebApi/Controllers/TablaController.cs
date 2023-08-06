using Application;
using Domain.DTO;
using Domain.Response;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TablaController : ControllerBase
    {
        private readonly ITablaService _tablaService;

        public TablaController(ITablaService tablaService)
        {
            _tablaService = tablaService;
        }

        [HttpGet("by-codigo/{codigo}")]
        public async Task<ActionResult<ResponseData<TablaDTO>>> GetByCodigo(string codigo)
        {
            var response = await _tablaService.GetTablaDetallePorCodigo(codigo);
            return new ResponseData<TablaDTO>(response, "Registros Ok");
        }
    }
}
