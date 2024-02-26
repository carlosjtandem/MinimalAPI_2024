using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MinimalAPI_NetCore8_2024.Datos;
using MinimalAPI_NetCore8_2024.Modelo;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
}).WithName("ObtenerPropiedades");

//GET  by id
app.MapGet("/api/propiedades/{id:int}", (int id) =>
{
    return Results.Ok(DatosPropiedad.listaPropiedades.FirstOrDefault(p => p.IdPropiedad == id));
}).WithName("ObtenerPropiedad");

//CREAR propiedad
app.MapPost("/api/propiedades", ([FromBody] Propiedad propiedad) =>
{
    if (propiedad.IdPropiedad != 0 || string.IsNullOrEmpty(propiedad.Nombre))
    {
        return Results.BadRequest("IdPropiedad incorrecto o nombre vacio");
    }

    //validacion si el nombre ya existe
    if (DatosPropiedad.listaPropiedades.FirstOrDefault(p => p.Nombre.ToLower() == propiedad.Nombre) != null)
    {
        return Results.BadRequest("El nombre ingresado ya existe");
    }

    propiedad.IdPropiedad = DatosPropiedad.listaPropiedades.OrderByDescending(p => p.IdPropiedad).FirstOrDefault().IdPropiedad + 1;  // Obtiene el ultimo id y suma 1

    DatosPropiedad.listaPropiedades.Add(propiedad);

    //return Results.Ok(DatosPropiedad.listaPropiedades);// forma 1 // retorna 200 pero eso no es lo correcto porque 200 es para get
    //return Results.Created($"/api/propiedades/{propiedad.IdPropiedad}", propiedad); // forma 2  // retorna location: /api/propiedades/6
    return Results.CreatedAtRoute("ObtenerPropiedad", new { id = propiedad.IdPropiedad }, propiedad);  // retorna ruta completa.  location: https://localhost:7200/api/propiedades/6 

}).WithName("CrearPropiedad");

//
app.UseHttpsRedirection();
app.Run();


