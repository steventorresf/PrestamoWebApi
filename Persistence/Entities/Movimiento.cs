namespace Persistence.Entities
{
    public class Movimiento
    {
        public long MovimientoId { get; set; }
        public long UsuarioId { get; set; }
        public long ClienteId { get; set; }
        public long? PrestamoId { get; set; }
        public long? PrestamoDetalleId { get; set; }
        public DateTime FechaPago { get; set; }
        public decimal Capital { get; set; }
        public decimal Intereses { get; set; }
        public long DescripcionId { get; set; }

#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.
        public Usuario Usuario { get; set; }
        public Cliente Cliente { get; set; }
        public Prestamo Prestamo { get; set; }
        public PrestamoDetalle PrestamoDetalle { get; set; }
        public TablaDetalle Descripcion { get; set; }
#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.
    }
}
