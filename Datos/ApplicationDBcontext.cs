using Microsoft.EntityFrameworkCore;
using MinimalAPI_NetCore8_2024.Modelo;

namespace MinimalAPI_NetCore8_2024.Datos
{
    public class ApplicationDBcontext : DbContext
    {
        public ApplicationDBcontext(DbContextOptions<ApplicationDBcontext> options) : base(options)
        {

        }

        //Por cada modelo de la DB se deben añadir aqui
        public DbSet<Propiedad> Propiedad { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)  // cuando se cree la tabla entonces va a añadir data
        {
            modelBuilder.Entity<Propiedad>().HasData(
                    new Propiedad { IdPropiedad = 1, Nombre = "Casa Colina campestre", Descripcion = "penthouse", Ubicacion = "Bogotá", Activa = true, FechaCreacion = DateTime.Now },
                    new Propiedad { IdPropiedad = 2, Nombre = "Apartamento 23", Descripcion = "ubicada en el sur de la ciudad", Ubicacion = "Bogotá", Activa = true, FechaCreacion = DateTime.Now },
                    new Propiedad { IdPropiedad = 3, Nombre = "Casa FAmilia Lopez", Descripcion = "fd", Ubicacion = "Medellin", Activa = true, FechaCreacion = DateTime.Now },
                    new Propiedad { IdPropiedad = 4, Nombre = "test 23", Descripcion = "sdfs", Ubicacion = "GYE", Activa = true, FechaCreacion = DateTime.Now },
                    new Propiedad { IdPropiedad = 5, Nombre = "casa nueva", Descripcion = "pentdfdsfhouse", Ubicacion = "Quito", Activa = false, FechaCreacion = DateTime.Now }
                );
        }
    }
}
