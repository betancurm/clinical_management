using Microsoft.EntityFrameworkCore;
using PatientManagementMicroservice.Models;

namespace PatientManagementMicroservice;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Patient> Patients { get; set; }
    public DbSet<PatientExtraInfo> PatientExtraInfos { get; set; }
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<VisitRecord> VisitRecords { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configurar relación 1:1 Patient - PatientExtraInfo
        modelBuilder.Entity<PatientExtraInfo>()
            .HasKey(pe => pe.PatientId);

        modelBuilder.Entity<PatientExtraInfo>()
            .HasOne(pe => pe.Patient)
            .WithOne(p => p.ExtraInfo)
            .HasForeignKey<PatientExtraInfo>(pe => pe.PatientId);

        // Configurar relación 1:N Patient - Appointments
        modelBuilder.Entity<Appointment>()
            .HasOne(a => a.Patient)
            .WithMany(p => p.Appointments)
            .HasForeignKey(a => a.PatientId);

        // Configurar relación 1:N Patient - VisitRecords
        modelBuilder.Entity<VisitRecord>()
            .HasOne(v => v.Patient)
            .WithMany(p => p.VisitRecords)
            .HasForeignKey(v => v.PatientId);

        // Índice único en NumeroIdentificacion
        modelBuilder.Entity<Patient>()
            .HasIndex(p => p.NumeroIdentificacion)
            .IsUnique();

        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            // Configuración por defecto con InMemory para pruebas
            optionsBuilder.UseInMemoryDatabase("PatientManagementDb");

            // Para activar SQL Server, descomentar la línea siguiente y proporcionar la cadena de conexión
            // optionsBuilder.UseSqlServer("<cadena_de_conexión>");
        }
    }
}