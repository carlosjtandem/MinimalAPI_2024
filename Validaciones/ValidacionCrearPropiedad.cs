using FluentValidation;
using MinimalAPI_NetCore8_2024.Modelo.Dto;

namespace MinimalAPI_NetCore8_2024.Validaciones
{
    public class ValidacionCrearPropiedad : AbstractValidator<CrearPropiedadDto>
    {
        public ValidacionCrearPropiedad()
        {
            RuleFor(modelo => modelo.Nombre).NotEmpty();
            RuleFor(modelo => modelo.Descripcion).NotEmpty();
            RuleFor(modelo => modelo.Ubicacion).NotEmpty();

        }
    }
}
