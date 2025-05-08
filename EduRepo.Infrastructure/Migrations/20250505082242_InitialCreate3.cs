using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduRepo.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Uczestnictwa",
                columns: table => new
                {
                    IdUczestnictwa = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IdKursu = table.Column<int>(type: "INTEGER", nullable: false),
                    WlascicielId = table.Column<string>(type: "TEXT", nullable: false),
                    UserName = table.Column<string>(type: "TEXT", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false),
                    KursIdKursu = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uczestnictwa", x => x.IdUczestnictwa);
                    table.ForeignKey(
                        name: "FK_Uczestnictwa_Kursy_KursIdKursu",
                        column: x => x.KursIdKursu,
                        principalTable: "Kursy",
                        principalColumn: "IdKursu",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Uczestnictwa_KursIdKursu",
                table: "Uczestnictwa",
                column: "KursIdKursu");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Uczestnictwa");
        }
    }
}
