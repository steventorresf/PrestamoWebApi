using MediatR;

namespace Application.Prestamos.ObtenerBalanceGeneral;

public class ObtenerBalanceGeneralRequest : IRequest<ObtenerBalanceGeneralResponse>
{
    public int UsuarioId {  get; set; }
    public string FechaInicial { get; set; } = string.Empty;
    public string FechaFinal { get; set; } = string.Empty;
}