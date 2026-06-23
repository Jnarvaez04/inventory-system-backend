using inventarySystem_backend.domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace inventarySystem_backend.infrastructure.Data;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }


    public DbSet<User> Users => Set<User>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<InventoryTransaction> InventoryTransactions => Set<InventoryTransaction>();


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>(entity =>
        {
            // Se especifica precisión para monton monetarios y evitar problemas de redondeo
            entity.Property(p => p.Price).HasColumnType("decimal(18,2)");

            // Garantiza que el SKU sea unico en la BD
            entity.HasIndex(p => p.SKU).IsUnique();            
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(u => u.Email).IsUnique();
            entity.HasIndex(u => u.Username).IsUnique();
        });
    }  
}
