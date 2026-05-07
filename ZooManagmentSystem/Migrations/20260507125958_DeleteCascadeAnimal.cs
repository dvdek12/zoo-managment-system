using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZooManagmentSystem.Migrations
{
    /// <inheritdoc />
    public partial class DeleteCascadeAnimal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnimalHistories_Animals_Animalid",
                table: "AnimalHistories");

            migrationBuilder.RenameColumn(
                name: "Animalid",
                table: "AnimalHistories",
                newName: "AnimalId");

            migrationBuilder.RenameIndex(
                name: "IX_AnimalHistories_Animalid",
                table: "AnimalHistories",
                newName: "IX_AnimalHistories_AnimalId");

            migrationBuilder.AlterColumn<int>(
                name: "AnimalId",
                table: "AnimalHistories",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AnimalHistories_Animals_AnimalId",
                table: "AnimalHistories",
                column: "AnimalId",
                principalTable: "Animals",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnimalHistories_Animals_AnimalId",
                table: "AnimalHistories");

            migrationBuilder.RenameColumn(
                name: "AnimalId",
                table: "AnimalHistories",
                newName: "Animalid");

            migrationBuilder.RenameIndex(
                name: "IX_AnimalHistories_AnimalId",
                table: "AnimalHistories",
                newName: "IX_AnimalHistories_Animalid");

            migrationBuilder.AlterColumn<int>(
                name: "Animalid",
                table: "AnimalHistories",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_AnimalHistories_Animals_Animalid",
                table: "AnimalHistories",
                column: "Animalid",
                principalTable: "Animals",
                principalColumn: "id");
        }
    }
}
