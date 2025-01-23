using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Udemy.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addCompanyRecords : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "City", "Name", "PhoneNumber", "State", "StreetAddress", "ZipCode" },
                values: new object[] { 4, "City4", "Name4", "PhoneNumber4", "State4", "StreetAddress4", "ZipCode4" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "City", "Name", "PhoneNumber", "State", "StreetAddress", "ZipCode" },
                values: new object[] { 1, "City1", "Name1", "PhoneNumber1", "State1", "StreetAddress1", "ZipCode1" });
        }
    }
}
