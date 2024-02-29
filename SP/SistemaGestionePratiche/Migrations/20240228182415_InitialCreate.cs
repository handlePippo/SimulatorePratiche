using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaGestionePratiche.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ListPratiche",
                columns: table => new
                {
                    IdPratica = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CodiceFiscale = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    DataNascita = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cognome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileByte = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Stato = table.Column<int>(type: "int", nullable: false),
                    DataCreazione = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataUpdate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ListPratiche", x => x.IdPratica);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ListPratiche");
        }
    }
}
