namespace Persistence.Entities
{
    public class Prestamo
    {
        public long PrestamoId { get; set; }
        public long ClienteId { get; set; }
        public decimal ValorPrestamo { get; set; }
        public decimal Porcentaje { get; set; }
        public int Dias { get; set; }
        public DateTime FechaPrestamo { get; set; }
        public DateTime FechaInicio { get; set; }
        public int NoCuotas { get; set; }
        public long PeriodoId { get; set; }
        public long EstadoId { get; set; }
        public DateTime? FechaAnulado { get; set; }

        public Cliente? Cliente { get; set; }
        public TablaDetalle? Periodo { get; set; }
        public TablaDetalle? Estado { get; set; }
    }
}
