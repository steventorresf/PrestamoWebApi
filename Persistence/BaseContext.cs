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
            
        }

        #region Definicion de Dbset

        public virtual DbSet<DiaNoHabil> DiaNoHabil { get; set; }
        public virtual DbSet<Prestamo> Prestamo { get; set; }
        public virtual DbSet<PrestamoDetalle> PrestamoDetalle { get; set; }
        public virtual DbSet<SystemError> SystemError { get; set; }
        public virtual DbSet<TablaDetalle> TablaDetalle { get; set; }
        #endregion
    }
}
