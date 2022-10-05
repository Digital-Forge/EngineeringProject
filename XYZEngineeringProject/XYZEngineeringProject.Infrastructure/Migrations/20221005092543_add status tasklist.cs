using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XYZEngineeringProject.Infrastructure.Migrations
{
    public partial class addstatustasklist : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "ListTasks",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "ListTasks");
        }
    }
}
