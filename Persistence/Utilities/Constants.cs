namespace Persistence.Utilities;

public class Constants
{
    public const long TablaId_EstadosPrestamos = 2;
    public const long TablaId_DescripcionesMovimientos = 2;
    public const long TablaId_PeriodosPrestamos = 2;

    public const string CodigoEstado_Prestamo_Finalizado = "";
    public const string CodigoEstado_Prestamo_Pendiente = "";
    public const string CodigoEstado_Prestamo_Anulado = "";

    public const string CodigoPeriodo_PorAbonos = "";
    public const string CodigoPeriodo_Quincenal = "";
    public const string CodigoPeriodo_Semanal = "";
    public const string CodigoPeriodo_Mensual = "";

    public const string CodigoDescripcion_Movimiento_FinPrestamo = "FP";

    // Meses 31
    public static string[] Meses31 = new string[] { "1", "3", "5", "7", "8", "10", "12" };
}
