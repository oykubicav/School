using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestIdentityApp.Migrations
{
    /// <inheritdoc />
    public partial class id : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_YıldızÖğrenciler_AspNetUsers_ÖğrenciId",
                table: "YıldızÖğrenciler");

            migrationBuilder.DropColumn(
                name: "öğrenciadsoyad",
                table: "YıldızÖğrenciler");

            migrationBuilder.AlterColumn<string>(
                name: "ÖğrenciId",
                table: "YıldızÖğrenciler",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_YıldızÖğrenciler_AspNetUsers_ÖğrenciId",
                table: "YıldızÖğrenciler",
                column: "ÖğrenciId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_YıldızÖğrenciler_AspNetUsers_ÖğrenciId",
                table: "YıldızÖğrenciler");

            migrationBuilder.AlterColumn<string>(
                name: "ÖğrenciId",
                table: "YıldızÖğrenciler",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "öğrenciadsoyad",
                table: "YıldızÖğrenciler",
                type: "text",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_YıldızÖğrenciler_AspNetUsers_ÖğrenciId",
                table: "YıldızÖğrenciler",
                column: "ÖğrenciId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
