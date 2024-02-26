using MinimalAPI_NetCore8_2024.Modelo;

namespace MinimalAPI_NetCore8_2024.Datos
{
    public static class DatosPropiedad
    {
        public static List<Propiedad> listaPropiedades = new List<Propiedad>
        {
        new Propiedad{IdPropiedad=1, Nombre="Casa Colina campestre", Descripcion="penthouse", Ubicacion="Bogotá", Activa=true, FechaCreacion=DateTime.Now.AddDays(-10) },
        new Propiedad{IdPropiedad=2, Nombre="Apartamento 23", Descripcion="test", Ubicacion="Bogotá", Activa=true, FechaCreacion=DateTime.Now.AddDays(-10) },
        new Propiedad{IdPropiedad=3, Nombre="Casa FAmilia Lopez", Descripcion="fd", Ubicacion="Medellin", Activa=true, FechaCreacion=DateTime.Now.AddDays(-10) },
        new Propiedad { IdPropiedad = 4, Nombre = "test 23", Descripcion = "sdfs", Ubicacion = "GYE", Activa = true, FechaCreacion = DateTime.Now.AddDays(-10) },
        new Propiedad { IdPropiedad = 5, Nombre = "casa nueva", Descripcion = "pentdfdsfhouse", Ubicacion = "Quito", Activa = true, FechaCreacion = DateTime.Now.AddDays(-10) }

        };
    }
}
