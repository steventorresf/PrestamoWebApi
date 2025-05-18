namespace Persistence.Utilities;

public class Constants
{
    public const long TablaId_EstadosClientes = 1;
    public const string CodigoEstado_Cliente_Activo = "AC";
    public const string CodigoEstado_Cliente_Inactivo = "IN";
    public const string CodigoEstado_Cliente_Eliminado = "XX";

    public const long TablaId_EstadosPrestamos = 2;
    public const string CodigoEstado_Prestamo_Finalizado = "FI";
    public const string CodigoEstado_Prestamo_Pendiente = "PE";
    public const string CodigoEstado_Prestamo_Congelado = "CO";
    public const string CodigoEstado_Prestamo_Anulado = "AN";

    public const long TablaId_PeriodosPrestamos = 3;
    public const string CodigoPeriodo_PorAbonos = "P";
    public const string CodigoPeriodo_Quincenal = "Q";
    public const string CodigoPeriodo_Semanal = "S";
    public const string CodigoPeriodo_Mensual = "M";

    public const long TablaId_DescripcionesMovimientos = 2;
    public const string CodigoDescripcion_Movimiento_AbonoPrestamo = "AB";
    public const string CodigoDescripcion_Movimiento_FinPrestamo = "FP";
    public const string CodigoDescripcion_Movimiento_AC = "AC";
    public const string CodigoDescripcion_Movimiento_PagoTotalCuota = "PA";
    public const string CodigoDescripcion_Movimiento_ExcedenteCuota = "EX";

    // Meses 31
    public static string[] Meses31 = new string[] { "1", "3", "5", "7", "8", "10", "12" };
}
