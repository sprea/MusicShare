using Microsoft.EntityFrameworkCore.Migrations;

namespace MusicShare.Data.Migrations
{
    public partial class ModTabRaccoltaCanzoni : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Raccolta_RaccoltaId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_RaccoltaId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RaccoltaId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Raccolta",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Data_Creazione",
                table: "Raccolta",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RaccoltaCanzoni",
                columns: table => new
                {
                    Id_Raccolta = table.Column<long>(nullable: false),
                    Id_Canzone = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RaccoltaCanzoni", x => new { x.Id_Raccolta, x.Id_Canzone });
                    table.ForeignKey(
                        name: "FK_RaccoltaCanzoni_Canzone_Id_Canzone",
                        column: x => x.Id_Canzone,
                        principalTable: "Canzone",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RaccoltaCanzoni_Raccolta_Id_Raccolta",
                        column: x => x.Id_Raccolta,
                        principalTable: "Raccolta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Raccolta_ApplicationUserId",
                table: "Raccolta",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RaccoltaCanzoni_Id_Canzone",
                table: "RaccoltaCanzoni",
                column: "Id_Canzone");

            migrationBuilder.AddForeignKey(
                name: "FK_Raccolta_AspNetUsers_ApplicationUserId",
                table: "Raccolta",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Raccolta_AspNetUsers_ApplicationUserId",
                table: "Raccolta");

            migrationBuilder.DropTable(
                name: "RaccoltaCanzoni");

            migrationBuilder.DropIndex(
                name: "IX_Raccolta_ApplicationUserId",
                table: "Raccolta");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Raccolta");

            migrationBuilder.DropColumn(
                name: "Data_Creazione",
                table: "Raccolta");

            migrationBuilder.AddColumn<long>(
                name: "RaccoltaId",
                table: "AspNetUsers",
                type: "bigint",
                nullable: true);

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
    }
}
