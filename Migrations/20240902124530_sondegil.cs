using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestIdentityApp.Migrations
{
    /// <inheritdoc />
    public partial class sondegil : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "öğrenciadsoyad",
                table: "YıldızÖğrenciler",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "öğrenciadsoyad",
                table: "YıldızÖğrenciler");
        }
    }
}
