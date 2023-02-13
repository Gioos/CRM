using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoraWeb.Migrations
{
    public partial class atualizacaopessoa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NM_CEP",
                table: "TPESSOA");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NM_CEP",
                table: "TPESSOA",
                type: "character varying(20)",
                unicode: false,
                maxLength: 20,
                nullable: true);
        }
    }
}
