﻿using Application;
using Domain.DTO;
using Domain.Response;
using Microsoft.AspNetCore.Mvc;
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
            int uid = Convert.ToInt32(HttpContext.Request.Headers["uid"]);
            var response = await _clienteService.GetClientes(uid, textFilter, pageNumber, pageSize);
            return new ResponseData<ResponseListItem<ClienteDTO>>(response, "Registros Ok.");
        }

        [HttpPost]
        public async Task<ActionResult<ResponseData<ClienteRequestDTO>>> PostCliente([FromBody] ClienteRequestDTO request)
        {
            var response = await _clienteService.PostCliente(request);
            return new ResponseData<ClienteRequestDTO>(response, "El cliente ha sido creado exitosamente.");
        }
    }
}
