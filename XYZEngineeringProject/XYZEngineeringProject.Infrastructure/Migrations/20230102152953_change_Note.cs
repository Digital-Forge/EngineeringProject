using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XYZEngineeringProject.Infrastructure.Migrations
{
    public partial class change_Note : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "NoteStatus",
                table: "Note",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<bool>(
                name: "isCompany",
                table: "Note",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isCompany",
                table: "Note");

            migrationBuilder.AlterColumn<int>(
                name: "NoteStatus",
                table: "Note",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
