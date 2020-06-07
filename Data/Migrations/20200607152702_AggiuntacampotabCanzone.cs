using Microsoft.EntityFrameworkCore.Migrations;

namespace MusicShare.Data.Migrations
{
    public partial class AggiuntacampotabCanzone : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Anno_pubblicazione",
                table: "Canzone",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Anno_pubblicazione",
                table: "Canzone");
        }
    }
}
