using Application;
using Domain.DTO;
using Domain.Response;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TablaDetalleController : ControllerBase
    {
        private readonly ITablaDetalleService _tablaDetalleService;

        public TablaDetalleController(ITablaDetalleService tablaDetalleService)
        {
            _tablaDetalleService = tablaDetalleService;
        }

        [HttpGet("by-codigos")]
        public async Task<ActionResult<ResponseData<List<TablaDetalleItemDTO>>>> GetByCodigo(string codigos)
        {
            var response = await _tablaDetalleService.GetTablaDetallePorCodigos(codigos);
            return new ResponseData<List<TablaDetalleItemDTO>>(response, "Registros Ok");
        }
    }
}
