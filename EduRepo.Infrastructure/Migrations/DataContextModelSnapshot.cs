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

            modelBuilder.Entity("EduRepo.Domain.AppUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsStudent")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("TEXT");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("TEXT");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("EduRepo.Domain.Kurs", b =>
                {
                    b.Property<int>("IdKursu")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("CzyZarchiwizowany")
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

                    b.Property<string>("UserNameId")
                        .HasColumnType("TEXT");

                    b.Property<string>("WlascicielId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("IdKursu");

                    b.HasIndex("UserNameId");

                    b.ToTable("Kursy");
                });

            modelBuilder.Entity("EduRepo.Domain.Odpowiedz", b =>
                {
                    b.Property<int>("IdOdpowiedzi")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DataOddania")
                        .HasColumnType("TEXT");

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

                    b.Property<int?>("ZadanieIdZadania")
                        .HasColumnType("INTEGER");

                    b.HasKey("IdOdpowiedzi");

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

                    b.Property<int>("IdZadania")
                        .HasColumnType("INTEGER");

                    b.HasKey("IdPowiadomienia");

                    b.ToTable("Powiadomienia");
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

                    b.Property<int?>("KursIdKursu")
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

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Value")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("EduRepo.Domain.Kurs", b =>
                {
                    b.HasOne("EduRepo.Domain.AppUser", "UserName")
                        .WithMany()
                        .HasForeignKey("UserNameId");

                    b.Navigation("UserName");
                });

            modelBuilder.Entity("EduRepo.Domain.Odpowiedz", b =>
                {
                    b.HasOne("EduRepo.Domain.Zadanie", null)
                        .WithMany("Odpowiedzi")
                        .HasForeignKey("ZadanieIdZadania");
                });

            modelBuilder.Entity("EduRepo.Domain.Zadanie", b =>
                {
                    b.HasOne("EduRepo.Domain.Kurs", null)
                        .WithMany("Zadania")
                        .HasForeignKey("KursIdKursu");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("EduRepo.Domain.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("EduRepo.Domain.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EduRepo.Domain.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("EduRepo.Domain.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EduRepo.Domain.Kurs", b =>
                {
                    b.Navigation("Zadania");
                });

            modelBuilder.Entity("EduRepo.Domain.Zadanie", b =>
                {
                    b.Navigation("Odpowiedzi");
                });
#pragma warning restore 612, 618
        }
    }
}
