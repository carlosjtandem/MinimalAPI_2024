namespace MinimalAPI_NetCore8_2024.Modelo.Dto
{
    public class CrearPropiedadDto  // esto es una copia de Propiedad Model-- pero no tiene Id ni fecha de creacion porque al crear no es necesario
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Ubicacion { get; set; }
        public bool Activa { get; set; }
    }
}
