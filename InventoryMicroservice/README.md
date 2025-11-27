# InventoryMicroservice

Gestion de medicamentos y lotes de inventario.

## Tecnologias
- ASP.NET Core 8 Web API
- Entity Framework Core InMemory (opcional SQL Server)
- JWT Bearer
- Swagger

## Configuracion
- Base de datos: UseInMemoryDatabase("InventoryDb") por defecto. Para SQL Server, habilitar UseSqlServer en Program.cs y agregar cadena de conexion.
- JWT: usa `Jwt:SecretKey`, `Jwt:Issuer`, `Jwt:Audience` o valores por defecto (ClinicalManagement + clave larga).
- Semilla: diez medicamentos y dos lotes por cada uno.

## Ejecucion
- `dotnet run --project InventoryMicroservice`
- URLs: http://localhost:5260 y https://localhost:7068. Swagger en `/swagger`.

## Endpoints principales
- Medicamentos (Roles SoporteTecnico para crear/actualizar/borrar; SoporteTecnico, Enfermero y Medico para leer):
  - `POST /api/medicamentos`
  - `GET /api/medicamentos` con soporte de filtros via query (MedicamentoFilter)
  - `GET /api/medicamentos/{id}`
  - `PUT /api/medicamentos/{id}`
  - `DELETE /api/medicamentos/{id}`
- Lotes (Rol SoporteTecnico para modificar; SoporteTecnico y Enfermero para leer y actualizar cantidad):
  - `POST /api/medicamentos/{medicamentoId}/lotes`
  - `GET /api/medicamentos/{medicamentoId}/lotes`
  - `GET /api/medicamentos/{medicamentoId}/lotes/{loteId}`
  - `PUT /api/medicamentos/{medicamentoId}/lotes/{loteId}`
  - `PATCH /api/medicamentos/{medicamentoId}/lotes/{loteId}/cantidad`
  - `DELETE /api/medicamentos/{medicamentoId}/lotes/{loteId}`
