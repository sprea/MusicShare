using Microsoft.EntityFrameworkCore.Migrations;

namespace MusicShare.Data.Migrations
{
    public partial class ModificaUtenteCreazioneTabelleRaccolta : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "RaccoltaId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Raccolta",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Raccolta", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_RaccoltaId",
                table: "AspNetUsers",
                column: "RaccoltaId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Raccolta_RaccoltaId",
                table: "AspNetUsers",
                column: "RaccoltaId",
                principalTable: "Raccolta",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Raccolta_RaccoltaId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Raccolta");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_RaccoltaId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RaccoltaId",
                table: "AspNetUsers");
        }
    }
}
