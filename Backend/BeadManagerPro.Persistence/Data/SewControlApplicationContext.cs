using Microsoft.EntityFrameworkCore;
using BeadManagerPro.Domain.Entities.Usuarios;
using BeadManagerPro.Domain.Entities.Encargos;

namespace BeadManagerPro.Persistence.Data;

public class BeadManagerProContext : DbContext
{
    public BeadManagerProContext(DbContextOptions<BeadManagerProContext> options) : base(options) { }

    // --- TUS TABLAS DE BISUTERÍA (Coinciden con tus clases) ---
    public DbSet<Material> Materiales => Set<Material>();
    public DbSet<Pieza> Piezas => Set<Pieza>();
    public DbSet<Pedido> Pedidos => Set<Pedido>();
    public DbSet<Artesana> Artesanas => Set<Artesana>();
    public DbSet<Cliente> Clientes => Set<Cliente>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuración para MATERIALES
        modelBuilder.Entity<Material>(e =>
        {
            e.HasKey(m => m.Id);
            e.Property(m => m.Nombre).IsRequired().HasMaxLength(100);
            // Corregido: Sin espacio en el nombre de la propiedad y configurado como decimal
            e.Property(m => m.CostoPorUnidad).HasColumnType("decimal(10,2)");
            e.HasQueryFilter(m => !m.IsDeleted);
        });

        // Configuración para PIEZAS (Collares, etc.)
        modelBuilder.Entity<Pieza>(e =>
        {
            e.HasKey(p => p.Id);
            e.Property(p => p.Nombre).IsRequired().HasMaxLength(100);
            e.Property(p => p.PrecioVenta).HasColumnType("decimal(10,2)");
            e.HasQueryFilter(p => !p.IsDeleted);
        });

        // Configuración para ARTESANAS
        modelBuilder.Entity<Artesana>(e =>
        {
            e.HasKey(a => a.Id);
            e.Property(a => a.Nombre).IsRequired().HasMaxLength(100);
            e.HasQueryFilter(a => !a.IsDeleted);
        });

        // Configuración para CLIENTES
        modelBuilder.Entity<Cliente>(e =>
        {
            e.HasKey(c => c.Id);
            e.Property(c => c.Nombre).IsRequired().HasMaxLength(100);
            e.Property(c => c.Telefono).HasMaxLength(20);
            e.HasQueryFilter(c => !c.IsDeleted);
        });

        // Configuración para PEDIDOS
        modelBuilder.Entity<Pedido>(e =>
        {
            e.HasKey(pe => pe.Id);
            e.Property(pe => pe.TotalPagar).HasColumnType("decimal(10,2)");
            e.HasQueryFilter(pe => !pe.IsDeleted);

            // Relación con Cliente
            e.HasOne(pe => pe.Cliente)
             .WithMany() // Si el cliente no tiene lista de pedidos, se deja vacío
             .HasForeignKey(pe => pe.ClienteId);

            // Relación con Artesana
            e.HasOne(pe => pe.Artesana)
             .WithMany(a => a.Pedidos)
             .HasForeignKey(pe => pe.ArtesanaId);
        });
    }
}