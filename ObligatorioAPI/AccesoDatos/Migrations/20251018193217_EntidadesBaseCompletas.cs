using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class EntidadesBaseCompletas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Equipos",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipos", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "TiposDeGasto",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    desc = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposDeGasto", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    apellido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    mail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    equipoid = table.Column<int>(type: "int", nullable: true),
                    rol = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.id);
                    table.ForeignKey(
                        name: "FK_Usuarios_Equipos_equipoid",
                        column: x => x.equipoid,
                        principalTable: "Equipos",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Pagos",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    metodoPago = table.Column<int>(type: "int", nullable: false),
                    tipoGastoid = table.Column<int>(type: "int", nullable: false),
                    usuarioid = table.Column<int>(type: "int", nullable: false),
                    desc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(21)", maxLength: 21, nullable: false),
                    fechaInicio = table.Column<DateTime>(type: "datetime2", nullable: true),
                    fechaFin = table.Column<DateTime>(type: "datetime2", nullable: true),
                    montoMensual = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    fechaPago = table.Column<DateTime>(type: "datetime2", nullable: true),
                    numRecibo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    monto = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pagos", x => x.id);
                    table.ForeignKey(
                        name: "FK_Pagos_TiposDeGasto_tipoGastoid",
                        column: x => x.tipoGastoid,
                        principalTable: "TiposDeGasto",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pagos_Usuarios_usuarioid",
                        column: x => x.usuarioid,
                        principalTable: "Usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pagos_tipoGastoid",
                table: "Pagos",
                column: "tipoGastoid");

            migrationBuilder.CreateIndex(
                name: "IX_Pagos_usuarioid",
                table: "Pagos",
                column: "usuarioid");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_equipoid",
                table: "Usuarios",
                column: "equipoid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pagos");

            migrationBuilder.DropTable(
                name: "TiposDeGasto");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Equipos");
        }
    }
}
