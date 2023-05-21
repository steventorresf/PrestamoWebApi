using Microsoft.EntityFrameworkCore;
using Persistence.Entities;

namespace Persistence
{
    public class BaseContext : DbContext
    {
        public BaseContext(DbContextOptions<BaseContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>()
                .HasOne(p => p.TipoIdentificacion)
                .WithMany(b => b.TiposIdentificacion)
                .HasForeignKey(p => p.TipoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Cliente>()
                .HasOne(p => p.Genero)
                .WithMany(b => b.Generos)
                .HasForeignKey(p => p.GeneroId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Cliente>()
                .HasOne(p => p.Estado)
                .WithMany(b => b.Estados)
                .HasForeignKey(p => p.EstadoId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        #region Definicion de Dbset

        public virtual DbSet<Cliente> Cliente { get; set; }
        public virtual DbSet<DiaNoHabil> DiaNoHabil { get; set; }
        public virtual DbSet<Prestamo> Prestamo { get; set; }
        public virtual DbSet<PrestamoDetalle> PrestamoDetalle { get; set; }
        public virtual DbSet<SystemError> SystemError { get; set; }
        public virtual DbSet<TablaDetalle> TablaDetalle { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
        #endregion
    }
}
