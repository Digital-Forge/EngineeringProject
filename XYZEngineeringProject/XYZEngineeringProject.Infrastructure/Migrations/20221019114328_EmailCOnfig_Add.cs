using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace XYZEngineeringProject.Infrastructure.Migrations
{
    public partial class EmailCOnfig_Add : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersToClientsGroups_AspNetUsers_UserId",
                table: "UsersToClientsGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersToClientsGroups_Clients_ClientId",
                table: "UsersToClientsGroups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsersToClientsGroups",
                table: "UsersToClientsGroups");

            migrationBuilder.RenameTable(
                name: "UsersToClientsGroups",
                newName: "UsersToClients");

            migrationBuilder.RenameIndex(
                name: "IX_UsersToClientsGroups_ClientId",
                table: "UsersToClients",
                newName: "IX_UsersToClients_ClientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsersToClients",
                table: "UsersToClients",
                columns: new[] { "UserId", "ClientId" });

            migrationBuilder.CreateTable(
                name: "EmailConfigs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HostSmtp = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EnableSSL = table.Column<bool>(type: "bit", nullable: false),
                    Port = table.Column<int>(type: "int", nullable: false),
                    SenderEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SenderEmailPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SenderName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Header = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Footer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UseStatus = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailConfigs", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_UsersToClients_AspNetUsers_UserId",
                table: "UsersToClients",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersToClients_Clients_ClientId",
                table: "UsersToClients",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsersToClients_AspNetUsers_UserId",
                table: "UsersToClients");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersToClients_Clients_ClientId",
                table: "UsersToClients");

            migrationBuilder.DropTable(
                name: "EmailConfigs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsersToClients",
                table: "UsersToClients");

            migrationBuilder.RenameTable(
                name: "UsersToClients",
                newName: "UsersToClientsGroups");

            migrationBuilder.RenameIndex(
                name: "IX_UsersToClients_ClientId",
                table: "UsersToClientsGroups",
                newName: "IX_UsersToClientsGroups_ClientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsersToClientsGroups",
                table: "UsersToClientsGroups",
                columns: new[] { "UserId", "ClientId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UsersToClientsGroups_AspNetUsers_UserId",
                table: "UsersToClientsGroups",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersToClientsGroups_Clients_ClientId",
                table: "UsersToClientsGroups",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
