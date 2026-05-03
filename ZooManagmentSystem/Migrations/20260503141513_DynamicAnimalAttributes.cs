using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZooManagmentSystem.Migrations
{
    /// <inheritdoc />
    public partial class DynamicAnimalAttributes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animals_Enclosures_EnclosureModelid",
                table: "Animals");

            migrationBuilder.DropTable(
                name: "Birds");

            migrationBuilder.DropTable(
                name: "Mammals");

            migrationBuilder.DropIndex(
                name: "IX_Animals_EnclosureModelid",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Enclosures");

            migrationBuilder.DropColumn(
                name: "EnclosureModelid",
                table: "Animals");

            migrationBuilder.AddColumn<int>(
                name: "TypeId",
                table: "Enclosures",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EnclosureId",
                table: "Animals",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Attributes",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnimalId = table.Column<int>(type: "int", nullable: false),
                    AttributeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AttributeValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AttributeType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attributes", x => x.id);
                    table.ForeignKey(
                        name: "FK_Attributes_Animals_AnimalId",
                        column: x => x.AnimalId,
                        principalTable: "Animals",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Enclosures_TypeId",
                table: "Enclosures",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Animals_EnclosureId",
                table: "Animals",
                column: "EnclosureId");

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
                name: "FK_Enclosures_EnclosureTypes_TypeId",
                table: "Enclosures",
                column: "TypeId",
                principalTable: "EnclosureTypes",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animals_Enclosures_EnclosureId",
                table: "Animals");

            migrationBuilder.DropForeignKey(
                name: "FK_Enclosures_EnclosureTypes_TypeId",
                table: "Enclosures");

            migrationBuilder.DropTable(
                name: "Attributes");

            migrationBuilder.DropIndex(
                name: "IX_Enclosures_TypeId",
                table: "Enclosures");

            migrationBuilder.DropIndex(
                name: "IX_Animals_EnclosureId",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "Enclosures");

            migrationBuilder.DropColumn(
                name: "EnclosureId",
                table: "Animals");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Enclosures",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "EnclosureModelid",
                table: "Animals",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Birds",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    CanFly = table.Column<bool>(type: "bit", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HatchedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Birds", x => x.id);
                    table.ForeignKey(
                        name: "FK_Birds_Animals_id",
                        column: x => x.id,
                        principalTable: "Animals",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Mammals",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mammals", x => x.id);
                    table.ForeignKey(
                        name: "FK_Mammals_Animals_id",
                        column: x => x.id,
                        principalTable: "Animals",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

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
