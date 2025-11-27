# PatientManagementMicroservice

Gestion de pacientes, citas y registros de visita.

## Tecnologias
- ASP.NET Core 8 Web API
- Entity Framework Core InMemory (opcional SQL Server)
- JWT Bearer
- Swagger

## Configuracion
- Base de datos: UseInMemoryDatabase("PatientManagementDb") por defecto. Para SQL Server, habilitar UseSqlServer en Program.cs y agregar cadena de conexion en appsettings.
- JWT: lee `Jwt:SecretKey`, `Jwt:Issuer`, `Jwt:Audience` o usa valores por defecto (ClinicalManagement + clave larga). Deben coincidir con AuthenticationMicroservice.
- Semilla: dos pacientes con informacion de contacto y tres citas cada uno; se imprime mensaje en consola.

## Ejecucion
- `dotnet run --project PatientManagementMicroservice`
- URLs: http://localhost:5156 y https://localhost:7188. Swagger en `/swagger`.

## Endpoints principales
- Pacientes (Rol Medico): `POST /api/patients`, `GET /api/patients`, `GET /api/patients/{numeroIdentificacion}`, `GET /api/patients/by-id/{id}`, `PUT /api/patients/{numeroIdentificacion}`, `DELETE /api/patients/{numeroIdentificacion}`.
- Citas (Rol Medico o PersonalAdministrativo): `POST/GET/PUT/DELETE /api/patients/{numeroIdentificacion}/appointments/{appointmentId?}`; acceso directo `GET` y `PUT /api/patients/appointments/{appointmentId}`.
- Registros de visita (Rol Enfermero para crear/editar; Enfermero o Medico para leer): `POST/PUT/GET /api/patients/{numeroIdentificacion}/visits` y `GET /api/patients/{numeroIdentificacion}/visits/{visitId}`.
