using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClassroomManagement.Migrations
{
    /// <inheritdoc />
    public partial class UpdateColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NotificationRecipient_Student_StudentId",
                table: "NotificationRecipient");

            migrationBuilder.DropIndex(
                name: "IX_NotificationRecipient_StudentId",
                table: "NotificationRecipient");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Instructor");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Employee");

            migrationBuilder.RenameColumn(
                name: "StudentId",
                table: "NotificationRecipient",
                newName: "IsRead");

            migrationBuilder.AddColumn<DateTime>(
                name: "ReadAt",
                table: "NotificationRecipient",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "NotificationRecipient",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NotificationType",
                table: "Notification",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Notification",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationRecipient_UserId",
                table: "NotificationRecipient",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_NotificationRecipient_AspNetUsers_UserId",
                table: "NotificationRecipient",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NotificationRecipient_AspNetUsers_UserId",
                table: "NotificationRecipient");

            migrationBuilder.DropIndex(
                name: "IX_NotificationRecipient_UserId",
                table: "NotificationRecipient");

            migrationBuilder.DropColumn(
                name: "ReadAt",
                table: "NotificationRecipient");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "NotificationRecipient");

            migrationBuilder.DropColumn(
                name: "NotificationType",
                table: "Notification");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Notification");

            migrationBuilder.RenameColumn(
                name: "IsRead",
                table: "NotificationRecipient",
                newName: "StudentId");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Student",
                type: "TEXT",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Instructor",
                type: "TEXT",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Employee",
                type: "TEXT",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationRecipient_StudentId",
                table: "NotificationRecipient",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_NotificationRecipient_Student_StudentId",
                table: "NotificationRecipient",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
