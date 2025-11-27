# HistoriaClinicaMicroservice

Registro y consulta de atenciones clinicas persistidas en MongoDB.

## Tecnologias
- ASP.NET Core 8 Web API
- MongoDB Driver
- JWT Bearer
- Swagger

## Configuracion
- MongoDB: connection string y nombre de base en `appsettings.json` bajo `MongoDB:ConnectionString` y `MongoDB:DatabaseName` (por defecto mongodb://localhost:27017 y HistoriaClinicaDB).
- JWT: usa `Jwt:SecretKey`, `Jwt:Issuer`, `Jwt:Audience` con valores por defecto compatibles con los demas servicios.
- No hay semilla; los registros se crean via API.

## Ejecucion
- `dotnet run --project HistoriaClinicaMicroservice`
- URLs: http://localhost:5292 y https://localhost:7031. Swagger en `/swagger`.

## Endpoints principales
- `POST /api/HistoriaClinica/atenciones` (Rol Medico): registra una atencion.
- `GET /api/HistoriaClinica/paciente/{cedulaPaciente}` (Roles Medico,Enfermero): lista atenciones de un paciente.
- `GET /api/HistoriaClinica/paciente/{cedulaPaciente}/atencion?fechaAtencion=YYYY-MM-DD` (Roles Medico,Enfermero): obtiene la atencion de una fecha.
- `GET /api/HistoriaClinica/paciente/{cedulaPaciente}/rango?fechaInicio=...&fechaFin=...` (Roles Medico,Enfermero): atenciones dentro de un rango.
- `GET /api/HistoriaClinica/medico/{cedulaMedico}` (Roles Medico,Enfermero): atenciones registradas por un medico.
