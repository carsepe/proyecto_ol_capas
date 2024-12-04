using Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Domain.Model
{
    public class ModelContext : DbContext
    {
        public ModelContext(DbContextOptions<ModelContext> options) : base(options) { }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Producto> Productos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de Cliente
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.ToTable("Clientes");
                entity.HasKey(c => c.Id);

                entity.Property(c => c.TipoIdentificacion)
                      .IsRequired()
                      .HasMaxLength(20);

                entity.Property(c => c.Identificacion)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(c => c.Nombre)
                      .IsRequired()
                      .HasMaxLength(150);

                entity.Property(c => c.Direccion)
                      .HasMaxLength(300);

                entity.Property(c => c.Telefono)
                      .HasMaxLength(20);

                entity.Property(c => c.FechaNacimiento)
                      .IsRequired();
            });

            // Configuración de Producto
            modelBuilder.Entity<Producto>(entity =>
            {
                entity.ToTable("Productos");
                entity.HasKey(p => p.Id);

                entity.Property(p => p.Codigo)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(p => p.NombreProducto)
                      .IsRequired()
                      .HasMaxLength(150);

                entity.Property(p => p.Descripcion)
                      .HasMaxLength(300);

                entity.Property(p => p.Stock)
                      .IsRequired();

                entity.Property(p => p.FechaVencimiento)
                      .IsRequired();
            });
        }
    }
}
