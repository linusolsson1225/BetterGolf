using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BetterGolfASP.Migrations
{
    /// <inheritdoc />
    public partial class AddImageUrlToGolfClub2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "GolfClubs",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "GolfClubs");
        }
    }
}
