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
                name: "Uzytkownicy",
                columns: table => new
                {
                    IdUzytkownika = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Login = table.Column<string>(type: "TEXT", nullable: false),
                    Haslo = table.Column<string>(type: "TEXT", nullable: false),
                    Imie = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Rola = table.Column<string>(type: "TEXT", nullable: false),
                    Klasa = table.Column<string>(type: "TEXT", nullable: false),
                    Aktywny = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uzytkownicy", x => x.IdUzytkownika);
                });

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
                    CzyZarchiwizowany = table.Column<bool>(type: "INTEGER", nullable: false),
                    IdWlasciciela = table.Column<int>(type: "INTEGER", nullable: false),
                    WlascicielIdUzytkownika = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kursy", x => x.IdKursu);
                    table.ForeignKey(
                        name: "FK_Kursy_Uzytkownicy_WlascicielIdUzytkownika",
                        column: x => x.WlascicielIdUzytkownika,
                        principalTable: "Uzytkownicy",
                        principalColumn: "IdUzytkownika",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Uczestnictwa",
                columns: table => new
                {
                    IdKursu = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    KursIdKursu = table.Column<int>(type: "INTEGER", nullable: false),
                    IdUzytkownika = table.Column<int>(type: "INTEGER", nullable: false),
                    UzytkownikIdUzytkownika = table.Column<int>(type: "INTEGER", nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Uczestnictwa", x => x.IdKursu);
                    table.ForeignKey(
                        name: "FK_Uczestnictwa_Kursy_KursIdKursu",
                        column: x => x.KursIdKursu,
                        principalTable: "Kursy",
                        principalColumn: "IdKursu",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Uczestnictwa_Uzytkownicy_UzytkownikIdUzytkownika",
                        column: x => x.UzytkownikIdUzytkownika,
                        principalTable: "Uzytkownicy",
                        principalColumn: "IdUzytkownika",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Zadania",
                columns: table => new
                {
                    IdZadania = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IdKursu = table.Column<int>(type: "INTEGER", nullable: false),
                    KursIdKursu = table.Column<int>(type: "INTEGER", nullable: false),
                    Nazwa = table.Column<string>(type: "TEXT", nullable: false),
                    Tresc = table.Column<string>(type: "TEXT", nullable: false),
                    TerminOddania = table.Column<DateTime>(type: "TEXT", nullable: false),
                    PlikPomocniczy = table.Column<string>(type: "TEXT", nullable: false),
                    CzyObowiazkowe = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zadania", x => x.IdZadania);
                    table.ForeignKey(
                        name: "FK_Zadania_Kursy_KursIdKursu",
                        column: x => x.KursIdKursu,
                        principalTable: "Kursy",
                        principalColumn: "IdKursu",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Odpowiedzi",
                columns: table => new
                {
                    IdOdpowiedzi = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IdZadania = table.Column<int>(type: "INTEGER", nullable: false),
                    ZadanieIdZadania = table.Column<int>(type: "INTEGER", nullable: false),
                    IdUzytkownika = table.Column<int>(type: "INTEGER", nullable: false),
                    UzytkownikIdUzytkownika = table.Column<int>(type: "INTEGER", nullable: false),
                    DataOddania = table.Column<DateTime>(type: "TEXT", nullable: false),
                    KomentarzNauczyciela = table.Column<string>(type: "TEXT", nullable: false),
                    NazwaPliku = table.Column<string>(type: "TEXT", nullable: false),
                    Ocena = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Odpowiedzi", x => x.IdOdpowiedzi);
                    table.ForeignKey(
                        name: "FK_Odpowiedzi_Uzytkownicy_UzytkownikIdUzytkownika",
                        column: x => x.UzytkownikIdUzytkownika,
                        principalTable: "Uzytkownicy",
                        principalColumn: "IdUzytkownika",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Odpowiedzi_Zadania_ZadanieIdZadania",
                        column: x => x.ZadanieIdZadania,
                        principalTable: "Zadania",
                        principalColumn: "IdZadania",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Powiadomienia",
                columns: table => new
                {
                    IdPowiadomienia = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IdZadania = table.Column<int>(type: "INTEGER", nullable: false),
                    ZadanieIdZadania = table.Column<int>(type: "INTEGER", nullable: false),
                    IdUzytkownika = table.Column<int>(type: "INTEGER", nullable: false),
                    UzytkownikIdUzytkownika = table.Column<int>(type: "INTEGER", nullable: false),
                    DataPowiadomienia = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CzyOdczytane = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Powiadomienia", x => x.IdPowiadomienia);
                    table.ForeignKey(
                        name: "FK_Powiadomienia_Uzytkownicy_UzytkownikIdUzytkownika",
                        column: x => x.UzytkownikIdUzytkownika,
                        principalTable: "Uzytkownicy",
                        principalColumn: "IdUzytkownika",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Powiadomienia_Zadania_ZadanieIdZadania",
                        column: x => x.ZadanieIdZadania,
                        principalTable: "Zadania",
                        principalColumn: "IdZadania",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Kursy_WlascicielIdUzytkownika",
                table: "Kursy",
                column: "WlascicielIdUzytkownika");

            migrationBuilder.CreateIndex(
                name: "IX_Odpowiedzi_UzytkownikIdUzytkownika",
                table: "Odpowiedzi",
                column: "UzytkownikIdUzytkownika");

            migrationBuilder.CreateIndex(
                name: "IX_Odpowiedzi_ZadanieIdZadania",
                table: "Odpowiedzi",
                column: "ZadanieIdZadania");

            migrationBuilder.CreateIndex(
                name: "IX_Powiadomienia_UzytkownikIdUzytkownika",
                table: "Powiadomienia",
                column: "UzytkownikIdUzytkownika");

            migrationBuilder.CreateIndex(
                name: "IX_Powiadomienia_ZadanieIdZadania",
                table: "Powiadomienia",
                column: "ZadanieIdZadania");

            migrationBuilder.CreateIndex(
                name: "IX_Uczestnictwa_KursIdKursu",
                table: "Uczestnictwa",
                column: "KursIdKursu");

            migrationBuilder.CreateIndex(
                name: "IX_Uczestnictwa_UzytkownikIdUzytkownika",
                table: "Uczestnictwa",
                column: "UzytkownikIdUzytkownika");

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
                name: "Uczestnictwa");

            migrationBuilder.DropTable(
                name: "Zadania");

            migrationBuilder.DropTable(
                name: "Kursy");

            migrationBuilder.DropTable(
                name: "Uzytkownicy");
        }
    }
}
