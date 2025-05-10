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
        public DateTime? FechaPago { get; set; }
        public bool Pagado { get; set; }

#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.
        public Prestamo Prestamo { get; set; }
        public IEnumerable<Movimiento> Movimientos { get; set; }
#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.
    }
}
