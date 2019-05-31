using Microsoft.EntityFrameworkCore.Migrations;

namespace BelaSopa.Migrations
{
    public partial class v5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TextoTarefa_Tarefa_TarefaId",
                table: "TextoTarefa");

            migrationBuilder.AlterColumn<int>(
                name: "TarefaId",
                table: "TextoTarefa",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TextoTarefa_Tarefa_TarefaId",
                table: "TextoTarefa",
                column: "TarefaId",
                principalTable: "Tarefa",
                principalColumn: "TarefaId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TextoTarefa_Tarefa_TarefaId",
                table: "TextoTarefa");

            migrationBuilder.AlterColumn<int>(
                name: "TarefaId",
                table: "TextoTarefa",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_TextoTarefa_Tarefa_TarefaId",
                table: "TextoTarefa",
                column: "TarefaId",
                principalTable: "Tarefa",
                principalColumn: "TarefaId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
