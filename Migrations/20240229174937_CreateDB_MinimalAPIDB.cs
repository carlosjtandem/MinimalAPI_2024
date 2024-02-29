using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MinimalAPI_NetCore8_2024.Migrations
{
    /// <inheritdoc />
    public partial class CreateDB_MinimalAPIDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Propiedad",
                columns: table => new
                {
                    IdPropiedad = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ubicacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Activa = table.Column<bool>(type: "bit", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Propiedad", x => x.IdPropiedad);
                });

            migrationBuilder.InsertData(
                table: "Propiedad",
                columns: new[] { "IdPropiedad", "Activa", "Descripcion", "FechaCreacion", "Nombre", "Ubicacion" },
                values: new object[,]
                {
                    { 1, true, "penthouse", new DateTime(2024, 2, 29, 12, 49, 34, 743, DateTimeKind.Local).AddTicks(6372), "Casa Colina campestre", "Bogotá" },
                    { 2, true, "ubicada en el sur de la ciudad", new DateTime(2024, 2, 29, 12, 49, 34, 743, DateTimeKind.Local).AddTicks(6388), "Apartamento 23", "Bogotá" },
                    { 3, true, "fd", new DateTime(2024, 2, 29, 12, 49, 34, 743, DateTimeKind.Local).AddTicks(6391), "Casa FAmilia Lopez", "Medellin" },
                    { 4, true, "sdfs", new DateTime(2024, 2, 29, 12, 49, 34, 743, DateTimeKind.Local).AddTicks(6393), "test 23", "GYE" },
                    { 5, false, "pentdfdsfhouse", new DateTime(2024, 2, 29, 12, 49, 34, 743, DateTimeKind.Local).AddTicks(6395), "casa nueva", "Quito" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Propiedad");
        }
    }
}
