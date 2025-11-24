using InventoryMicroservice.Models;

namespace InventoryMicroservice;

public static class Seeder
{
    public static void Seed(ApplicationDbContext context)
    {
        if (context.Medicamentos.Any())
            return;

        var medicamentos = new List<Medicamento>
        {
            new()
            {
                Id = Guid.NewGuid(),
                Nombre = "Paracetamol",
                Descripcion = "Analgesico y antipiretico",
                Concentracion = "500 mg",
                FormaFarmaceutica = FormaFarmaceutica.Tableta,
                Proveedor = "Farmalab"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Nombre = "Ibuprofeno",
                Descripcion = "Antiinflamatorio no esteroide",
                Concentracion = "400 mg",
                FormaFarmaceutica = FormaFarmaceutica.Capsula,
                Proveedor = "Medicorp"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Nombre = "Amoxicilina",
                Descripcion = "Antibiotico penicilinico",
                Concentracion = "500 mg",
                FormaFarmaceutica = FormaFarmaceutica.Capsula,
                Proveedor = "BioLabs"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Nombre = "Loratadina",
                Descripcion = "Antihistaminico",
                Concentracion = "10 mg",
                FormaFarmaceutica = FormaFarmaceutica.Tableta,
                Proveedor = "Allerpharma"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Nombre = "Omeprazol",
                Descripcion = "Inhibidor de bomba de protones",
                Concentracion = "20 mg",
                FormaFarmaceutica = FormaFarmaceutica.Capsula,
                Proveedor = "GastroHealth"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Nombre = "Salbutamol",
                Descripcion = "Broncodilatador",
                Concentracion = "100 mcg/dosis",
                FormaFarmaceutica = FormaFarmaceutica.Solucion,
                Proveedor = "RespiraMed"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Nombre = "Metformina",
                Descripcion = "Antidiabetico oral",
                Concentracion = "850 mg",
                FormaFarmaceutica = FormaFarmaceutica.Tableta,
                Proveedor = "GlucoPharm"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Nombre = "Diclofenaco",
                Descripcion = "Antiinflamatorio no esteroide",
                Concentracion = "75 mg/3 ml",
                FormaFarmaceutica = FormaFarmaceutica.Inyeccion,
                Proveedor = "DolorFree"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Nombre = "Ketoconazol",
                Descripcion = "Antimicotico topico",
                Concentracion = "2 %",
                FormaFarmaceutica = FormaFarmaceutica.Crema,
                Proveedor = "Dermalab"
            },
            new()
            {
                Id = Guid.NewGuid(),
                Nombre = "Gotas nasales de oximetazolina",
                Descripcion = "Descongestionante nasal",
                Concentracion = "0.05 %",
                FormaFarmaceutica = FormaFarmaceutica.Gotas,
                Proveedor = "RespiraMed"
            }
        };

        context.Medicamentos.AddRange(medicamentos);
        context.SaveChanges();

        Console.WriteLine("Seeder ejecutado: 10 medicamentos agregados al inventario.");
    }
}
