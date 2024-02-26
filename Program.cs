using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MinimalAPI_NetCore8_2024.Datos;
using MinimalAPI_NetCore8_2024.Mapper;
using MinimalAPI_NetCore8_2024.Modelo;
using MinimalAPI_NetCore8_2024.Modelo.Dto;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//A�adir Automapper
builder.Services.AddAutoMapper(typeof(ConfigurationMapper));

//A�adir  Fluent Validation al contendero de servicios
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//GET all
app.MapGet("/api/propiedades", (ILogger<Program> logger) =>
{
    //Usarr le _logger como inyeccion de dependencias
    logger.Log(LogLevel.Information, "Carga todas las propiedades - esto es Log");

    return Results.Ok(DatosPropiedad.listaPropiedades);
}).WithName("ObtenerPropiedades").Produces<IEnumerable<Propiedad>>(200);

//GET  by id
app.MapGet("/api/propiedades/{id:int}", (int id) =>
{
    return Results.Ok(DatosPropiedad.listaPropiedades.FirstOrDefault(p => p.IdPropiedad == id));
}).WithName("ObtenerPropiedad").Produces<Propiedad>(200);

//CREAR propiedad
app.MapPost("/api/propiedades", (IMapper _mapper, IValidator<CrearPropiedadDto> _validacion, [FromBody] CrearPropiedadDto crearPropiedadDto) => // En vez de exponer el modelo se expone el DTO
{
    var resultadoValidaciones = _validacion.ValidateAsync(crearPropiedadDto).GetAwaiter().GetResult();
    
    
    if (!resultadoValidaciones.IsValid)
    {
        return Results.BadRequest(resultadoValidaciones.Errors.FirstOrDefault().ToString());
    }

    //validacion si el nombre ya existe
    if (DatosPropiedad.listaPropiedades.FirstOrDefault(p => p.Nombre.ToLower() == crearPropiedadDto.Nombre.ToLower()) != null)
    {
        return Results.BadRequest("El nombre ingresado ya existe");
    }

    //Propiedad propiedad = new Propiedad()  // Convertimos a propiedad(modelo) desde PropiedadDto ya que para asignar el Id solo lo tenemos en el modelo.
    //{
    //    Nombre = crearPropiedadDto.Nombre,
    //    Descripcion = crearPropiedadDto.Descripcion,
    //    Ubicacion = crearPropiedadDto.Ubicacion,
    //    Activa = crearPropiedadDto.Activa
    //};

    Propiedad propiedad = _mapper.Map<Propiedad>(crearPropiedadDto);

    propiedad.IdPropiedad = DatosPropiedad.listaPropiedades.OrderByDescending(p => p.IdPropiedad).FirstOrDefault().IdPropiedad + 1;  // Obtiene el ultimo id y suma 1

    DatosPropiedad.listaPropiedades.Add(propiedad);

    //PropiedadDto propiedadDto = new PropiedadDto()
    //{
    //    IdPropiedad = propiedad.IdPropiedad,
    //    Nombre = propiedad.Nombre,
    //    Descripcion = propiedad.Descripcion,
    //    Ubicacion = propiedad.Ubicacion,
    //    Activa = propiedad.Activa
    //};

    PropiedadDto propiedadDto = _mapper.Map<PropiedadDto>(propiedad);


    return Results.CreatedAtRoute("ObtenerPropiedad", new { id = propiedad.IdPropiedad }, propiedadDto);  // retorna ruta completa.  location: https://localhost:7200/api/propiedades/6 

}).WithName("CrearPropiedad").Accepts<CrearPropiedadDto>("application/json").Produces<PropiedadDto>(201).Produces<PropiedadDto>(400);

//
app.UseHttpsRedirection();
app.Run();


