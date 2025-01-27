using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Udemy.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class bugFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShopingCarts_AspNetUsers_ApplicationUserUserId",
                table: "ShopingCarts");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserUserId",
                table: "ShopingCarts",
                newName: "ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_ShopingCarts_ApplicationUserUserId",
                table: "ShopingCarts",
                newName: "IX_ShopingCarts_ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShopingCarts_AspNetUsers_ApplicationUserId",
                table: "ShopingCarts",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShopingCarts_AspNetUsers_ApplicationUserId",
                table: "ShopingCarts");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "ShopingCarts",
                newName: "ApplicationUserUserId");

            migrationBuilder.RenameIndex(
                name: "IX_ShopingCarts_ApplicationUserId",
                table: "ShopingCarts",
                newName: "IX_ShopingCarts_ApplicationUserUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShopingCarts_AspNetUsers_ApplicationUserUserId",
                table: "ShopingCarts",
                column: "ApplicationUserUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
