using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BelaSopa.Migrations
{
    public partial class v : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Distrito",
                table: "Administrador");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Administrador");

            migrationBuilder.DropColumn(
                name: "Nome",
                table: "Administrador");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Administrador",
                newName: "NomeDeUtilizador");

            migrationBuilder.RenameColumn(
                name: "UtilizadorId",
                table: "Administrador",
                newName: "ID");

            migrationBuilder.AddColumn<byte[]>(
                name: "HashPalavraChave",
                table: "Administrador",
                maxLength: 32,
                nullable: false,
                defaultValue: new byte[] {  });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HashPalavraChave",
                table: "Administrador");

            migrationBuilder.RenameColumn(
                name: "NomeDeUtilizador",
                table: "Administrador",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Administrador",
                newName: "UtilizadorId");

            migrationBuilder.AddColumn<string>(
                name: "Distrito",
                table: "Administrador",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Administrador",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "Administrador",
                nullable: false,
                defaultValue: "");
        }
    }
}
