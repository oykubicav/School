﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TestIdentityApp.Data;

#nullable disable

namespace TestIdentityApp.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("DersÖğrenci", b =>
                {
                    b.Property<int>("DerslerDersId")
                        .HasColumnType("integer");

                    b.Property<string>("ÖğrencilerId")
                        .HasColumnType("text");

                    b.HasKey("DerslerDersId", "ÖğrencilerId");

                    b.HasIndex("ÖğrencilerId");

                    b.ToTable("DersÖğrenci");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

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
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .HasColumnType("text");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("TestIdentityApp.Data.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("Ad")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(21)
                        .HasColumnType("character varying(21)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("text");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<string>("Soyad")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasDiscriminator().HasValue("ApplicationUser");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("TestIdentityApp.Data.Models.Ders", b =>
                {
                    b.Property<int?>("DersId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int?>("DersId"));

                    b.Property<string>("DersAdi")
                        .HasColumnType("text");

                    b.Property<int?>("Günler")
                        .HasColumnType("integer");

                    b.Property<int?>("ÖğretmenId")
                        .HasColumnType("integer");

                    b.Property<string>("ÖğretmenId1")
                        .HasColumnType("text");

                    b.HasKey("DersId");

                    b.HasIndex("ÖğretmenId1");

                    b.ToTable("Dersler");

                    b.HasData(
                        new
                        {
                            DersId = 1,
                            DersAdi = "Matematik",
                            Günler = 3,
                            ÖğretmenId = 1
                        },
                        new
                        {
                            DersId = 2,
                            DersAdi = "Türkçe",
                            Günler = 5,
                            ÖğretmenId = 1
                        },
                        new
                        {
                            DersId = 3,
                            DersAdi = "Sosyal Bilgiler",
                            Günler = 6,
                            ÖğretmenId = 1
                        },
                        new
                        {
                            DersId = 4,
                            DersAdi = "Fen Bilimleri",
                            Günler = 24,
                            ÖğretmenId = 1
                        },
                        new
                        {
                            DersId = 5,
                            DersAdi = "Din Kültürü ve Ahlak Bilgisi",
                            Günler = 3,
                            ÖğretmenId = 1
                        },
                        new
                        {
                            DersId = 6,
                            DersAdi = "İngilizce",
                            Günler = 18,
                            ÖğretmenId = 1
                        },
                        new
                        {
                            DersId = 7,
                            DersAdi = "Müzik",
                            Günler = 1,
                            ÖğretmenId = 1
                        },
                        new
                        {
                            DersId = 8,
                            DersAdi = "Resim",
                            Günler = 2,
                            ÖğretmenId = 1
                        },
                        new
                        {
                            DersId = 9,
                            DersAdi = "Beden Eğitimi",
                            Günler = 4,
                            ÖğretmenId = 1
                        },
                        new
                        {
                            DersId = 10,
                            DersAdi = "Trafik Güvenliği",
                            Günler = 8,
                            ÖğretmenId = 1
                        },
                        new
                        {
                            DersId = 11,
                            DersAdi = "Yurttaşlık ve Demokrasi",
                            Günler = 2,
                            ÖğretmenId = 1
                        });
                });

            modelBuilder.Entity("TestIdentityApp.Data.Models.DersIcerik", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int?>("Id"));

                    b.Property<string>("DersAdi")
                        .HasColumnType("text");

                    b.Property<int?>("DersId")
                        .HasColumnType("integer");

                    b.Property<string>("IcerikFileePath")
                        .HasColumnType("text");

                    b.Property<string>("Konu")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("DersIcerikler");
                });

            modelBuilder.Entity("TestIdentityApp.Data.Models.HikayeÖzeti", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int?>("Id"));

                    b.Property<string>("HikayeAdı")
                        .HasColumnType("text");

                    b.Property<string>("Özet")
                        .HasColumnType("text");

                    b.Property<string>("ÖğrenciId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ÖğrenciId");

                    b.ToTable("HikayeÖzetleri");
                });

            modelBuilder.Entity("TestIdentityApp.Data.Models.Ödev", b =>
                {
                    b.Property<int?>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int?>("Id"));

                    b.Property<string>("Başlık")
                        .HasColumnType("text");

                    b.Property<int?>("DersId")
                        .HasColumnType("integer");

                    b.Property<string>("HomeworkFilePath")
                        .HasColumnType("text");

                    b.Property<DateTime?>("Tarih")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("ÖğretmenId")
                        .HasColumnType("integer");

                    b.Property<string>("ÖğretmenId1")
                        .HasColumnType("text");

                    b.Property<string>("İçerik")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("DersId");

                    b.HasIndex("ÖğretmenId1");

                    b.ToTable("Ödevler");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Başlık = "Öykü",
                            DersId = 1,
                            İçerik = "oyku"
                        });
                });

            modelBuilder.Entity("TestIdentityApp.Models.YıldızÖğrenci", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("Hafta")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ÖğrenciId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ÖğretmenId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ÖğrenciId");

                    b.HasIndex("ÖğretmenId");

                    b.ToTable("YıldızÖğrenciler");
                });

            modelBuilder.Entity("ÖdevÖğrenci", b =>
                {
                    b.Property<string>("SorumluÖğrencilerId")
                        .HasColumnType("text");

                    b.Property<int>("ÖdevlerId")
                        .HasColumnType("integer");

                    b.HasKey("SorumluÖğrencilerId", "ÖdevlerId");

                    b.HasIndex("ÖdevlerId");

                    b.ToTable("ÖdevÖğrenci");
                });

            modelBuilder.Entity("TestIdentityApp.Data.Models.Veli", b =>
                {
                    b.HasBaseType("TestIdentityApp.Data.Models.ApplicationUser");

                    b.Property<string>("ÖğretmenId")
                        .HasColumnType("text");

                    b.HasIndex("ÖğretmenId");

                    b.HasDiscriminator().HasValue("Veli");
                });

            modelBuilder.Entity("TestIdentityApp.Data.Models.Öğrenci", b =>
                {
                    b.HasBaseType("TestIdentityApp.Data.Models.ApplicationUser");

                    b.Property<string>("Sınıf")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("VeliId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("VeliTelefon")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ÖğretmenId")
                        .HasColumnType("text");

                    b.HasIndex("VeliId");

                    b.HasIndex("ÖğretmenId");

                    b.ToTable("AspNetUsers", t =>
                        {
                            t.Property("ÖğretmenId")
                                .HasColumnName("Öğrenci_ÖğretmenId");
                        });

                    b.HasDiscriminator().HasValue("Öğrenci");
                });

            modelBuilder.Entity("TestIdentityApp.Data.Models.Öğretmen", b =>
                {
                    b.HasBaseType("TestIdentityApp.Data.Models.ApplicationUser");

                    b.HasDiscriminator().HasValue("Öğretmen");
                });

            modelBuilder.Entity("DersÖğrenci", b =>
                {
                    b.HasOne("TestIdentityApp.Data.Models.Ders", null)
                        .WithMany()
                        .HasForeignKey("DerslerDersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TestIdentityApp.Data.Models.Öğrenci", null)
                        .WithMany()
                        .HasForeignKey("ÖğrencilerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
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
                    b.HasOne("TestIdentityApp.Data.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("TestIdentityApp.Data.Models.ApplicationUser", null)
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

                    b.HasOne("TestIdentityApp.Data.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("TestIdentityApp.Data.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TestIdentityApp.Data.Models.Ders", b =>
                {
                    b.HasOne("TestIdentityApp.Data.Models.Öğretmen", "Öğretmen")
                        .WithMany("Dersler")
                        .HasForeignKey("ÖğretmenId1");

                    b.Navigation("Öğretmen");
                });

            modelBuilder.Entity("TestIdentityApp.Data.Models.HikayeÖzeti", b =>
                {
                    b.HasOne("TestIdentityApp.Data.Models.Öğrenci", null)
                        .WithMany("HikayeÖzetleri")
                        .HasForeignKey("ÖğrenciId");
                });

            modelBuilder.Entity("TestIdentityApp.Data.Models.Ödev", b =>
                {
                    b.HasOne("TestIdentityApp.Data.Models.Ders", null)
                        .WithMany("Ödevler")
                        .HasForeignKey("DersId");

                    b.HasOne("TestIdentityApp.Data.Models.Öğretmen", null)
                        .WithMany("Ödevler")
                        .HasForeignKey("ÖğretmenId1");
                });

            modelBuilder.Entity("TestIdentityApp.Models.YıldızÖğrenci", b =>
                {
                    b.HasOne("TestIdentityApp.Data.Models.Öğrenci", null)
                        .WithMany("YıldızÖğrenciler")
                        .HasForeignKey("ÖğrenciId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TestIdentityApp.Data.Models.Öğretmen", null)
                        .WithMany("YıldızÖğrenciler")
                        .HasForeignKey("ÖğretmenId");
                });

            modelBuilder.Entity("ÖdevÖğrenci", b =>
                {
                    b.HasOne("TestIdentityApp.Data.Models.Öğrenci", null)
                        .WithMany()
                        .HasForeignKey("SorumluÖğrencilerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TestIdentityApp.Data.Models.Ödev", null)
                        .WithMany()
                        .HasForeignKey("ÖdevlerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TestIdentityApp.Data.Models.Veli", b =>
                {
                    b.HasOne("TestIdentityApp.Data.Models.Öğretmen", null)
                        .WithMany("Veliler")
                        .HasForeignKey("ÖğretmenId");
                });

            modelBuilder.Entity("TestIdentityApp.Data.Models.Öğrenci", b =>
                {
                    b.HasOne("TestIdentityApp.Data.Models.Veli", "Veli")
                        .WithMany("Öğrenciler")
                        .HasForeignKey("VeliId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TestIdentityApp.Data.Models.Öğretmen", null)
                        .WithMany("Öğrenciler")
                        .HasForeignKey("ÖğretmenId");

                    b.Navigation("Veli");
                });

            modelBuilder.Entity("TestIdentityApp.Data.Models.Ders", b =>
                {
                    b.Navigation("Ödevler");
                });

            modelBuilder.Entity("TestIdentityApp.Data.Models.Veli", b =>
                {
                    b.Navigation("Öğrenciler");
                });

            modelBuilder.Entity("TestIdentityApp.Data.Models.Öğrenci", b =>
                {
                    b.Navigation("HikayeÖzetleri");

                    b.Navigation("YıldızÖğrenciler");
                });

            modelBuilder.Entity("TestIdentityApp.Data.Models.Öğretmen", b =>
                {
                    b.Navigation("Dersler");

                    b.Navigation("Veliler");

                    b.Navigation("YıldızÖğrenciler");

                    b.Navigation("Ödevler");

                    b.Navigation("Öğrenciler");
                });
#pragma warning restore 612, 618
        }
    }
}
