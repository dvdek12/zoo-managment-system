using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZooManagmentSystem.Migrations
{
    /// <inheritdoc />
    public partial class fixedEmployeeRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Employees_Supervisorid",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Roles_Roleid",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Animals_Animalid",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Employees_AssignedEmployeeid",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Enclosures_Enclosureid",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Roles_RoleModelid",
                table: "Tasks");

            migrationBuilder.RenameColumn(
                name: "Enclosureid",
                table: "Tasks",
                newName: "EnclosureId");

            migrationBuilder.RenameColumn(
                name: "AssignedEmployeeid",
                table: "Tasks",
                newName: "AssignedEmployeeId");

            migrationBuilder.RenameColumn(
                name: "Animalid",
                table: "Tasks",
                newName: "AnimalId");

            migrationBuilder.RenameColumn(
                name: "RoleModelid",
                table: "Tasks",
                newName: "RoleId");

            migrationBuilder.RenameIndex(
                name: "IX_Tasks_Enclosureid",
                table: "Tasks",
                newName: "IX_Tasks_EnclosureId");

            migrationBuilder.RenameIndex(
                name: "IX_Tasks_AssignedEmployeeid",
                table: "Tasks",
                newName: "IX_Tasks_AssignedEmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_Tasks_Animalid",
                table: "Tasks",
                newName: "IX_Tasks_AnimalId");

            migrationBuilder.RenameIndex(
                name: "IX_Tasks_RoleModelid",
                table: "Tasks",
                newName: "IX_Tasks_RoleId");

            migrationBuilder.RenameColumn(
                name: "Supervisorid",
                table: "Employees",
                newName: "SupervisorId");

            migrationBuilder.RenameColumn(
                name: "Roleid",
                table: "Employees",
                newName: "RoleId");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Employees",
                newName: "Email");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_Supervisorid",
                table: "Employees",
                newName: "IX_Employees_SupervisorId");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_Roleid",
                table: "Employees",
                newName: "IX_Employees_RoleId");

            migrationBuilder.AlterColumn<int>(
                name: "Category",
                table: "Tasks",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<bool>(
                name: "IsManagerial",
                table: "Roles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Employees_SupervisorId",
                table: "Employees",
                column: "SupervisorId",
                principalTable: "Employees",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Roles_RoleId",
                table: "Employees",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Animals_AnimalId",
                table: "Tasks",
                column: "AnimalId",
                principalTable: "Animals",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Employees_AssignedEmployeeId",
                table: "Tasks",
                column: "AssignedEmployeeId",
                principalTable: "Employees",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Enclosures_EnclosureId",
                table: "Tasks",
                column: "EnclosureId",
                principalTable: "Enclosures",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Roles_RoleId",
                table: "Tasks",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Employees_SupervisorId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Roles_RoleId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Animals_AnimalId",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Employees_AssignedEmployeeId",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Enclosures_EnclosureId",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Roles_RoleId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "IsManagerial",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Employees");

            migrationBuilder.RenameColumn(
                name: "EnclosureId",
                table: "Tasks",
                newName: "Enclosureid");

            migrationBuilder.RenameColumn(
                name: "AssignedEmployeeId",
                table: "Tasks",
                newName: "AssignedEmployeeid");

            migrationBuilder.RenameColumn(
                name: "AnimalId",
                table: "Tasks",
                newName: "Animalid");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "Tasks",
                newName: "RoleModelid");

            migrationBuilder.RenameIndex(
                name: "IX_Tasks_EnclosureId",
                table: "Tasks",
                newName: "IX_Tasks_Enclosureid");

            migrationBuilder.RenameIndex(
                name: "IX_Tasks_AssignedEmployeeId",
                table: "Tasks",
                newName: "IX_Tasks_AssignedEmployeeid");

            migrationBuilder.RenameIndex(
                name: "IX_Tasks_AnimalId",
                table: "Tasks",
                newName: "IX_Tasks_Animalid");

            migrationBuilder.RenameIndex(
                name: "IX_Tasks_RoleId",
                table: "Tasks",
                newName: "IX_Tasks_RoleModelid");

            migrationBuilder.RenameColumn(
                name: "SupervisorId",
                table: "Employees",
                newName: "Supervisorid");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "Employees",
                newName: "Roleid");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Employees",
                newName: "Title");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_SupervisorId",
                table: "Employees",
                newName: "IX_Employees_Supervisorid");

            migrationBuilder.RenameIndex(
                name: "IX_Employees_RoleId",
                table: "Employees",
                newName: "IX_Employees_Roleid");

            migrationBuilder.AlterColumn<int>(
                name: "Category",
                table: "Tasks",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Employees_Supervisorid",
                table: "Employees",
                column: "Supervisorid",
                principalTable: "Employees",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Roles_Roleid",
                table: "Employees",
                column: "Roleid",
                principalTable: "Roles",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Animals_Animalid",
                table: "Tasks",
                column: "Animalid",
                principalTable: "Animals",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Employees_AssignedEmployeeid",
                table: "Tasks",
                column: "AssignedEmployeeid",
                principalTable: "Employees",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Enclosures_Enclosureid",
                table: "Tasks",
                column: "Enclosureid",
                principalTable: "Enclosures",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Roles_RoleModelid",
                table: "Tasks",
                column: "RoleModelid",
                principalTable: "Roles",
                principalColumn: "id");
        }
    }
}
