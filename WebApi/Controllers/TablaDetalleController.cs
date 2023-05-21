using Application;
using Domain.DTO;
using Domain.Response;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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
        public async Task<IActionResult> GetByCodigo(string codigos)
        {
            try
            {
                var result = await _tablaDetalleService.GetTablaDetallePorCodigos(codigos);

                var response = new ResponseData<IEnumerable<TablaDetalleItemDTO>>(result);
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
