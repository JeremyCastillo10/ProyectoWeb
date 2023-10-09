using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoWeb.Migrations
{
    /// <inheritdoc />
    public partial class ventaanddetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Venta",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreadoPorUsusarioId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FechaVenta = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaEnvio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FinalVentaTotal = table.Column<double>(type: "float", nullable: false),
                    EstadoVenta = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaPago = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TransaccionId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ciudad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NombreCompleto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Venta", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Venta_AspNetUsers_CreadoPorUsusarioId",
                        column: x => x.CreadoPorUsusarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ventaDetalle",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VentaId = table.Column<int>(type: "int", nullable: false),
                    ProductoId = table.Column<int>(type: "int", nullable: false),
                    MetroCuadrado = table.Column<int>(type: "int", nullable: false),
                    PrecioPorMetroCuadrado = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ventaDetalle", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ventaDetalle_Producto_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Producto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ventaDetalle_Venta_VentaId",
                        column: x => x.VentaId,
                        principalTable: "Venta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Venta_CreadoPorUsusarioId",
                table: "Venta",
                column: "CreadoPorUsusarioId");

            migrationBuilder.CreateIndex(
                name: "IX_ventaDetalle_ProductoId",
                table: "ventaDetalle",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_ventaDetalle_VentaId",
                table: "ventaDetalle",
                column: "VentaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ventaDetalle");

            migrationBuilder.DropTable(
                name: "Venta");
        }
    }
}
