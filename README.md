# ClinicalManagement

Solucion de microservicios en .NET 8 para gestionar autenticacion, pacientes, ordenes medicas, inventario y historia clinica.

## Microservicios
- AuthenticationMicroservice (http://localhost:5077): login JWT y gestion de usuarios y roles.
- PatientManagementMicroservice (http://localhost:5156): pacientes, citas y registros de visita.
- MedicalOrdersMicroservice (http://localhost:5267): ordenes medicas y validacion de pacientes/citas.
- InventoryMicroservice (http://localhost:5260): catalogo de medicamentos y lotes.
- HistoriaClinicaMicroservice (http://localhost:5292): atenciones clinicas almacenadas en MongoDB.

## Requisitos
- .NET 8 SDK
- MongoDB local para HistoriaClinicaMicroservice (connection string en appsettings.json).
- Opcional SQL Server si se cambia de InMemory a SQL Server en los microservicios relacionales.
- Una herramienta tipo curl o Postman para probar los endpoints.

## Autenticacion y JWT
- El emisor y audiencia por defecto es ClinicalManagement.
- La clave por defecto es `MiClaveSecretaSuperLargaParaJWTQueDebeSerDeAlMenos32CaracteresParaSeguridad`. Mantener el mismo valor en todos los servicios si se cambia.
- AuthenticationMicroservice contiene la clave en Program.cs; los demas leen `Jwt:SecretKey`, `Jwt:Issuer`, `Jwt:Audience` de appsettings con los mismos valores por defecto.

## Ejecucion rapida
1. `dotnet restore`
2. Levantar cada servicio en una terminal con `dotnet run --project <ruta_del_microservicio>`.
3. Abrir `/swagger` en cada puerto para explorar endpoints.
4. Autenticarse con `POST /api/auth/login` y usar el token JWT en el header `Authorization: Bearer <token>`.

## Dependencias cruzadas
- MedicalOrdersMicroservice valida pacientes y citas contra PatientManagementMicroservice usando base URL http://localhost:5156 (ajustar en los servicios PatientValidationService y AppointmentValidationService si el host cambia).
- HistoriaClinicaMicroservice requiere MongoDB activo.
- Todos los servicios esperan el mismo secreto/issuer/audience para validar JWT.

## Datos iniciales
- AuthenticationMicroservice: crea un usuario por rol (SoporteTecnico, Enfermero, Medico, PersonalAdministrativo, RecursosHumanos) con contrasenas aleatorias impresas en consola al iniciar.
- PatientManagementMicroservice: dos pacientes con informacion de contacto y tres citas cada uno.
- InventoryMicroservice: diez medicamentos con dos lotes cada uno.
- MedicalOrdersMicroservice e HistoriaClinicaMicroservice no incluyen semilla, los datos se crean via API.

## Flujo sugerido
1. Iniciar AuthenticationMicroservice y obtener un token para el rol requerido.
2. Crear o consultar pacientes en PatientManagementMicroservice.
3. Crear inventario en InventoryMicroservice si se requieren medicamentos.
4. Crear ordenes medicas en MedicalOrdersMicroservice.
5. Registrar atenciones en HistoriaClinicaMicroservice si se desea persistir la historia.
