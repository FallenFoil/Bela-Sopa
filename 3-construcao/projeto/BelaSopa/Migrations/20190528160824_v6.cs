using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BelaSopa.Migrations
{
    public partial class v6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Ingrediente");

            migrationBuilder.DropColumn(
                name: "Link",
                table: "Ingrediente");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Ingrediente",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                table: "Ingrediente",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 200);

            migrationBuilder.AddColumn<byte[]>(
                name: "Imagem",
                table: "Ingrediente",
                nullable: false,
                defaultValue: new byte[] {  });

            migrationBuilder.AddColumn<string>(
                name: "Texto",
                table: "Ingrediente",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Imagem",
                table: "Ingrediente");

            migrationBuilder.DropColumn(
                name: "Texto",
                table: "Ingrediente");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Ingrediente",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                table: "Ingrediente",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Ingrediente",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Link",
                table: "Ingrediente",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
