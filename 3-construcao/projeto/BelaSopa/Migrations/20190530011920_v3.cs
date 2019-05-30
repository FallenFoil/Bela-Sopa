using Microsoft.EntityFrameworkCore.Migrations;

namespace BelaSopa.Migrations
{
    public partial class v3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "PercentagemVdrAdulto",
                table: "ValorNutricional",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "PercentagemVdrAdulto",
                table: "ValorNutricional",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
