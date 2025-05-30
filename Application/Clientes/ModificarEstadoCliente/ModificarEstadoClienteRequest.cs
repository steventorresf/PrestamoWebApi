﻿using MediatR;

namespace Application.Clientes.ModificarEstadoCliente;

public class ModificarEstadoClienteRequest : ModificarEstadoClienteDTO, IRequest<ModificarEstadoClienteResponse>
{
    public int UsuarioId {  get; set; }    
}

public class ModificarEstadoClienteDTO
{
    public long ClienteId { get; set; }
    public string CodigoEstado { get; set; } = string.Empty;
}
