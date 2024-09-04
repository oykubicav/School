using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestIdentityApp.Migrations
{
    /// <inheritdoc />
    public partial class forth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_YıldızÖğrenciler_AspNetUsers_ÖğrenciId1",
                table: "YıldızÖğrenciler");

            migrationBuilder.DropIndex(
                name: "IX_YıldızÖğrenciler_ÖğrenciId1",
                table: "YıldızÖğrenciler");

            migrationBuilder.DropColumn(
                name: "ÖğrenciId1",
                table: "YıldızÖğrenciler");

            migrationBuilder.AlterColumn<string>(
                name: "ÖğrenciId",
                table: "YıldızÖğrenciler",
                type: "text",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_YıldızÖğrenciler_ÖğrenciId",
                table: "YıldızÖğrenciler",
                column: "ÖğrenciId");

            migrationBuilder.AddForeignKey(
                name: "FK_YıldızÖğrenciler_AspNetUsers_ÖğrenciId",
                table: "YıldızÖğrenciler",
                column: "ÖğrenciId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_YıldızÖğrenciler_AspNetUsers_ÖğrenciId",
                table: "YıldızÖğrenciler");

            migrationBuilder.DropIndex(
                name: "IX_YıldızÖğrenciler_ÖğrenciId",
                table: "YıldızÖğrenciler");

            migrationBuilder.AlterColumn<int>(
                name: "ÖğrenciId",
                table: "YıldızÖğrenciler",
                type: "integer",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ÖğrenciId1",
                table: "YıldızÖğrenciler",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_YıldızÖğrenciler_ÖğrenciId1",
                table: "YıldızÖğrenciler",
                column: "ÖğrenciId1");

            migrationBuilder.AddForeignKey(
                name: "FK_YıldızÖğrenciler_AspNetUsers_ÖğrenciId1",
                table: "YıldızÖğrenciler",
                column: "ÖğrenciId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
