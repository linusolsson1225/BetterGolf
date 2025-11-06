using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BetterGolfASP.Migrations
{
    /// <inheritdoc />
    public partial class AddPricePrecisionAndImgUrls : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OrderRowID",
                table: "OrderRows",
                newName: "OrderRowId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OrderRowId",
                table: "OrderRows",
                newName: "OrderRowID");
        }
    }
}
