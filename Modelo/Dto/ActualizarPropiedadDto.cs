namespace MinimalAPI_NetCore8_2024.Modelo.Dto
{
    public class ActualizarPropiedadDto
    {
        public int IdPropiedad { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Ubicacion { get; set; }
        public bool Activa { get; set; }
    }
}
