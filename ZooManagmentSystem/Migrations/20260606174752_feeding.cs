using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZooManagmentSystem.Migrations
{
    /// <inheritdoc />
    public partial class feeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "AmountPerFeeding",
                table: "Animals",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FeedingEmployeeId",
                table: "Animals",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FeedingsPerDay",
                table: "Animals",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Animals_FeedingEmployeeId",
                table: "Animals",
                column: "FeedingEmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Animals_Employees_FeedingEmployeeId",
                table: "Animals",
                column: "FeedingEmployeeId",
                principalTable: "Employees",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animals_Employees_FeedingEmployeeId",
                table: "Animals");

            migrationBuilder.DropIndex(
                name: "IX_Animals_FeedingEmployeeId",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "AmountPerFeeding",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "FeedingEmployeeId",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "FeedingsPerDay",
                table: "Animals");
        }
    }
}
