using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZooManagmentSystem.Migrations
{
    /// <inheritdoc />
    public partial class OnDeletCascadeAnimalType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attributes_AnimalType_AnimalTypeId",
                table: "Attributes");

            migrationBuilder.AddForeignKey(
                name: "FK_Attributes_AnimalType_AnimalTypeId",
                table: "Attributes",
                column: "AnimalTypeId",
                principalTable: "AnimalType",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attributes_AnimalType_AnimalTypeId",
                table: "Attributes");

            migrationBuilder.AddForeignKey(
                name: "FK_Attributes_AnimalType_AnimalTypeId",
                table: "Attributes",
                column: "AnimalTypeId",
                principalTable: "AnimalType",
                principalColumn: "id");
        }
    }
}
