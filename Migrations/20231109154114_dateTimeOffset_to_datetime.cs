using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tresure_api.Migrations
{
    /// <inheritdoc />
    public partial class dateTimeOffset_to_datetime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a9e8122d-4c0e-49e6-97a3-00cc9a40a523");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "fa01e8cf-eefd-4f74-8dd9-af59ad459327", null, "User", "USER" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fa01e8cf-eefd-4f74-8dd9-af59ad459327");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a9e8122d-4c0e-49e6-97a3-00cc9a40a523", null, "User", "USER" });
        }
    }
}
