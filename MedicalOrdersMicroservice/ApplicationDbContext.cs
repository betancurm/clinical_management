using Microsoft.EntityFrameworkCore;
using MedicalOrdersMicroservice.Models;

namespace MedicalOrdersMicroservice;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<OrdenMedica> OrdenesMedicas { get; set; }
    public DbSet<OrdenMedicamentoDetalle> OrdenMedicamentoDetalles { get; set; }
    public DbSet<OrdenProcedimientoDetalle> OrdenProcedimientoDetalles { get; set; }
    public DbSet<OrdenAyudaDiagnosticaDetalle> OrdenAyudaDiagnosticaDetalles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configurar claves compuestas
        modelBuilder.Entity<OrdenMedicamentoDetalle>()
            .HasKey(omd => new { omd.NumeroOrden, omd.NumeroItem });

        modelBuilder.Entity<OrdenProcedimientoDetalle>()
            .HasKey(opd => new { opd.NumeroOrden, opd.NumeroItem });

        modelBuilder.Entity<OrdenAyudaDiagnosticaDetalle>()
            .HasKey(oad => new { oad.NumeroOrden, oad.NumeroItem });

        // Relaciones
        modelBuilder.Entity<OrdenMedicamentoDetalle>()
            .HasOne(omd => omd.OrdenMedica)
            .WithMany(om => om.Medicamentos)
            .HasForeignKey(omd => omd.NumeroOrden);

        modelBuilder.Entity<OrdenProcedimientoDetalle>()
            .HasOne(opd => opd.OrdenMedica)
            .WithMany(om => om.Procedimientos)
            .HasForeignKey(opd => opd.NumeroOrden);

        modelBuilder.Entity<OrdenAyudaDiagnosticaDetalle>()
            .HasOne(oad => oad.OrdenMedica)
            .WithMany(om => om.AyudasDiagnosticas)
            .HasForeignKey(oad => oad.NumeroOrden);

        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            // Configuración por defecto con InMemory para pruebas
            optionsBuilder.UseInMemoryDatabase("MedicalOrdersDb");

            // Para activar SQL Server, descomentar la línea siguiente y proporcionar la cadena de conexión
            // optionsBuilder.UseSqlServer("<cadena_de_conexión>");
        }
    }
}