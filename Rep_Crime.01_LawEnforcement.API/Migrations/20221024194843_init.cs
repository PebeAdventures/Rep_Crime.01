using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rep_Crime._01_LawEnforcement.API.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "lawEnforcements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PublicLawEnforcementId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rank = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lawEnforcements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "assignedCrimeEvents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CrimeEventId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LawEnforcementId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_assignedCrimeEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_assignedCrimeEvents_lawEnforcements_LawEnforcementId",
                        column: x => x.LawEnforcementId,
                        principalTable: "lawEnforcements",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_assignedCrimeEvents_LawEnforcementId",
                table: "assignedCrimeEvents",
                column: "LawEnforcementId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "assignedCrimeEvents");

            migrationBuilder.DropTable(
                name: "lawEnforcements");
        }
    }
}
