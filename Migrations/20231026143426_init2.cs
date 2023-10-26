using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tresure_api.Migrations
{
    /// <inheritdoc />
    public partial class init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Roles_Members_MemberId",
                table: "Roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Roles",
                table: "Roles");

            migrationBuilder.RenameTable(
                name: "Roles",
                newName: "AppRoles");

            migrationBuilder.RenameIndex(
                name: "IX_Roles_MemberId",
                table: "AppRoles",
                newName: "IX_AppRoles_MemberId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppRoles",
                table: "AppRoles",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppRoles_Members_MemberId",
                table: "AppRoles",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppRoles_Members_MemberId",
                table: "AppRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppRoles",
                table: "AppRoles");

            migrationBuilder.RenameTable(
                name: "AppRoles",
                newName: "Roles");

            migrationBuilder.RenameIndex(
                name: "IX_AppRoles_MemberId",
                table: "Roles",
                newName: "IX_Roles_MemberId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Roles",
                table: "Roles",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_Members_MemberId",
                table: "Roles",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "Id");
        }
    }
}
