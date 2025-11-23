using PatientManagementMicroservice.Models;

namespace PatientManagementMicroservice;

public static class Seeder
{
    public static void Seed(ApplicationDbContext context)
    {
        if (!context.Patients.Any())
        {
            // Paciente 1
            var patient1 = new Patient
            {
                Id = Guid.NewGuid(),
                NumeroIdentificacion = "123456789",
                NombreCompleto = "Juan Pérez López",
                FechaNacimiento = new DateTime(1985, 5, 15),
                Genero = Genero.Masculino,
                Direccion = "Calle 1 #10-20, Bogotá",
                NumeroTelefono = "3001111111",
                CorreoElectronico = "juan.perez@example.com"
            };

            var extraInfo1 = new PatientExtraInfo
            {
                PatientId = patient1.Id,
                PrimerNombreContacto = "María",
                PrimerApellidoContacto = "Gómez",
                RelacionConPaciente = "Madre",
                NumeroTelefonoEmergencia = "3002222222",
                NombreCompaniaSeguros = "Seguros XYZ",
                NumeroPoliza = "POL123456",
                PolizaActiva = true,
                VigenciaPoliza = new DateTime(2025, 12, 31)
            };

            patient1.ExtraInfo = extraInfo1;

            // Paciente 2
            var patient2 = new Patient
            {
                Id = Guid.NewGuid(),
                NumeroIdentificacion = "987654321",
                NombreCompleto = "Ana García Rodríguez",
                FechaNacimiento = new DateTime(1990, 8, 20),
                Genero = Genero.Femenino,
                Direccion = "Carrera 5 #15-30, Medellín",
                NumeroTelefono = "3003333333",
                CorreoElectronico = "ana.garcia@example.com"
            };

            var extraInfo2 = new PatientExtraInfo
            {
                PatientId = patient2.Id,
                PrimerNombreContacto = "Carlos",
                PrimerApellidoContacto = "Rodríguez",
                RelacionConPaciente = "Padre",
                NumeroTelefonoEmergencia = "3004444444",
                NombreCompaniaSeguros = "Aseguradora ABC",
                NumeroPoliza = "POL789012",
                PolizaActiva = true,
                VigenciaPoliza = new DateTime(2026, 6, 15)
            };

            patient2.ExtraInfo = extraInfo2;

            context.Patients.Add(patient1);
            context.Patients.Add(patient2);
            context.SaveChanges();

            Console.WriteLine("Seeder ejecutado: 2 pacientes agregados con información de contacto y póliza de seguro.");
        }
    }
}