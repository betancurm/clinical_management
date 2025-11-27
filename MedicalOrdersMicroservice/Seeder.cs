using MedicalOrdersMicroservice.Models;

namespace MedicalOrdersMicroservice;

public static class Seeder
{
    public static void Seed(ApplicationDbContext context)
    {
        SeedProcedimientos(context);
        SeedAyudasDiagnosticas(context);
    }

    private static void SeedProcedimientos(ApplicationDbContext context)
    {
        if (!context.Procedimientos.Any())
        {
            var procedimientos = new List<Procedimiento>
            {
                new Procedimiento { Id = Guid.NewGuid(), Nombre = "Biopsia de piel", Descripcion = "Extracción de tejido cutáneo para análisis", DuracionEstimadaMinutos = 30, Costo = 150000, Activo = true, FechaCreacion = DateTime.Now },
                new Procedimiento { Id = Guid.NewGuid(), Nombre = "Cirugía de cataratas", Descripcion = "Extracción del cristalino opaco", DuracionEstimadaMinutos = 60, Costo = 2000000, Activo = true, FechaCreacion = DateTime.Now },
                new Procedimiento { Id = Guid.NewGuid(), Nombre = "Apendicectomía", Descripcion = "Extirpación del apéndice", DuracionEstimadaMinutos = 90, Costo = 3000000, Activo = true, FechaCreacion = DateTime.Now },
                new Procedimiento { Id = Guid.NewGuid(), Nombre = "Colocación de marcapasos", Descripcion = "Implante de dispositivo cardíaco", DuracionEstimadaMinutos = 120, Costo = 5000000, Activo = true, FechaCreacion = DateTime.Now },
                new Procedimiento { Id = Guid.NewGuid(), Nombre = "Artroscopia de rodilla", Descripcion = "Examen y reparación articular", DuracionEstimadaMinutos = 45, Costo = 1800000, Activo = true, FechaCreacion = DateTime.Now },
                new Procedimiento { Id = Guid.NewGuid(), Nombre = "Extracción dental", Descripcion = "Remoción de diente dañado", DuracionEstimadaMinutos = 20, Costo = 200000, Activo = true, FechaCreacion = DateTime.Now },
                new Procedimiento { Id = Guid.NewGuid(), Nombre = "Cesárea", Descripcion = "Parto por cirugía", DuracionEstimadaMinutos = 60, Costo = 2500000, Activo = true, FechaCreacion = DateTime.Now },
                new Procedimiento { Id = Guid.NewGuid(), Nombre = "Laparoscopia", Descripcion = "Cirugía mínimamente invasiva abdominal", DuracionEstimadaMinutos = 75, Costo = 2800000, Activo = true, FechaCreacion = DateTime.Now },
                new Procedimiento { Id = Guid.NewGuid(), Nombre = "Colostomía", Descripcion = "Creación de abertura intestinal", DuracionEstimadaMinutos = 90, Costo = 3200000, Activo = true, FechaCreacion = DateTime.Now },
                new Procedimiento { Id = Guid.NewGuid(), Nombre = "Traqueotomía", Descripcion = "Apertura quirúrgica de tráquea", DuracionEstimadaMinutos = 40, Costo = 1500000, Activo = true, FechaCreacion = DateTime.Now }
            };

            context.Procedimientos.AddRange(procedimientos);
            context.SaveChanges();
            Console.WriteLine("10 procedimientos médicos creados.");
        }
    }

    private static void SeedAyudasDiagnosticas(ApplicationDbContext context)
    {
        if (!context.AyudasDiagnosticas.Any())
        {
            var ayudas = new List<AyudaDiagnostica>
            {
                new AyudaDiagnostica { Id = Guid.NewGuid(), Nombre = "Rayos X de tórax", Descripcion = "Radiografía del pecho", Tipo = TipoAyudaDiagnostica.RayosX, Costo = 80000, RequierePreparacion = false, Activo = true, FechaCreacion = DateTime.Now },
                new AyudaDiagnostica { Id = Guid.NewGuid(), Nombre = "Ecografía abdominal", Descripcion = "Ultrasonido del abdomen", Tipo = TipoAyudaDiagnostica.Ecografia, Costo = 120000, RequierePreparacion = true, InstruccionesPreparacion = "Ayuno de 8 horas", Activo = true, FechaCreacion = DateTime.Now },
                new AyudaDiagnostica { Id = Guid.NewGuid(), Nombre = "Tomografía computarizada", Descripcion = "Escaneo detallado del cuerpo", Tipo = TipoAyudaDiagnostica.Tomografia, Costo = 350000, RequierePreparacion = false, Activo = true, FechaCreacion = DateTime.Now },
                new AyudaDiagnostica { Id = Guid.NewGuid(), Nombre = "Resonancia magnética cerebral", Descripcion = "Imagen detallada del cerebro", Tipo = TipoAyudaDiagnostica.ResonanciaMagnetica, Costo = 500000, RequierePreparacion = false, Activo = true, FechaCreacion = DateTime.Now },
                new AyudaDiagnostica { Id = Guid.NewGuid(), Nombre = "Rayos X de columna", Descripcion = "Radiografía vertebral", Tipo = TipoAyudaDiagnostica.RayosX, Costo = 90000, RequierePreparacion = false, Activo = true, FechaCreacion = DateTime.Now },
                new AyudaDiagnostica { Id = Guid.NewGuid(), Nombre = "Ecografía obstétrica", Descripcion = "Ultrasonido prenatal", Tipo = TipoAyudaDiagnostica.Ecografia, Costo = 100000, RequierePreparacion = false, Activo = true, FechaCreacion = DateTime.Now },
                new AyudaDiagnostica { Id = Guid.NewGuid(), Nombre = "Tomografía dental", Descripcion = "Escaneo de dientes y mandíbula", Tipo = TipoAyudaDiagnostica.Tomografia, Costo = 150000, RequierePreparacion = false, Activo = true, FechaCreacion = DateTime.Now },
                new AyudaDiagnostica { Id = Guid.NewGuid(), Nombre = "Resonancia magnética articular", Descripcion = "Imagen de articulaciones", Tipo = TipoAyudaDiagnostica.ResonanciaMagnetica, Costo = 400000, RequierePreparacion = false, Activo = true, FechaCreacion = DateTime.Now },
                new AyudaDiagnostica { Id = Guid.NewGuid(), Nombre = "Rayos X de extremidades", Descripcion = "Radiografía de brazos/piernas", Tipo = TipoAyudaDiagnostica.RayosX, Costo = 70000, RequierePreparacion = false, Activo = true, FechaCreacion = DateTime.Now },
                new AyudaDiagnostica { Id = Guid.NewGuid(), Nombre = "Ecografía cardíaca", Descripcion = "Ecocardiograma", Tipo = TipoAyudaDiagnostica.Ecografia, Costo = 140000, RequierePreparacion = false, Activo = true, FechaCreacion = DateTime.Now }
            };

            context.AyudasDiagnosticas.AddRange(ayudas);
            context.SaveChanges();
            Console.WriteLine("10 ayudas diagnósticas creadas.");
        }
    }
}