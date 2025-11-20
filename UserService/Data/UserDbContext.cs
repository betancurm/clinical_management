using Microsoft.EntityFrameworkCore;
using UserService.Models;

namespace UserService.Data;

public class UserDbContext : DbContext
{
    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            
            entity.Property(e => e.NombreCompleto).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Cedula).IsRequired().HasMaxLength(15);
            entity.Property(e => e.CorreoElectronico).IsRequired().HasMaxLength(100);
            entity.Property(e => e.NumeroTelefono).HasMaxLength(10);
            entity.Property(e => e.Direccion).IsRequired().HasMaxLength(30);
            entity.Property(e => e.Rol).IsRequired().HasMaxLength(50);
            entity.Property(e => e.NombreUsuario).IsRequired().HasMaxLength(15);
            entity.Property(e => e.Contraseña).IsRequired();
            entity.Property(e => e.FechaCreacion).IsRequired();
            
            // Índices únicos
            entity.HasIndex(e => e.Cedula).IsUnique();
            entity.HasIndex(e => e.CorreoElectronico).IsUnique();
            entity.HasIndex(e => e.NombreUsuario).IsUnique();
        });
    }
}