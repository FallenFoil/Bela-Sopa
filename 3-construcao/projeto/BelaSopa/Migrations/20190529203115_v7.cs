using Microsoft.EntityFrameworkCore.Migrations;

namespace BelaSopa.Migrations
{
    public partial class v7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "ReceitaIngrediente",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cliente",
                table: "Cliente",
                column: "UtilizadorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Administrador",
                table: "Administrador",
                column: "UtilizadorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Cliente",
                table: "Cliente");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Administrador",
                table: "Administrador");

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
                oldClrType: typeof(string));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Clientes",
                table: "Clientes",
                column: "UtilizadorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Administradores",
                table: "Administradores",
                column: "UtilizadorId");
        }
    }
}
