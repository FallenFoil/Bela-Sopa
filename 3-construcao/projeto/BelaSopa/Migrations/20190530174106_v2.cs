using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BelaSopa.Migrations
{
    public partial class v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ClienteEmentaSemanal",
                table: "ClienteEmentaSemanal");

            migrationBuilder.DropColumn(
                name: "Horario",
                table: "ClienteEmentaSemanal");

            migrationBuilder.AddColumn<int>(
                name: "DataRefeicaoId",
                table: "ClienteEmentaSemanal",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClienteEmentaSemanal",
                table: "ClienteEmentaSemanal",
                columns: new[] { "ClienteId", "DataRefeicaoId" });

            migrationBuilder.CreateTable(
                name: "DataRefeicao",
                columns: table => new
                {
                    DataRefeicaoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Dia = table.Column<int>(nullable: false),
                    Almoco = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataRefeicao", x => x.DataRefeicaoId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DataRefeicao");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ClienteEmentaSemanal",
                table: "ClienteEmentaSemanal");

            migrationBuilder.DropColumn(
                name: "DataRefeicaoId",
                table: "ClienteEmentaSemanal");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Horario",
                table: "ClienteEmentaSemanal",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClienteEmentaSemanal",
                table: "ClienteEmentaSemanal",
                columns: new[] { "ClienteId", "Horario" });
        }
    }
}
