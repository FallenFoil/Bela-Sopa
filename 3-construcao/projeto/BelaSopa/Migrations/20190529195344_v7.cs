using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BelaSopa.Migrations
{
    public partial class v7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReceitaIngrediente_Ingrediente_IngredienteId",
                table: "ReceitaIngrediente");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_ReceitaIngrediente_IngredienteId_ReceitaId",
                table: "ReceitaIngrediente");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReceitaIngrediente",
                table: "ReceitaIngrediente");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Clientes",
                table: "Clientes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Administradores",
                table: "Administradores");

            migrationBuilder.RenameTable(
                name: "Clientes",
                newName: "Cliente");

            migrationBuilder.RenameTable(
                name: "Administradores",
                newName: "Administrador");

            migrationBuilder.AlterColumn<string>(
                name: "Quantidade",
                table: "ReceitaIngrediente",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "IngredienteId",
                table: "ReceitaIngrediente",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "ReceitaIngredienteId",
                table: "ReceitaIngrediente",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "ReceitaIngrediente",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReceitaIngrediente",
                table: "ReceitaIngrediente",
                column: "ReceitaIngredienteId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cliente",
                table: "Cliente",
                column: "UtilizadorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Administrador",
                table: "Administrador",
                column: "UtilizadorId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceitaIngrediente_IngredienteId",
                table: "ReceitaIngrediente",
                column: "IngredienteId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceitaIngrediente_ReceitaId",
                table: "ReceitaIngrediente",
                column: "ReceitaId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReceitaIngrediente_Ingrediente_IngredienteId",
                table: "ReceitaIngrediente",
                column: "IngredienteId",
                principalTable: "Ingrediente",
                principalColumn: "IngredienteId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReceitaIngrediente_Ingrediente_IngredienteId",
                table: "ReceitaIngrediente");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReceitaIngrediente",
                table: "ReceitaIngrediente");

            migrationBuilder.DropIndex(
                name: "IX_ReceitaIngrediente_IngredienteId",
                table: "ReceitaIngrediente");

            migrationBuilder.DropIndex(
                name: "IX_ReceitaIngrediente_ReceitaId",
                table: "ReceitaIngrediente");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cliente",
                table: "Cliente");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Administrador",
                table: "Administrador");

            migrationBuilder.DropColumn(
                name: "ReceitaIngredienteId",
                table: "ReceitaIngrediente");

            migrationBuilder.DropColumn(
                name: "Nome",
                table: "ReceitaIngrediente");

            migrationBuilder.RenameTable(
                name: "Cliente",
                newName: "Clientes");

            migrationBuilder.RenameTable(
                name: "Administrador",
                newName: "Administradores");

            migrationBuilder.AlterColumn<int>(
                name: "Quantidade",
                table: "ReceitaIngrediente",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<int>(
                name: "IngredienteId",
                table: "ReceitaIngrediente",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_ReceitaIngrediente_IngredienteId_ReceitaId",
                table: "ReceitaIngrediente",
                columns: new[] { "IngredienteId", "ReceitaId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReceitaIngrediente",
                table: "ReceitaIngrediente",
                columns: new[] { "ReceitaId", "IngredienteId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Clientes",
                table: "Clientes",
                column: "UtilizadorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Administradores",
                table: "Administradores",
                column: "UtilizadorId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReceitaIngrediente_Ingrediente_IngredienteId",
                table: "ReceitaIngrediente",
                column: "IngredienteId",
                principalTable: "Ingrediente",
                principalColumn: "IngredienteId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
