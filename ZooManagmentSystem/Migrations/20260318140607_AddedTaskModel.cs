using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZooManagmentSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddedTaskModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EnclosureModelid",
                table: "Animals",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EnclosureModel",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnclosureModel", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Deadline = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false),
                    Category = table.Column<int>(type: "int", nullable: false),
                    Enclosureid = table.Column<int>(type: "int", nullable: true),
                    Animalid = table.Column<int>(type: "int", nullable: true),
                    AssignedEmployeeid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.id);
                    table.ForeignKey(
                        name: "FK_Tasks_Animals_Animalid",
                        column: x => x.Animalid,
                        principalTable: "Animals",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Tasks_Employees_AssignedEmployeeid",
                        column: x => x.AssignedEmployeeid,
                        principalTable: "Employees",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Tasks_EnclosureModel_Enclosureid",
                        column: x => x.Enclosureid,
                        principalTable: "EnclosureModel",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Animals_EnclosureModelid",
                table: "Animals",
                column: "EnclosureModelid");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_Animalid",
                table: "Tasks",
                column: "Animalid");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_AssignedEmployeeid",
                table: "Tasks",
                column: "AssignedEmployeeid");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_Enclosureid",
                table: "Tasks",
                column: "Enclosureid");

            migrationBuilder.AddForeignKey(
                name: "FK_Animals_EnclosureModel_EnclosureModelid",
                table: "Animals",
                column: "EnclosureModelid",
                principalTable: "EnclosureModel",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animals_EnclosureModel_EnclosureModelid",
                table: "Animals");

            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "EnclosureModel");

            migrationBuilder.DropIndex(
                name: "IX_Animals_EnclosureModelid",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "EnclosureModelid",
                table: "Animals");
        }
    }
}
