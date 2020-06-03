using Microsoft.EntityFrameworkCore.Migrations;

namespace MusicShare.Data.Migrations
{
    public partial class CreazioneTabelleCanzoneGenere : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Genere",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genere", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Canzone",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titolo = table.Column<string>(nullable: true),
                    Autore = table.Column<string>(nullable: true),
                    Nome_file = table.Column<string>(nullable: true),
                    Id_Genere = table.Column<long>(nullable: false),
                    ApplicationUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Canzone", x => x.id);
                    table.ForeignKey(
                        name: "FK_Canzone_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Canzone_Genere_Id_Genere",
                        column: x => x.Id_Genere,
                        principalTable: "Genere",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Canzone_ApplicationUserId",
                table: "Canzone",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Canzone_Id_Genere",
                table: "Canzone",
                column: "Id_Genere");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Canzone");

            migrationBuilder.DropTable(
                name: "Genere");
        }
    }
}
