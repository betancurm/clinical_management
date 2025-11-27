@echo off

REM === AuthenticationMicroservice ===
start "AuthenticationMicroservice" cmd /k "cd /d %~dp0AuthenticationMicroservice && dotnet watch run"

REM === HistoriaClinicaMicroservice ===
start "HistoriaClinicaMicroservice" cmd /k "cd /d %~dp0HistoriaClinicaMicroservice && dotnet watch run"

REM === InventoryMicroservice ===
start "InventoryMicroservice" cmd /k "cd /d %~dp0InventoryMicroservice && dotnet watch run"

REM === MedicalOrdersMicroservice ===
start "MedicalOrdersMicroservice" cmd /k "cd /d %~dp0MedicalOrdersMicroservice && dotnet watch run"

REM === PatientManagementMicroservice ===
start "PatientManagementMicroservice" cmd /k "cd /d %~dp0PatientManagementMicroservice && dotnet watch run"