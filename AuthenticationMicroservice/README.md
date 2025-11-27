# AuthenticationMicroservice

API ASP.NET Core 8 para emitir tokens JWT y gestionar usuarios y roles.

## Tecnologias
- ASP.NET Core 8 Web API
- Entity Framework Core InMemory (opcional SQL Server)
- JWT Bearer
- Swagger

## Configuracion
- Base de datos: UseInMemoryDatabase("AuthenticationDb") por defecto. Para SQL Server, reemplazar por UseSqlServer y agregar la cadena de conexion en appsettings.Development.json.
- JWT: Issuer y Audience `ClinicalManagement`; clave secreta en Program.cs (`MiClaveSecretaSuperLargaParaJWTQueDebeSerDeAlMenos32CaracteresParaSeguridad`). Cambiarla en este servicio y en los demas para mantener compatibilidad.
- Semilla: al iniciar se crean usuarios por rol con contrasenas aleatorias y se muestran en consola.

## Ejecucion
- `dotnet run --project AuthenticationMicroservice`
- URLs por defecto: http://localhost:5077 y https://localhost:7299. Swagger disponible en `/swagger` en entorno Development.

## Endpoints
- `POST /api/auth/login` (AllowAnonymous): body `{"username":"...","password":"..."}` devuelve JWT.
- `POST /api/users` (Rol RecursosHumanos): crea usuario.
- `GET /api/users` (Rol RecursosHumanos): lista usuarios.
- `GET /api/users/{id}` (Rol RecursosHumanos): obtiene usuario por id.
- `DELETE /api/users/{id}` (Rol RecursosHumanos): elimina usuario.
