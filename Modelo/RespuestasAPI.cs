using System.Net;

namespace MinimalAPI_NetCore8_2024.Modelo
{
    public class RespuestasAPI
    {
        public RespuestasAPI()
        {
            Errores = new List<string>();
        }
        public bool Success { get; set; }
        public Object Resultado { get; set; }
        public HttpStatusCode codigoEstado { get; set; }
        public List<string> Errores { get; set; }

    }
}
