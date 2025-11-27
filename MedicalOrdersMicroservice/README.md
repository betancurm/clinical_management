# MedicalOrdersMicroservice

Creacion y consulta de ordenes medicas (medicamentos, procedimientos, ayudas diagnosticas).

## Tecnologias
- ASP.NET Core 8 Web API
- Entity Framework Core InMemory (opcional SQL Server)
- JWT Bearer
- Swagger
- HttpClient para validar pacientes y citas

## Configuracion
- Base de datos: UseInMemoryDatabase("MedicalOrdersDb") por defecto. Para SQL Server, habilitar UseSqlServer en Program.cs y agregar cadena de conexion.
- JWT: issuer/audience `ClinicalManagement` y clave `MiClaveSecretaSuperLargaParaJWTQueDebeSerDeAlMenos32CaracteresParaSeguridad` en AuthConfiguration.cs (sin lectura de appsettings).
- Dependencias externas: PatientValidationService y AppointmentValidationService usan base URL `http://localhost:5156` para llamar a PatientManagementMicroservice. Ajustar si cambia el host.
- Roles: crear orden requiere rol Medico; consultas permiten Medico, Enfermero o PersonalAdministrativo segun el controller.

## Ejecucion
- `dotnet run --project MedicalOrdersMicroservice`
- URLs: http://localhost:5267 y https://localhost:7079. Swagger en `/swagger`.

## Endpoints principales
- `POST /api/ordenes` (Rol Medico): crea orden con listas de medicamentos, procedimientos y ayudas diagnosticas.
- `GET /api/ordenes/{numeroOrden}`: obtiene una orden por numero.
- `GET /api/ordenes/paciente/{cedulaPaciente}`: ordenes de un paciente.
- `GET /api/ordenes/medico/{cedulaMedico}`: ordenes emitidas por un medico.
- `GET /api/ordenes/cita/{citaId}`: ordenes asociadas a una cita.
- `POST /api/ordenes/batch`: obtiene un lote de ordenes enviando `{ "numeroOrdenes": ["..."] }`.
