using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestIdentityApp.Migrations
{
    /// <inheritdoc />
    public partial class KonutoIcerik : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Konu",
                table: "DersIcerikler",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Konu",
                table: "DersIcerikler");
        }
    }
}
