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

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClienteEmentaSemanal",
                table: "ClienteEmentaSemanal",
                columns: new[] { "ClienteId", "Horario" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ClienteEmentaSemanal",
                table: "ClienteEmentaSemanal");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClienteEmentaSemanal",
                table: "ClienteEmentaSemanal",
                columns: new[] { "ClienteId", "ReceitaId" });
        }
    }
}
