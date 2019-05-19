using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Migrations
{
    public partial class fixReceita : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Receita",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Cliente",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Administrador",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Receita");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Cliente");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Administrador");
        }
    }
}
