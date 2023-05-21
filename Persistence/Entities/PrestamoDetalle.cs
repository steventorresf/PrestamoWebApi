namespace Persistence.Entities
{
    public class PrestamoDetalle
    {
        public long PrestamoDetalleId { get; set; }
        public long PrestamoId { get; set; }
        public decimal Capital { get; set; }
        public decimal Intereses { get; set; }
        public decimal AbonoCapital { get; set; }
        public decimal AbonoIntereses { get; set; }
        public DateTime FechaCuota { get; set; }
        public DateTime FechaPago { get; set; }
        public bool Pagado { get; set; }

        public Prestamo? Prestamo { get; set; }
    }
}
