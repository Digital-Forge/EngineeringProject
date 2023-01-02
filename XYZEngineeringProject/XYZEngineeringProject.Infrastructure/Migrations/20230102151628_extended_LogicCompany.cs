using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XYZEngineeringProject.Infrastructure.Migrations
{
    public partial class extended_LogicCompany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "LogicCompanies",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreateBy",
                table: "LogicCompanies",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "LogicCompanies",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "UpdateBy",
                table: "LogicCompanies",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "LogicCompanies",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UseStatus",
                table: "LogicCompanies",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "LogicCompanies");

            migrationBuilder.DropColumn(
                name: "CreateBy",
                table: "LogicCompanies");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "LogicCompanies");

            migrationBuilder.DropColumn(
                name: "UpdateBy",
                table: "LogicCompanies");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "LogicCompanies");

            migrationBuilder.DropColumn(
                name: "UseStatus",
                table: "LogicCompanies");
        }
    }
}
