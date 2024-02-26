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
app.MapGet("/api/propiedades", () =>
{
    return Results.Ok(DatosPropiedad.listaPropiedades);
});
//GET  by id
app.MapGet("/api/propiedades/{id:int}", (int id) =>
{
    return Results.Ok(DatosPropiedad.listaPropiedades.FirstOrDefault(p => p.IdPropiedad == id));
});

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

    return Results.Ok(DatosPropiedad.listaPropiedades);
});

//
app.UseHttpsRedirection();
app.Run();


