using Microsoft.EntityFrameworkCore;
using AuthenticationMicroservice.Models;

namespace AuthenticationMicroservice;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            // Configuración por defecto con InMemory para pruebas
            optionsBuilder.UseInMemoryDatabase("AuthenticationDb");

            // Para activar SQL Server, descomentar la línea siguiente y proporcionar la cadena de conexión
            // optionsBuilder.UseSqlServer("<cadena_de_conexión>");
        }
    }
}