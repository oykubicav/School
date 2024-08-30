using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TestIdentityApp.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Ad = table.Column<string>(type: "text", nullable: false),
                    Soyad = table.Column<string>(type: "text", nullable: false),
                    ImageUrl = table.Column<string>(type: "text", nullable: true),
                    Discriminator = table.Column<string>(type: "character varying(21)", maxLength: 21, nullable: false),
                    ÖğretmenId = table.Column<string>(type: "text", nullable: true),
                    Sınıf = table.Column<string>(type: "text", nullable: true),
                    VeliTelefon = table.Column<string>(type: "text", nullable: true),
                    VeliId = table.Column<string>(type: "text", nullable: true),
                    Öğrenci_ÖğretmenId = table.Column<string>(type: "text", nullable: true),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_AspNetUsers_VeliId",
                        column: x => x.VeliId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_AspNetUsers_Öğrenci_ÖğretmenId",
                        column: x => x.Öğrenci_ÖğretmenId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AspNetUsers_AspNetUsers_ÖğretmenId",
                        column: x => x.ÖğretmenId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Dersler",
                columns: table => new
                {
                    DersId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DersAdi = table.Column<string>(type: "text", nullable: true),
                    Günler = table.Column<int>(type: "integer", nullable: true),
                    ÖğretmenId = table.Column<int>(type: "integer", nullable: true),
                    ÖğretmenId1 = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dersler", x => x.DersId);
                    table.ForeignKey(
                        name: "FK_Dersler_AspNetUsers_ÖğretmenId1",
                        column: x => x.ÖğretmenId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "HikayeÖzetleri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    HikayeAdı = table.Column<string>(type: "text", nullable: true),
                    Özet = table.Column<string>(type: "text", nullable: true),
                    ÖğrenciId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HikayeÖzetleri", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HikayeÖzetleri_AspNetUsers_ÖğrenciId",
                        column: x => x.ÖğrenciId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "YıldızÖğrenciler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Hafta = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ÖğrenciId = table.Column<int>(type: "integer", nullable: true),
                    ÖğrenciId1 = table.Column<string>(type: "text", nullable: true),
                    ImageUrl = table.Column<string>(type: "text", nullable: false),
                    ÖğretmenId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YıldızÖğrenciler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_YıldızÖğrenciler_AspNetUsers_ÖğrenciId1",
                        column: x => x.ÖğrenciId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_YıldızÖğrenciler_AspNetUsers_ÖğretmenId",
                        column: x => x.ÖğretmenId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DersÖğrenci",
                columns: table => new
                {
                    DerslerDersId = table.Column<int>(type: "integer", nullable: false),
                    ÖğrencilerId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DersÖğrenci", x => new { x.DerslerDersId, x.ÖğrencilerId });
                    table.ForeignKey(
                        name: "FK_DersÖğrenci_AspNetUsers_ÖğrencilerId",
                        column: x => x.ÖğrencilerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DersÖğrenci_Dersler_DerslerDersId",
                        column: x => x.DerslerDersId,
                        principalTable: "Dersler",
                        principalColumn: "DersId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ödevler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Başlık = table.Column<string>(type: "text", nullable: true),
                    İçerik = table.Column<string>(type: "text", nullable: true),
                    Tarih = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ÖğretmenId = table.Column<int>(type: "integer", nullable: true),
                    DersId = table.Column<int>(type: "integer", nullable: true),
                    ÖğretmenId1 = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ödevler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ödevler_AspNetUsers_ÖğretmenId1",
                        column: x => x.ÖğretmenId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Ödevler_Dersler_DersId",
                        column: x => x.DersId,
                        principalTable: "Dersler",
                        principalColumn: "DersId");
                });

            migrationBuilder.CreateTable(
                name: "ÖdevÖğrenci",
                columns: table => new
                {
                    SorumluÖğrencilerId = table.Column<string>(type: "text", nullable: false),
                    ÖdevlerId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ÖdevÖğrenci", x => new { x.SorumluÖğrencilerId, x.ÖdevlerId });
                    table.ForeignKey(
                        name: "FK_ÖdevÖğrenci_AspNetUsers_SorumluÖğrencilerId",
                        column: x => x.SorumluÖğrencilerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ÖdevÖğrenci_Ödevler_ÖdevlerId",
                        column: x => x.ÖdevlerId,
                        principalTable: "Ödevler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Dersler",
                columns: new[] { "DersId", "DersAdi", "Günler", "ÖğretmenId", "ÖğretmenId1" },
                values: new object[,]
                {
                    { 1, "Matematik", 3, 1, null },
                    { 2, "Türkçe", 5, 1, null },
                    { 3, "Sosyal Bilgiler", 6, 1, null },
                    { 4, "Fen Bilimleri", 24, 1, null },
                    { 5, "Din Kültürü ve Ahlak Bilgisi", 3, 1, null },
                    { 6, "İngilizce", 18, 1, null },
                    { 7, "Müzik", 1, 1, null },
                    { 8, "Resim", 2, 1, null },
                    { 9, "Beden Eğitimi", 4, 1, null },
                    { 10, "Trafik Güvenliği", 8, 1, null },
                    { 11, "Yurttaşlık ve Demokrasi", 2, 1, null }
                });

            migrationBuilder.InsertData(
                table: "Ödevler",
                columns: new[] { "Id", "Başlık", "DersId", "Tarih", "ÖğretmenId", "ÖğretmenId1", "İçerik" },
                values: new object[] { 1, "Öykü", 1, null, null, null, "oyku" });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_Öğrenci_ÖğretmenId",
                table: "AspNetUsers",
                column: "Öğrenci_ÖğretmenId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ÖğretmenId",
                table: "AspNetUsers",
                column: "ÖğretmenId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_VeliId",
                table: "AspNetUsers",
                column: "VeliId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Dersler_ÖğretmenId1",
                table: "Dersler",
                column: "ÖğretmenId1");

            migrationBuilder.CreateIndex(
                name: "IX_DersÖğrenci_ÖğrencilerId",
                table: "DersÖğrenci",
                column: "ÖğrencilerId");

            migrationBuilder.CreateIndex(
                name: "IX_HikayeÖzetleri_ÖğrenciId",
                table: "HikayeÖzetleri",
                column: "ÖğrenciId");

            migrationBuilder.CreateIndex(
                name: "IX_Ödevler_DersId",
                table: "Ödevler",
                column: "DersId");

            migrationBuilder.CreateIndex(
                name: "IX_Ödevler_ÖğretmenId1",
                table: "Ödevler",
                column: "ÖğretmenId1");

            migrationBuilder.CreateIndex(
                name: "IX_ÖdevÖğrenci_ÖdevlerId",
                table: "ÖdevÖğrenci",
                column: "ÖdevlerId");

            migrationBuilder.CreateIndex(
                name: "IX_YıldızÖğrenciler_ÖğrenciId1",
                table: "YıldızÖğrenciler",
                column: "ÖğrenciId1");

            migrationBuilder.CreateIndex(
                name: "IX_YıldızÖğrenciler_ÖğretmenId",
                table: "YıldızÖğrenciler",
                column: "ÖğretmenId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "DersÖğrenci");

            migrationBuilder.DropTable(
                name: "HikayeÖzetleri");

            migrationBuilder.DropTable(
                name: "ÖdevÖğrenci");

            migrationBuilder.DropTable(
                name: "YıldızÖğrenciler");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Ödevler");

            migrationBuilder.DropTable(
                name: "Dersler");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
