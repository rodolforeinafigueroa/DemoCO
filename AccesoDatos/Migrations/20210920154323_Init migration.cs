using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ERP.AccesoDatos.Migrations
{
    public partial class Initmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Companias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RazonSocial = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contactos",
                columns: table => new
                {
                    RowId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompaniaId = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contactos", x => x.RowId);
                    table.ForeignKey(
                        name: "FK_Contactos_Companias_CompaniaId",
                        column: x => x.CompaniaId,
                        principalTable: "Companias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Regionales",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    CompaniaId = table.Column<int>(type: "int", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    IndEstado = table.Column<int>(type: "int", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regionales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Regionales_Companias_CompaniaId",
                        column: x => x.CompaniaId,
                        principalTable: "Companias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CentrosOperacion",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    CompaniaId = table.Column<int>(type: "int", nullable: false),
                    RegionalId = table.Column<string>(type: "nvarchar(2)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    IndEstado = table.Column<int>(type: "int", nullable: false),
                    ContactoRowid = table.Column<int>(type: "int", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CentrosOperacion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CentrosOperacion_Companias_CompaniaId",
                        column: x => x.CompaniaId,
                        principalTable: "Companias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CentrosOperacion_Contactos_ContactoRowid",
                        column: x => x.ContactoRowid,
                        principalTable: "Contactos",
                        principalColumn: "RowId");
                    table.ForeignKey(
                        name: "FK_CentrosOperacion_Regionales_RegionalId",
                        column: x => x.RegionalId,
                        principalTable: "Regionales",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CentrosOperacion_CompaniaId",
                table: "CentrosOperacion",
                column: "CompaniaId");

            migrationBuilder.CreateIndex(
                name: "IX_CentrosOperacion_ContactoRowid",
                table: "CentrosOperacion",
                column: "ContactoRowid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CentrosOperacion_RegionalId",
                table: "CentrosOperacion",
                column: "RegionalId");

            migrationBuilder.CreateIndex(
                name: "IX_Contactos_CompaniaId",
                table: "Contactos",
                column: "CompaniaId");

            migrationBuilder.CreateIndex(
                name: "IX_Regionales_CompaniaId",
                table: "Regionales",
                column: "CompaniaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CentrosOperacion");

            migrationBuilder.DropTable(
                name: "Contactos");

            migrationBuilder.DropTable(
                name: "Regionales");

            migrationBuilder.DropTable(
                name: "Companias");
        }
    }
}
