using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestIdentityApp.Migrations
{
    /// <inheritdoc />
    public partial class OdevFile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HomeworkFilePath",
                table: "Ödevler",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Ödevler",
                keyColumn: "Id",
                keyValue: 1,
                column: "HomeworkFilePath",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HomeworkFilePath",
                table: "Ödevler");
        }
    }
}
