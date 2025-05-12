using MediatR;

namespace Application.Clientes.GuardarCliente;

public class GuardarClienteRequest : GuardarClienteDTO, IRequest<GuardarClienteResponse>
{
    public long UsuarioId { get; set; }
}

public class GuardarClienteDTO
{
    public long ClienteId { get; set; }
    public long TipoId { get; set; }
    public string Identificacion { get; set; } = string.Empty;
    public string NombreCompleto { get; set; } = string.Empty;
    public long GeneroId { get; set; }
    public string Direccion { get; set; } = string.Empty;
    public string TelCel { get; set; } = string.Empty;
}
