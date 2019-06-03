using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BelaSopa.Migrations
{
    public partial class v14 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Data",
                table: "ClienteReceitaFinalizada",
                newName: "DataInicio");

            migrationBuilder.AddColumn<int>(
                name: "ReceitaId",
                table: "EstadoConfecao",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataFim",
                table: "ClienteReceitaFinalizada",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_EstadoConfecao_ReceitaId",
                table: "EstadoConfecao",
                column: "ReceitaId");

            migrationBuilder.AddForeignKey(
                name: "FK_EstadoConfecao_Receita_ReceitaId",
                table: "EstadoConfecao",
                column: "ReceitaId",
                principalTable: "Receita",
                principalColumn: "ReceitaId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EstadoConfecao_Receita_ReceitaId",
                table: "EstadoConfecao");

            migrationBuilder.DropIndex(
                name: "IX_EstadoConfecao_ReceitaId",
                table: "EstadoConfecao");

            migrationBuilder.DropColumn(
                name: "ReceitaId",
                table: "EstadoConfecao");

            migrationBuilder.DropColumn(
                name: "DataFim",
                table: "ClienteReceitaFinalizada");

            migrationBuilder.RenameColumn(
                name: "DataInicio",
                table: "ClienteReceitaFinalizada",
                newName: "Data");
        }
    }
}
