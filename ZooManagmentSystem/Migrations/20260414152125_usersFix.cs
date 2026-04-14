using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZooManagmentSystem.Migrations
{
    /// <inheritdoc />
    public partial class usersFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Roles_Roleid",
                table: "Employees");

            migrationBuilder.AlterColumn<int>(
                name: "Roleid",
                table: "Employees",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Roles_Roleid",
                table: "Employees",
                column: "Roleid",
                principalTable: "Roles",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Roles_Roleid",
                table: "Employees");

            migrationBuilder.AlterColumn<int>(
                name: "Roleid",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Roles_Roleid",
                table: "Employees",
                column: "Roleid",
                principalTable: "Roles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
