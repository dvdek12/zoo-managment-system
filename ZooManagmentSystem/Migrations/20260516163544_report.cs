using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZooManagmentSystem.Migrations
{
    /// <inheritdoc />
    public partial class report : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnimalHistoryRaports");

            migrationBuilder.DropTable(
                name: "FeedingPlanRaports");

            migrationBuilder.DropTable(
                name: "WorkFlowRaports");

            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AuthorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.id);
                    table.ForeignKey(
                        name: "FK_Reports_Employees_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Employees",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reports_AuthorId",
                table: "Reports",
                column: "AuthorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reports");

            migrationBuilder.CreateTable(
                name: "AnimalHistoryRaports",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Animalid = table.Column<int>(type: "int", nullable: true),
                    Authorid = table.Column<int>(type: "int", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimalHistoryRaports", x => x.id);
                    table.ForeignKey(
                        name: "FK_AnimalHistoryRaports_Animals_Animalid",
                        column: x => x.Animalid,
                        principalTable: "Animals",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_AnimalHistoryRaports_Employees_Authorid",
                        column: x => x.Authorid,
                        principalTable: "Employees",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "FeedingPlanRaports",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Authorid = table.Column<int>(type: "int", nullable: true),
                    EmployeeAssignedid = table.Column<int>(type: "int", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedingPlanRaports", x => x.id);
                    table.ForeignKey(
                        name: "FK_FeedingPlanRaports_Employees_Authorid",
                        column: x => x.Authorid,
                        principalTable: "Employees",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_FeedingPlanRaports_Employees_EmployeeAssignedid",
                        column: x => x.EmployeeAssignedid,
                        principalTable: "Employees",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "WorkFlowRaports",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeAssignedid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkFlowRaports", x => x.id);
                    table.ForeignKey(
                        name: "FK_WorkFlowRaports_Employees_EmployeeAssignedid",
                        column: x => x.EmployeeAssignedid,
                        principalTable: "Employees",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnimalHistoryRaports_Animalid",
                table: "AnimalHistoryRaports",
                column: "Animalid");

            migrationBuilder.CreateIndex(
                name: "IX_AnimalHistoryRaports_Authorid",
                table: "AnimalHistoryRaports",
                column: "Authorid");

            migrationBuilder.CreateIndex(
                name: "IX_FeedingPlanRaports_Authorid",
                table: "FeedingPlanRaports",
                column: "Authorid");

            migrationBuilder.CreateIndex(
                name: "IX_FeedingPlanRaports_EmployeeAssignedid",
                table: "FeedingPlanRaports",
                column: "EmployeeAssignedid");

            migrationBuilder.CreateIndex(
                name: "IX_WorkFlowRaports_EmployeeAssignedid",
                table: "WorkFlowRaports",
                column: "EmployeeAssignedid");
        }
    }
}
