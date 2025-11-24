using Microsoft.EntityFrameworkCore;
using InventoryMicroservice.Models;

namespace InventoryMicroservice;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Medicamento> Medicamentos { get; set; }
    public DbSet<MedicamentoLote> MedicamentoLotes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configurar relación 1:N Medicamento - MedicamentoLote
        modelBuilder.Entity<MedicamentoLote>()
            .HasOne(ml => ml.Medicamento)
            .WithMany(m => m.Lotes)
            .HasForeignKey(ml => ml.MedicamentoId);

        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            // Configuración por defecto con InMemory para pruebas
            optionsBuilder.UseInMemoryDatabase("InventoryDb");

            // Para activar SQL Server, descomentar la línea siguiente y proporcionar la cadena de conexión
            // optionsBuilder.UseSqlServer("<cadena_de_conexión>");
        }
    }
}