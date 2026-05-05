using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZooManagmentSystem.Migrations
{
    /// <inheritdoc />
    public partial class changedAttributesStructure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animals_Enclosures_EnclosureId",
                table: "Animals");

            migrationBuilder.DropForeignKey(
                name: "FK_Attributes_Animals_AnimalId",
                table: "Attributes");

            migrationBuilder.DropIndex(
                name: "IX_Attributes_AnimalId",
                table: "Attributes");

            migrationBuilder.DropColumn(
                name: "AnimalId",
                table: "Attributes");

            migrationBuilder.DropColumn(
                name: "AttributeValue",
                table: "Attributes");

            migrationBuilder.AddColumn<int>(
                name: "AnimalTypeId",
                table: "Attributes",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EnclosureId",
                table: "Animals",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "AnimalAttributes",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnimalId = table.Column<int>(type: "int", nullable: false),
                    AttributeId = table.Column<int>(type: "int", nullable: false),
                    AttributeValue = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimalAttributes", x => x.id);
                    table.ForeignKey(
                        name: "FK_AnimalAttributes_Animals_AnimalId",
                        column: x => x.AnimalId,
                        principalTable: "Animals",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnimalAttributes_Attributes_AttributeId",
                        column: x => x.AttributeId,
                        principalTable: "Attributes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnimalType",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnimalTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimalType", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attributes_AnimalTypeId",
                table: "Attributes",
                column: "AnimalTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AnimalAttributes_AnimalId",
                table: "AnimalAttributes",
                column: "AnimalId");

            migrationBuilder.CreateIndex(
                name: "IX_AnimalAttributes_AttributeId",
                table: "AnimalAttributes",
                column: "AttributeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Animals_Enclosures_EnclosureId",
                table: "Animals",
                column: "EnclosureId",
                principalTable: "Enclosures",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Attributes_AnimalType_AnimalTypeId",
                table: "Attributes",
                column: "AnimalTypeId",
                principalTable: "AnimalType",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animals_Enclosures_EnclosureId",
                table: "Animals");

            migrationBuilder.DropForeignKey(
                name: "FK_Attributes_AnimalType_AnimalTypeId",
                table: "Attributes");

            migrationBuilder.DropTable(
                name: "AnimalAttributes");

            migrationBuilder.DropTable(
                name: "AnimalType");

            migrationBuilder.DropIndex(
                name: "IX_Attributes_AnimalTypeId",
                table: "Attributes");

            migrationBuilder.DropColumn(
                name: "AnimalTypeId",
                table: "Attributes");

            migrationBuilder.AddColumn<int>(
                name: "AnimalId",
                table: "Attributes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "AttributeValue",
                table: "Attributes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "EnclosureId",
                table: "Animals",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Attributes_AnimalId",
                table: "Attributes",
                column: "AnimalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Animals_Enclosures_EnclosureId",
                table: "Animals",
                column: "EnclosureId",
                principalTable: "Enclosures",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Attributes_Animals_AnimalId",
                table: "Attributes",
                column: "AnimalId",
                principalTable: "Animals",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
