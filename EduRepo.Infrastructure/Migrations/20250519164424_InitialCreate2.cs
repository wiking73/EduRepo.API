using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduRepo.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Uczestnictwa_Kursy_KursIdKursu",
                table: "Uczestnictwa");

            migrationBuilder.DropColumn(
                name: "IdKursu",
                table: "Uczestnictwa");

            migrationBuilder.RenameColumn(
                name: "KursIdKursu",
                table: "Uczestnictwa",
                newName: "KursId");

            migrationBuilder.RenameIndex(
                name: "IX_Uczestnictwa_KursIdKursu",
                table: "Uczestnictwa",
                newName: "IX_Uczestnictwa_KursId");

            migrationBuilder.AddForeignKey(
                name: "FK_Uczestnictwa_Kursy_KursId",
                table: "Uczestnictwa",
                column: "KursId",
                principalTable: "Kursy",
                principalColumn: "IdKursu",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Uczestnictwa_Kursy_KursId",
                table: "Uczestnictwa");

            migrationBuilder.RenameColumn(
                name: "KursId",
                table: "Uczestnictwa",
                newName: "KursIdKursu");

            migrationBuilder.RenameIndex(
                name: "IX_Uczestnictwa_KursId",
                table: "Uczestnictwa",
                newName: "IX_Uczestnictwa_KursIdKursu");

            migrationBuilder.AddColumn<int>(
                name: "IdKursu",
                table: "Uczestnictwa",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Uczestnictwa_Kursy_KursIdKursu",
                table: "Uczestnictwa",
                column: "KursIdKursu",
                principalTable: "Kursy",
                principalColumn: "IdKursu",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
