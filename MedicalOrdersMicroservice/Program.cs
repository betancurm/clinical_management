using MedicalOrdersMicroservice;
using MedicalOrdersMicroservice.Configurations;
using MedicalOrdersMicroservice.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter JWT with Bearer into field",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
    {
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            }
        },
        new string[] { }
    }});
});

// Configurar DbContext con InMemory por defecto para pruebas
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseInMemoryDatabase("MedicalOrdersDb")
    // Para activar SQL Server, descomentar la línea siguiente y proporcionar la cadena de conexión
    // .UseSqlServer("<cadena_de_conexión>");
);

// Configurar autenticación JWT
builder.Services.AddJwtAuthentication();

// Registrar servicios
builder.Services.AddScoped<IOrdenMedicaService, OrdenMedicaService>();
builder.Services.AddHttpClient<IPatientValidationService, PatientValidationService>();
builder.Services.AddScoped<IPatientValidationService, PatientValidationService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
