using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZooManagmentSystem.Migrations
{
    /// <inheritdoc />
    public partial class Dictionaries : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnimalBreeds");

            migrationBuilder.DropColumn(
                name: "ConditionAdmission",
                table: "AnimalHistories");

            migrationBuilder.RenameColumn(
                name: "Category",
                table: "Tasks",
                newName: "CategoryId");

            migrationBuilder.RenameColumn(
                name: "CategoryName",
                table: "TaskCategories",
                newName: "Category");

            migrationBuilder.AddColumn<int>(
                name: "FoodId",
                table: "Animals",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ConditionId",
                table: "AnimalHistories",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AnimalConditions",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Condition = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimalConditions", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_CategoryId",
                table: "Tasks",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Animals_FoodId",
                table: "Animals",
                column: "FoodId");

            migrationBuilder.CreateIndex(
                name: "IX_AnimalHistories_ConditionId",
                table: "AnimalHistories",
                column: "ConditionId");

            migrationBuilder.AddForeignKey(
                name: "FK_AnimalHistories_AnimalConditions_ConditionId",
                table: "AnimalHistories",
                column: "ConditionId",
                principalTable: "AnimalConditions",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Animals_FoodTypes_FoodId",
                table: "Animals",
                column: "FoodId",
                principalTable: "FoodTypes",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_TaskCategories_CategoryId",
                table: "Tasks",
                column: "CategoryId",
                principalTable: "TaskCategories",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnimalHistories_AnimalConditions_ConditionId",
                table: "AnimalHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_Animals_FoodTypes_FoodId",
                table: "Animals");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_TaskCategories_CategoryId",
                table: "Tasks");

            migrationBuilder.DropTable(
                name: "AnimalConditions");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_CategoryId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Animals_FoodId",
                table: "Animals");

            migrationBuilder.DropIndex(
                name: "IX_AnimalHistories_ConditionId",
                table: "AnimalHistories");

            migrationBuilder.DropColumn(
                name: "FoodId",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "ConditionId",
                table: "AnimalHistories");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Tasks",
                newName: "Category");

            migrationBuilder.RenameColumn(
                name: "Category",
                table: "TaskCategories",
                newName: "CategoryName");

            migrationBuilder.AddColumn<int>(
                name: "ConditionAdmission",
                table: "AnimalHistories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AnimalBreeds",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BreedName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimalBreeds", x => x.id);
                });
        }
    }
}
