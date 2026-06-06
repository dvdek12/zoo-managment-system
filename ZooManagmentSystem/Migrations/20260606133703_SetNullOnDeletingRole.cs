using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZooManagmentSystem.Migrations
{
    /// <inheritdoc />
    public partial class SetNullOnDeletingRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Roles_RoleId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Roles_RoleId",
                table: "Tasks");

            migrationBuilder.AddColumn<int>(
                name: "RoleModelid",
                table: "Tasks",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_RoleModelid",
                table: "Tasks",
                column: "RoleModelid");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Roles_RoleId",
                table: "Employees",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Roles_RoleId",
                table: "Tasks",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Roles_RoleModelid",
                table: "Tasks",
                column: "RoleModelid",
                principalTable: "Roles",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Roles_RoleId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Roles_RoleId",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Roles_RoleModelid",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_RoleModelid",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "RoleModelid",
                table: "Tasks");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Roles_RoleId",
                table: "Employees",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Roles_RoleId",
                table: "Tasks",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "id");
        }
    }
}
