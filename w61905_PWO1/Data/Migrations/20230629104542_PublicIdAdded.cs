using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace w61905_PWO1.Data.Migrations
{
    /// <inheritdoc />
    public partial class PublicIdAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1a2d6a7a-b4e4-4152-8e1b-4d8e53bad892");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "35c8ca61-5e46-4f7e-a15a-513fc33d648c");

            migrationBuilder.AddColumn<string>(
                name: "PublicId",
                table: "Announcements",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0806e798-15ef-4b87-9e69-80db71743590", null, "Admin", "ADMIN" },
                    { "1411a85f-f86f-4af2-b759-6ca10c97e727", null, "Member", "MEMBER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0806e798-15ef-4b87-9e69-80db71743590");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1411a85f-f86f-4af2-b759-6ca10c97e727");

            migrationBuilder.DropColumn(
                name: "PublicId",
                table: "Announcements");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1a2d6a7a-b4e4-4152-8e1b-4d8e53bad892", null, "Admin", "ADMIN" },
                    { "35c8ca61-5e46-4f7e-a15a-513fc33d648c", null, "Member", "MEMBER" }
                });
        }
    }
}
