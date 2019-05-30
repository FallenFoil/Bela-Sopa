using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BelaSopa.Migrations
{
    public partial class v10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Utensilio");

            migrationBuilder.DropColumn(
                name: "Link",
                table: "Utensilio");

            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Tecnica");

            migrationBuilder.DropColumn(
                name: "Link",
                table: "Tecnica");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Utensilio",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                table: "Utensilio",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 200);

            migrationBuilder.AddColumn<byte[]>(
                name: "Imagem",
                table: "Utensilio",
                nullable: false,
                defaultValue: new byte[] {  });

            migrationBuilder.AddColumn<string>(
                name: "Texto",
                table: "Utensilio",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Tecnica",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                table: "Tecnica",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 200);

            migrationBuilder.AddColumn<byte[]>(
                name: "Imagem",
                table: "Tecnica",
                nullable: false,
                defaultValue: new byte[] {  });

            migrationBuilder.AddColumn<string>(
                name: "Texto",
                table: "Tecnica",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Imagem",
                table: "Utensilio");

            migrationBuilder.DropColumn(
                name: "Texto",
                table: "Utensilio");

            migrationBuilder.DropColumn(
                name: "Imagem",
                table: "Tecnica");

            migrationBuilder.DropColumn(
                name: "Texto",
                table: "Tecnica");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Utensilio",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                table: "Utensilio",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Utensilio",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Link",
                table: "Utensilio",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Tecnica",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                table: "Tecnica",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Tecnica",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Link",
                table: "Tecnica",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
