﻿// <auto-generated />
using System;
using EduRepo.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EduRepo.Infrastructure.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.4");

            modelBuilder.Entity("EduRepo.Domain.Kurs", b =>
                {
                    b.Property<int>("IdKursu")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("CzyZarchiwizowany")
                        .HasColumnType("INTEGER");

                    b.Property<int>("IdWlasciciela")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Klasa")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Nazwa")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("OpisKursu")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("RokAkademicki")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("WlascicielIdUzytkownika")
                        .HasColumnType("INTEGER");

                    b.HasKey("IdKursu");

                    b.HasIndex("WlascicielIdUzytkownika");

                    b.ToTable("Kursy");
                });

            modelBuilder.Entity("EduRepo.Domain.Odpowiedz", b =>
                {
                    b.Property<int>("IdOdpowiedzi")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DataOddania")
                        .HasColumnType("TEXT");

                    b.Property<int>("IdUzytkownika")
                        .HasColumnType("INTEGER");

                    b.Property<int>("IdZadania")
                        .HasColumnType("INTEGER");

                    b.Property<string>("KomentarzNauczyciela")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("NazwaPliku")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Ocena")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("UzytkownikIdUzytkownika")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ZadanieIdZadania")
                        .HasColumnType("INTEGER");

                    b.HasKey("IdOdpowiedzi");

                    b.HasIndex("UzytkownikIdUzytkownika");

                    b.HasIndex("ZadanieIdZadania");

                    b.ToTable("Odpowiedzi");
                });

            modelBuilder.Entity("EduRepo.Domain.PowiadomienieBrakOdpowiedzi", b =>
                {
                    b.Property<int>("IdPowiadomienia")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("CzyOdczytane")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DataPowiadomienia")
                        .HasColumnType("TEXT");

                    b.Property<int>("IdUzytkownika")
                        .HasColumnType("INTEGER");

                    b.Property<int>("IdZadania")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UzytkownikIdUzytkownika")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ZadanieIdZadania")
                        .HasColumnType("INTEGER");

                    b.HasKey("IdPowiadomienia");

                    b.HasIndex("UzytkownikIdUzytkownika");

                    b.HasIndex("ZadanieIdZadania");

                    b.ToTable("Powiadomienia");
                });

            modelBuilder.Entity("EduRepo.Domain.Uczestnictwo", b =>
                {
                    b.Property<int>("IdKursu")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("IdUzytkownika")
                        .HasColumnType("INTEGER");

                    b.Property<int>("KursIdKursu")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("UzytkownikIdUzytkownika")
                        .HasColumnType("INTEGER");

                    b.HasKey("IdKursu");

                    b.HasIndex("KursIdKursu");

                    b.HasIndex("UzytkownikIdUzytkownika");

                    b.ToTable("Uczestnictwa");
                });

            modelBuilder.Entity("EduRepo.Domain.Uzytkownik", b =>
                {
                    b.Property<int>("IdUzytkownika")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Aktywny")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Haslo")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Imie")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Klasa")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Rola")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("IdUzytkownika");

                    b.ToTable("Uzytkownicy");
                });

            modelBuilder.Entity("EduRepo.Domain.Zadanie", b =>
                {
                    b.Property<int>("IdZadania")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("CzyObowiazkowe")
                        .HasColumnType("INTEGER");

                    b.Property<int>("IdKursu")
                        .HasColumnType("INTEGER");

                    b.Property<int>("KursIdKursu")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Nazwa")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PlikPomocniczy")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("TerminOddania")
                        .HasColumnType("TEXT");

                    b.Property<string>("Tresc")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("IdZadania");

                    b.HasIndex("KursIdKursu");

                    b.ToTable("Zadania");
                });

            modelBuilder.Entity("EduRepo.Domain.Kurs", b =>
                {
                    b.HasOne("EduRepo.Domain.Uzytkownik", "Wlasciciel")
                        .WithMany("KursyWlasne")
                        .HasForeignKey("WlascicielIdUzytkownika")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Wlasciciel");
                });

            modelBuilder.Entity("EduRepo.Domain.Odpowiedz", b =>
                {
                    b.HasOne("EduRepo.Domain.Uzytkownik", "Uzytkownik")
                        .WithMany("Odpowiedzi")
                        .HasForeignKey("UzytkownikIdUzytkownika")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EduRepo.Domain.Zadanie", "Zadanie")
                        .WithMany("Odpowiedzi")
                        .HasForeignKey("ZadanieIdZadania")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Uzytkownik");

                    b.Navigation("Zadanie");
                });

            modelBuilder.Entity("EduRepo.Domain.PowiadomienieBrakOdpowiedzi", b =>
                {
                    b.HasOne("EduRepo.Domain.Uzytkownik", "Uzytkownik")
                        .WithMany("Powiadomienia")
                        .HasForeignKey("UzytkownikIdUzytkownika")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EduRepo.Domain.Zadanie", "Zadanie")
                        .WithMany("Powiadomienia")
                        .HasForeignKey("ZadanieIdZadania")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Uzytkownik");

                    b.Navigation("Zadanie");
                });

            modelBuilder.Entity("EduRepo.Domain.Uczestnictwo", b =>
                {
                    b.HasOne("EduRepo.Domain.Kurs", "Kurs")
                        .WithMany("Uczestnicy")
                        .HasForeignKey("KursIdKursu")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EduRepo.Domain.Uzytkownik", "Uzytkownik")
                        .WithMany("Uczestnictwa")
                        .HasForeignKey("UzytkownikIdUzytkownika")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Kurs");

                    b.Navigation("Uzytkownik");
                });

            modelBuilder.Entity("EduRepo.Domain.Zadanie", b =>
                {
                    b.HasOne("EduRepo.Domain.Kurs", "Kurs")
                        .WithMany("Zadania")
                        .HasForeignKey("KursIdKursu")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Kurs");
                });

            modelBuilder.Entity("EduRepo.Domain.Kurs", b =>
                {
                    b.Navigation("Uczestnicy");

                    b.Navigation("Zadania");
                });

            modelBuilder.Entity("EduRepo.Domain.Uzytkownik", b =>
                {
                    b.Navigation("KursyWlasne");

                    b.Navigation("Odpowiedzi");

                    b.Navigation("Powiadomienia");

                    b.Navigation("Uczestnictwa");
                });

            modelBuilder.Entity("EduRepo.Domain.Zadanie", b =>
                {
                    b.Navigation("Odpowiedzi");

                    b.Navigation("Powiadomienia");
                });
#pragma warning restore 612, 618
        }
    }
}
