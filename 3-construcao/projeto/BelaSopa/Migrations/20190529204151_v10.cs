using Microsoft.EntityFrameworkCore.Migrations;

namespace BelaSopa.Migrations
{
    public partial class v10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IngredienteUtilizado_Ingrediente_IngredienteId",
                table: "IngredienteUtilizado");

            migrationBuilder.AlterColumn<int>(
                name: "IngredienteId",
                table: "IngredienteUtilizado",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_IngredienteUtilizado_Ingrediente_IngredienteId",
                table: "IngredienteUtilizado",
                column: "IngredienteId",
                principalTable: "Ingrediente",
                principalColumn: "IngredienteId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IngredienteUtilizado_Ingrediente_IngredienteId",
                table: "IngredienteUtilizado");

            migrationBuilder.AlterColumn<int>(
                name: "IngredienteId",
                table: "IngredienteUtilizado",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_IngredienteUtilizado_Ingrediente_IngredienteId",
                table: "IngredienteUtilizado",
                column: "IngredienteId",
                principalTable: "Ingrediente",
                principalColumn: "IngredienteId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
