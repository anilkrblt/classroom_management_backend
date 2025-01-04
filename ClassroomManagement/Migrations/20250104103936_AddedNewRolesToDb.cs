using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ClassroomManagement.Migrations
{
    /// <inheritdoc />
    public partial class AddedNewRolesToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "934d80d6-644f-4da7-b89c-8d4dc1f7befe");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f321330a-04c7-4d82-9014-04cf5df124bc");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0ea49c81-ad3a-47f0-a285-786cedf3d9fd", null, "Administrator", "ADMINISTRATOR" },
                    { "73a3d52c-b632-4426-99f0-c3d38da012e8", null, "Student", "STUDENT" },
                    { "e04582f8-a3c5-40cd-9bb6-c1e85c6c5275", null, "Instructor", "INSTRUCTOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0ea49c81-ad3a-47f0-a285-786cedf3d9fd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "73a3d52c-b632-4426-99f0-c3d38da012e8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e04582f8-a3c5-40cd-9bb6-c1e85c6c5275");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "934d80d6-644f-4da7-b89c-8d4dc1f7befe", null, "Administrator", "ADMINISTRATOR" },
                    { "f321330a-04c7-4d82-9014-04cf5df124bc", null, "Manager", "MANAGER" }
                });
        }
    }
}
