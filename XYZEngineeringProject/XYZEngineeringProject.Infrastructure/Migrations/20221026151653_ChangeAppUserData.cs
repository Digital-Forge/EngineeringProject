using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XYZEngineeringProject.Infrastructure.Migrations
{
    public partial class ChangeAppUserData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_AspNetUsers_AssigneeUserId",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_AspNetUsers_AssignerUserId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "PESEL",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "AssignerUserId",
                table: "Tasks",
                newName: "AssignFromUserId");

            migrationBuilder.RenameColumn(
                name: "AssigneeUserId",
                table: "Tasks",
                newName: "AssignToUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Tasks_AssignerUserId",
                table: "Tasks",
                newName: "IX_Tasks_AssignFromUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Tasks_AssigneeUserId",
                table: "Tasks",
                newName: "IX_Tasks_AssignToUserId");

            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "AspNetUsers",
                newName: "Surname");

            migrationBuilder.AddColumn<string>(
                name: "Firstname",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_AspNetUsers_AssignFromUserId",
                table: "Tasks",
                column: "AssignFromUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_AspNetUsers_AssignToUserId",
                table: "Tasks",
                column: "AssignToUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_AspNetUsers_AssignFromUserId",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_AspNetUsers_AssignToUserId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "Firstname",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "AssignToUserId",
                table: "Tasks",
                newName: "AssigneeUserId");

            migrationBuilder.RenameColumn(
                name: "AssignFromUserId",
                table: "Tasks",
                newName: "AssignerUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Tasks_AssignToUserId",
                table: "Tasks",
                newName: "IX_Tasks_AssigneeUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Tasks_AssignFromUserId",
                table: "Tasks",
                newName: "IX_Tasks_AssignerUserId");

            migrationBuilder.RenameColumn(
                name: "Surname",
                table: "AspNetUsers",
                newName: "FullName");

            migrationBuilder.AddColumn<int>(
                name: "PESEL",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_AspNetUsers_AssigneeUserId",
                table: "Tasks",
                column: "AssigneeUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_AspNetUsers_AssignerUserId",
                table: "Tasks",
                column: "AssignerUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
