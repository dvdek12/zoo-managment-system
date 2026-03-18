using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZooManagmentSystem.Migrations
{
    /// <inheritdoc />
    public partial class Raports : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animals_EnclosureModel_EnclosureModelid",
                table: "Animals");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_EnclosureModel_Enclosureid",
                table: "Tasks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EnclosureModel",
                table: "EnclosureModel");

            migrationBuilder.RenameTable(
                name: "EnclosureModel",
                newName: "Enclosures");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Enclosures",
                table: "Enclosures",
                column: "id");

            migrationBuilder.CreateTable(
                name: "AnimalHistories",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Animalid = table.Column<int>(type: "int", nullable: true),
                    ConditionAdmission = table.Column<int>(type: "int", nullable: false),
                    Temperature = table.Column<float>(type: "real", nullable: false),
                    Weight = table.Column<float>(type: "real", nullable: false),
                    IsVacinated = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimalHistories", x => x.id);
                    table.ForeignKey(
                        name: "FK_AnimalHistories_Animals_Animalid",
                        column: x => x.Animalid,
                        principalTable: "Animals",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "AnimalHistoryRaports",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Animalid = table.Column<int>(type: "int", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Authorid = table.Column<int>(type: "int", nullable: true)
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
                    EmployeeAssignedid = table.Column<int>(type: "int", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Authorid = table.Column<int>(type: "int", nullable: true)
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

            migrationBuilder.CreateIndex(
                name: "IX_AnimalHistories_Animalid",
                table: "AnimalHistories",
                column: "Animalid");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Animals_Enclosures_EnclosureModelid",
                table: "Animals",
                column: "EnclosureModelid",
                principalTable: "Enclosures",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Enclosures_Enclosureid",
                table: "Tasks",
                column: "Enclosureid",
                principalTable: "Enclosures",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animals_Enclosures_EnclosureModelid",
                table: "Animals");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Enclosures_Enclosureid",
                table: "Tasks");

            migrationBuilder.DropTable(
                name: "AnimalHistories");

            migrationBuilder.DropTable(
                name: "AnimalHistoryRaports");

            migrationBuilder.DropTable(
                name: "FeedingPlanRaports");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Enclosures",
                table: "Enclosures");

            migrationBuilder.RenameTable(
                name: "Enclosures",
                newName: "EnclosureModel");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EnclosureModel",
                table: "EnclosureModel",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Animals_EnclosureModel_EnclosureModelid",
                table: "Animals",
                column: "EnclosureModelid",
                principalTable: "EnclosureModel",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_EnclosureModel_Enclosureid",
                table: "Tasks",
                column: "Enclosureid",
                principalTable: "EnclosureModel",
                principalColumn: "id");
        }
    }
}
