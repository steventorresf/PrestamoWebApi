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
        public async Task<ActionResult<ResponseData<List<TablaDetalleItemDTO>>>> GetByCodigo(string codigos)
        {
            try
            {
                var response = await _tablaDetalleService.GetTablaDetallePorCodigos(codigos);
                return StatusCode((int)response.StatusCode, response.StatusCode == HttpStatusCode.OK ? response : response.Message);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
