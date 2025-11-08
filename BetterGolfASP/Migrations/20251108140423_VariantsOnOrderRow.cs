using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BetterGolfASP.Migrations
{
    /// <inheritdoc />
    public partial class VariantsOnOrderRow : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderRows_Orders_OrderID",
                table: "OrderRows");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderRows_Products_ProductID",
                table: "OrderRows");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Customers_CustomerID",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductVariants_Products_ProductID",
                table: "ProductVariants");

            migrationBuilder.RenameColumn(
                name: "ProductID",
                table: "ProductVariants",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "VariantID",
                table: "ProductVariants",
                newName: "VariantId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductVariants_ProductID",
                table: "ProductVariants",
                newName: "IX_ProductVariants_ProductId");

            migrationBuilder.RenameColumn(
                name: "ProductID",
                table: "Products",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "CustomerID",
                table: "Orders",
                newName: "CustomerId");

            migrationBuilder.RenameColumn(
                name: "OrderID",
                table: "Orders",
                newName: "OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_CustomerID",
                table: "Orders",
                newName: "IX_Orders_CustomerId");

            migrationBuilder.RenameColumn(
                name: "ProductID",
                table: "OrderRows",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "OrderID",
                table: "OrderRows",
                newName: "OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderRows_ProductID",
                table: "OrderRows",
                newName: "IX_OrderRows_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderRows_OrderID",
                table: "OrderRows",
                newName: "IX_OrderRows_OrderId");

            migrationBuilder.AddColumn<int>(
                name: "VariantId",
                table: "OrderRows",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderRows_VariantId",
                table: "OrderRows",
                column: "VariantId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderRows_Orders_OrderId",
                table: "OrderRows",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderRows_ProductVariants_VariantId",
                table: "OrderRows",
                column: "VariantId",
                principalTable: "ProductVariants",
                principalColumn: "VariantId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderRows_Products_ProductId",
                table: "OrderRows",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Customers_CustomerId",
                table: "Orders",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariants_Products_ProductId",
                table: "ProductVariants",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderRows_Orders_OrderId",
                table: "OrderRows");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderRows_ProductVariants_VariantId",
                table: "OrderRows");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderRows_Products_ProductId",
                table: "OrderRows");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Customers_CustomerId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductVariants_Products_ProductId",
                table: "ProductVariants");

            migrationBuilder.DropIndex(
                name: "IX_OrderRows_VariantId",
                table: "OrderRows");

            migrationBuilder.DropColumn(
                name: "VariantId",
                table: "OrderRows");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "ProductVariants",
                newName: "ProductID");

            migrationBuilder.RenameColumn(
                name: "VariantId",
                table: "ProductVariants",
                newName: "VariantID");

            migrationBuilder.RenameIndex(
                name: "IX_ProductVariants_ProductId",
                table: "ProductVariants",
                newName: "IX_ProductVariants_ProductID");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "Products",
                newName: "ProductID");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "Orders",
                newName: "CustomerID");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "Orders",
                newName: "OrderID");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                newName: "IX_Orders_CustomerID");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "OrderRows",
                newName: "ProductID");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "OrderRows",
                newName: "OrderID");

            migrationBuilder.RenameIndex(
                name: "IX_OrderRows_ProductId",
                table: "OrderRows",
                newName: "IX_OrderRows_ProductID");

            migrationBuilder.RenameIndex(
                name: "IX_OrderRows_OrderId",
                table: "OrderRows",
                newName: "IX_OrderRows_OrderID");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderRows_Orders_OrderID",
                table: "OrderRows",
                column: "OrderID",
                principalTable: "Orders",
                principalColumn: "OrderID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderRows_Products_ProductID",
                table: "OrderRows",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "ProductID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Customers_CustomerID",
                table: "Orders",
                column: "CustomerID",
                principalTable: "Customers",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariants_Products_ProductID",
                table: "ProductVariants",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "ProductID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
