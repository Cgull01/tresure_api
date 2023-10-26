using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tresure_api.Migrations
{
    /// <inheritdoc />
    public partial class IdentityRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6b286f52-43f5-4758-94bf-b89cc5c84d23", null, "User", "USER" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6b286f52-43f5-4758-94bf-b89cc5c84d23");
        }
    }
}
