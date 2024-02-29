using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.ComponentModel.DataAnnotations;

namespace MinimalAPI_NetCore8_2024.Modelo
{
    public class Propiedad
    {
        [Key]  // esta linea es necesria para que EF al migrar lo identifique como PK
        public  int IdPropiedad { get; set; }
        public string Nombre { get; set;}
        public string  Descripcion { get; set; }
        public string Ubicacion { get; set; }
        public bool Activa { get; set; }
        public  DateTime? FechaCreacion { get; set; }
    }
}
