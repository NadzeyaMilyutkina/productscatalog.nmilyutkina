using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProductsCatalog.Client.Data.Contracts;
using ProductsCatalog.Client.Domain;

namespace ProductsCatalog.Client.Data;

public class ApplicationDbContext : IdentityDbContext, IBaseDbContext
{
    public DbSet<Catalog>? Catalog { get; set; }
    public DbSet<Product>? Products { get; set; }
    public DbSet<ProductCategory>? ProductCategories { get; set; }

    public DbSet<Note>? Notes { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<TBaseEntity> GetDbSet<TBaseEntity>() where TBaseEntity : class
    {
        Console.WriteLine($"Call of GetDbSet from CatalogDbContext with type {typeof(TBaseEntity)}");

        return Set<TBaseEntity>();
    }

    public void SaveEntitiesChanges()
    {
        Console.WriteLine($"Call of SaveChanges from CatalogDbContext");

        SaveChanges();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Catalog>();
        modelBuilder.Entity<Note>();
        modelBuilder.Entity<ProductCategory>();
        modelBuilder.Entity<Product>();
    }
}