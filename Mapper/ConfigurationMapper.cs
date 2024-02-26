using AutoMapper;
using MinimalAPI_NetCore8_2024.Modelo;
using MinimalAPI_NetCore8_2024.Modelo.Dto;

namespace MinimalAPI_NetCore8_2024.Mapper
{
    public class ConfigurationMapper : Profile
    {
        public ConfigurationMapper()
        {
            CreateMap<Propiedad, CrearPropiedadDto>().ReverseMap(); // Mapeamos de Mapear a PropiedadDto, si se requiere de de Dto a Propiedead entonces ReverseMap
            CreateMap<Propiedad, PropiedadDto>().ReverseMap();
        }
    }
}
