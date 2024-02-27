using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MinimalAPI_NetCore8_2024.Datos;
using MinimalAPI_NetCore8_2024.Mapper;
using MinimalAPI_NetCore8_2024.Modelo;
using MinimalAPI_NetCore8_2024.Modelo.Dto;
using System.ComponentModel;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Añadir Automapper
builder.Services.AddAutoMapper(typeof(ConfigurationMapper));

//Añadir  Fluent Validation al contendero de servicios
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
    RespuestasAPI respuesta = new();

    //Usarr le _logger como inyeccion de dependencias
    logger.Log(LogLevel.Information, "Carga todas las propiedades - esto es Log por inyeccion de dependencias");

    respuesta.Resultado = DatosPropiedad.listaPropiedades;
    respuesta.Success = true;
    respuesta.codigoEstado = HttpStatusCode.OK;

    return Results.Ok(respuesta);
}).WithName("ObtenerPropiedades").Produces<RespuestasAPI>(200);

//GET  by id
app.MapGet("/api/propiedades/{id:int}", (int id) =>
{
    RespuestasAPI respuesta = new();
    respuesta.Resultado = DatosPropiedad.listaPropiedades.FirstOrDefault(p => p.IdPropiedad == id);
    respuesta.Success = true;
    respuesta.codigoEstado = HttpStatusCode.OK;

    return Results.Ok(respuesta);
}).WithName("ObtenerPropiedad").Produces<RespuestasAPI>(200);

//CREAR propiedad
app.MapPost("/api/propiedades", async (IMapper _mapper, IValidator<CrearPropiedadDto> _validacion, [FromBody] CrearPropiedadDto crearPropiedadDto) => // En vez de exponer el modelo se expone el DTO
{
    RespuestasAPI respuesta = new();

    var resultadoValidaciones = await _validacion.ValidateAsync(crearPropiedadDto);

    if (!resultadoValidaciones.IsValid)
    {
        respuesta.Errores.Add(resultadoValidaciones.Errors.FirstOrDefault().ToString());
        return Results.BadRequest(respuesta);
    }

    //validacion si el nombre ya existe
    if (DatosPropiedad.listaPropiedades.FirstOrDefault(p => p.Nombre.ToLower() == crearPropiedadDto.Nombre.ToLower()) != null)
    {
        respuesta.Errores.Add("El nombre ingresado ya existe");
        return Results.BadRequest(respuesta);
    }

    Propiedad propiedad = _mapper.Map<Propiedad>(crearPropiedadDto);

    propiedad.IdPropiedad = DatosPropiedad.listaPropiedades.OrderByDescending(p => p.IdPropiedad).FirstOrDefault().IdPropiedad + 1;  // Obtiene el ultimo id y suma 1

    DatosPropiedad.listaPropiedades.Add(propiedad);

    PropiedadDto propiedadDto = _mapper.Map<PropiedadDto>(propiedad);


    //return Results.CreatedAtRoute("ObtenerPropiedad", new { id = propiedad.IdPropiedad }, propiedadDto);  // retorna ruta completa.  location: https://localhost:7200/api/propiedades/6 

    respuesta.Resultado = propiedadDto;
    respuesta.Success = true;
    respuesta.codigoEstado = HttpStatusCode.Created;

    return Results.Ok(respuesta);

}).WithName("CrearPropiedad").Accepts<CrearPropiedadDto>("application/json").Produces<RespuestasAPI>(201).Produces<PropiedadDto>(400);


//UPDATE propiedad
app.MapPut("/api/propiedades", async (IMapper _mapper, IValidator<ActualizarPropiedadDto> _validacion, [FromBody] ActualizarPropiedadDto actualizarPropiedadDto) => // En vez de exponer el modelo se expone el DTO
{
    RespuestasAPI respuesta = new();

    var resultadoValidaciones = await _validacion.ValidateAsync(actualizarPropiedadDto);

    if (!resultadoValidaciones.IsValid)
    {
        respuesta.Errores.Add(resultadoValidaciones.Errors.FirstOrDefault().ToString());
        return Results.BadRequest(respuesta);
    }

    //Obtener la propiedad 
    Propiedad propiedadDesdeDB = DatosPropiedad.listaPropiedades.FirstOrDefault(p => p.IdPropiedad == actualizarPropiedadDto.IdPropiedad);

    propiedadDesdeDB.Nombre = actualizarPropiedadDto.Nombre;
    propiedadDesdeDB.Descripcion = actualizarPropiedadDto.Descripcion;
    propiedadDesdeDB.Ubicacion = actualizarPropiedadDto.Ubicacion;
    propiedadDesdeDB.Activa = actualizarPropiedadDto.Activa;


    respuesta.Resultado = _mapper.Map<PropiedadDto>(propiedadDesdeDB);
    respuesta.Success = true;
    respuesta.codigoEstado = HttpStatusCode.Created;

    return Results.Ok(respuesta);

}).WithName("ActualizarPropiedad").Accepts<ActualizarPropiedadDto>("application/json").Produces<RespuestasAPI>(201).Produces<PropiedadDto>(400);


//BORARR
//GET  by id
app.MapDelete("/api/propiedades/{id:int}", (int id) =>
{
    RespuestasAPI respuesta = new() { Success = false, codigoEstado = HttpStatusCode.BadRequest };

    //Obtener el id de la propiedad a eliminar
    Propiedad propiedadDesdeDB = DatosPropiedad.listaPropiedades.FirstOrDefault(p => p.IdPropiedad == id);

    if (propiedadDesdeDB != null)
    {
        DatosPropiedad.listaPropiedades.Remove(propiedadDesdeDB);
        respuesta.Success = true;
        respuesta.codigoEstado = HttpStatusCode.NoContent;
        return Results.Ok(respuesta);
    }
    else
    {
        respuesta.Errores.Add("el id de la propiedad es invalido");
        return Results.BadRequest(respuesta);

    }
});

//
app.UseHttpsRedirection();
app.Run();


