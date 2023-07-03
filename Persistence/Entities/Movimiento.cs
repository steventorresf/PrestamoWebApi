namespace Persistence.Entities
{
    public class Movimiento
    {
        public long MovimientoId { get; set; }
        public long UsuarioId { get; set; }
        public long ClienteId { get; set; }
        public long PrestamoId { get; set; }
        public long PrestamoDetalleId { get; set; }
        public DateTime FechaPago { get; set; }
        public decimal Capital { get; set; }
        public decimal Intereses { get; set; }
        public long DescripcionId { get; set; }

        public Prestamo? Prestamo { get; set; }
        public PrestamoDetalle? PrestamoDetalle { get; set; }
        public TablaDetalle? Descripcion { get; set; }
    }
}
