using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BelaSopa.Migrations
{
    public partial class v9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Imagem",
                table: "Utensilio");

            migrationBuilder.DropColumn(
                name: "Imagem",
                table: "Tecnica");

            migrationBuilder.DropColumn(
                name: "Imagem",
                table: "Receita");

            migrationBuilder.DropColumn(
                name: "Imagem",
                table: "Ingrediente");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Imagem",
                table: "Utensilio",
                nullable: false,
                defaultValue: new byte[] {  });

            migrationBuilder.AddColumn<byte[]>(
                name: "Imagem",
                table: "Tecnica",
                nullable: false,
                defaultValue: new byte[] {  });

            migrationBuilder.AddColumn<byte[]>(
                name: "Imagem",
                table: "Receita",
                nullable: false,
                defaultValue: new byte[] {  });

            migrationBuilder.AddColumn<byte[]>(
                name: "Imagem",
                table: "Ingrediente",
                nullable: false,
                defaultValue: new byte[] {  });
        }
    }
}
