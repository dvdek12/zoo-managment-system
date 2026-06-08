using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZooManagmentSystem.Migrations
{
    /// <inheritdoc />
    public partial class fixMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animals_Enclosures_EnclosureModelid",
                table: "Animals");

            migrationBuilder.DropIndex(
                name: "IX_Animals_EnclosureModelid",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "EnclosureModelid",
                table: "Animals");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EnclosureModelid",
                table: "Animals",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Animals_EnclosureModelid",
                table: "Animals",
                column: "EnclosureModelid");

            migrationBuilder.AddForeignKey(
                name: "FK_Animals_Enclosures_EnclosureModelid",
                table: "Animals",
                column: "EnclosureModelid",
                principalTable: "Enclosures",
                principalColumn: "id");
        }
    }
}
