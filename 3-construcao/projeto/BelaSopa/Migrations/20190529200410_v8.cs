using Microsoft.EntityFrameworkCore.Migrations;

namespace BelaSopa.Migrations
{
    public partial class v8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReceitaIngrediente_Ingrediente_IngredienteId",
                table: "ReceitaIngrediente");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceitaIngrediente_Receita_ReceitaId",
                table: "ReceitaIngrediente");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReceitaIngrediente",
                table: "ReceitaIngrediente");

            migrationBuilder.RenameTable(
                name: "ReceitaIngrediente",
                newName: "IngredienteUtilizado");

            migrationBuilder.RenameIndex(
                name: "IX_ReceitaIngrediente_ReceitaId",
                table: "IngredienteUtilizado",
                newName: "IX_IngredienteUtilizado_ReceitaId");

            migrationBuilder.RenameIndex(
                name: "IX_ReceitaIngrediente_IngredienteId",
                table: "IngredienteUtilizado",
                newName: "IX_IngredienteUtilizado_IngredienteId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IngredienteUtilizado",
                table: "IngredienteUtilizado",
                column: "ReceitaIngredienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_IngredienteUtilizado_Ingrediente_IngredienteId",
                table: "IngredienteUtilizado",
                column: "IngredienteId",
                principalTable: "Ingrediente",
                principalColumn: "IngredienteId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_IngredienteUtilizado_Receita_ReceitaId",
                table: "IngredienteUtilizado",
                column: "ReceitaId",
                principalTable: "Receita",
                principalColumn: "ReceitaId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IngredienteUtilizado_Ingrediente_IngredienteId",
                table: "IngredienteUtilizado");

            migrationBuilder.DropForeignKey(
                name: "FK_IngredienteUtilizado_Receita_ReceitaId",
                table: "IngredienteUtilizado");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IngredienteUtilizado",
                table: "IngredienteUtilizado");

            migrationBuilder.RenameTable(
                name: "IngredienteUtilizado",
                newName: "ReceitaIngrediente");

            migrationBuilder.RenameIndex(
                name: "IX_IngredienteUtilizado_ReceitaId",
                table: "ReceitaIngrediente",
                newName: "IX_ReceitaIngrediente_ReceitaId");

            migrationBuilder.RenameIndex(
                name: "IX_IngredienteUtilizado_IngredienteId",
                table: "ReceitaIngrediente",
                newName: "IX_ReceitaIngrediente_IngredienteId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReceitaIngrediente",
                table: "ReceitaIngrediente",
                column: "ReceitaIngredienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReceitaIngrediente_Ingrediente_IngredienteId",
                table: "ReceitaIngrediente",
                column: "IngredienteId",
                principalTable: "Ingrediente",
                principalColumn: "IngredienteId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReceitaIngrediente_Receita_ReceitaId",
                table: "ReceitaIngrediente",
                column: "ReceitaId",
                principalTable: "Receita",
                principalColumn: "ReceitaId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
