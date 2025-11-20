using Microsoft.EntityFrameworkCore;
using UserService.Data;
using UserService.Services;
using UserService.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configurar Entity Framework con base de datos en memoria
builder.Services.AddDbContext<UserDbContext>(options =>
    options.UseInMemoryDatabase("UserServiceDB"));

// Registrar servicios
builder.Services.AddScoped<IUserService, UserService.Services.UserService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Seed data para desarrollo
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<UserDbContext>();
    SeedDatabase(context);
}

app.Run();

static void SeedDatabase(UserDbContext context)
{
    // Limpiar datos existentes
    context.Users.RemoveRange(context.Users);
    context.SaveChanges();

    // Agregar usuarios de ejemplo
    var users = new[]
    {
        User.Builder.Crear()
            .ConNombreCompleto("Juan Carlos Pérez")
            .ConCedula("12345678")
            .ConCorreoElectronico("juan.perez@ejemplo.com")
            .ConNumeroTelefono("3001234567")
            .ConFechaNacimiento(new DateTime(1985, 5, 15))
            .ConDireccion("Calle 123 #45-67")
            .ConRol("Médico")
            .ConNombreUsuario("jperez")
            .ConContraseña("Password123!")
            .Construir(),
            
        User.Builder.Crear()
            .ConNombreCompleto("María González")
            .ConCedula("87654321")
            .ConCorreoElectronico("maria.gonzalez@ejemplo.com")
            .ConNumeroTelefono("3007654321")
            .ConFechaNacimiento(new DateTime(1990, 8, 22))
            .ConDireccion("Avenida 789 #12-34")
            .ConRol("Enfermera")
            .ConNombreUsuario("mgonzalez")
            .ConContraseña("SecurePass456!")
            .Construir(),
            
        User.Builder.Crear()
            .ConNombreCompleto("Pedro Rodríguez")
            .ConCedula("11223344")
            .ConCorreoElectronico("pedro.rodriguez@ejemplo.com")
            .ConNumeroTelefono("3001122334")
            .ConFechaNacimiento(new DateTime(1980, 12, 3))
            .ConDireccion("Carrera 456 #78-90")
            .ConRol("Personal Administrativo")
            .ConNombreUsuario("prodriguez")
            .ConContraseña("AdminPass789!")
            .Construir()
    };

    context.Users.AddRange(users);
    context.SaveChanges();
}
