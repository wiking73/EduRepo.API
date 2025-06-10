using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduRepo.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUserPersonalData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Imie",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Nazwisko",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NrAlbumu",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Kursy_WlascicielId",
                table: "Kursy",
                column: "WlascicielId");

            migrationBuilder.AddForeignKey(
                name: "FK_Kursy_AspNetUsers_WlascicielId",
                table: "Kursy",
                column: "WlascicielId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Kursy_AspNetUsers_WlascicielId",
                table: "Kursy");

            migrationBuilder.DropIndex(
                name: "IX_Kursy_WlascicielId",
                table: "Kursy");

            migrationBuilder.DropColumn(
                name: "Imie",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Nazwisko",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "NrAlbumu",
                table: "AspNetUsers");
        }
    }
}
