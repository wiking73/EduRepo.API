using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduRepo.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kursy",
                columns: table => new
                {
                    IdKursu = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nazwa = table.Column<string>(type: "TEXT", nullable: false),
                    OpisKursu = table.Column<string>(type: "TEXT", nullable: false),
                    RokAkademicki = table.Column<string>(type: "TEXT", nullable: false),
                    Klasa = table.Column<string>(type: "TEXT", nullable: false),
                    CzyZarchiwizowany = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kursy", x => x.IdKursu);
                });

            migrationBuilder.CreateTable(
                name: "Powiadomienia",
                columns: table => new
                {
                    IdPowiadomienia = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IdZadania = table.Column<int>(type: "INTEGER", nullable: false),
                    DataPowiadomienia = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CzyOdczytane = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Powiadomienia", x => x.IdPowiadomienia);
                });

            migrationBuilder.CreateTable(
                name: "Zadania",
                columns: table => new
                {
                    IdZadania = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IdKursu = table.Column<int>(type: "INTEGER", nullable: false),
                    Nazwa = table.Column<string>(type: "TEXT", nullable: false),
                    Tresc = table.Column<string>(type: "TEXT", nullable: false),
                    TerminOddania = table.Column<DateTime>(type: "TEXT", nullable: false),
                    PlikPomocniczy = table.Column<string>(type: "TEXT", nullable: false),
                    CzyObowiazkowe = table.Column<bool>(type: "INTEGER", nullable: false),
                    KursIdKursu = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zadania", x => x.IdZadania);
                    table.ForeignKey(
                        name: "FK_Zadania_Kursy_KursIdKursu",
                        column: x => x.KursIdKursu,
                        principalTable: "Kursy",
                        principalColumn: "IdKursu");
                });

            migrationBuilder.CreateTable(
                name: "Odpowiedzi",
                columns: table => new
                {
                    IdOdpowiedzi = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IdZadania = table.Column<int>(type: "INTEGER", nullable: false),
                    DataOddania = table.Column<DateTime>(type: "TEXT", nullable: false),
                    KomentarzNauczyciela = table.Column<string>(type: "TEXT", nullable: false),
                    NazwaPliku = table.Column<string>(type: "TEXT", nullable: false),
                    Ocena = table.Column<string>(type: "TEXT", nullable: false),
                    ZadanieIdZadania = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Odpowiedzi", x => x.IdOdpowiedzi);
                    table.ForeignKey(
                        name: "FK_Odpowiedzi_Zadania_ZadanieIdZadania",
                        column: x => x.ZadanieIdZadania,
                        principalTable: "Zadania",
                        principalColumn: "IdZadania");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Odpowiedzi_ZadanieIdZadania",
                table: "Odpowiedzi",
                column: "ZadanieIdZadania");

            migrationBuilder.CreateIndex(
                name: "IX_Zadania_KursIdKursu",
                table: "Zadania",
                column: "KursIdKursu");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Odpowiedzi");

            migrationBuilder.DropTable(
                name: "Powiadomienia");

            migrationBuilder.DropTable(
                name: "Zadania");

            migrationBuilder.DropTable(
                name: "Kursy");
        }
    }
}
