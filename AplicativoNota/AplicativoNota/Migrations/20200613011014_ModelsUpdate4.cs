using Microsoft.EntityFrameworkCore.Migrations;

namespace AplicativoNota.Migrations
{
    public partial class ModelsUpdate4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "Turma",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nome",
                table: "Turma");
        }
    }
}
