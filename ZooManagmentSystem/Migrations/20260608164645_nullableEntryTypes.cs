using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZooManagmentSystem.Migrations
{
    /// <inheritdoc />
    public partial class nullableEntryTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animals_Enclosures_EnclosureId",
                table: "Animals");

            migrationBuilder.DropForeignKey(
                name: "FK_Enclosures_EnclosureTypes_TypeId",
                table: "Enclosures");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_TaskCategories_CategoryId",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketEntryTypes_EntryTypes_EntryTypeId",
                table: "TicketEntryTypes");

            migrationBuilder.AlterColumn<int>(
                name: "EntryTypeId",
                table: "TicketEntryTypes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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
                name: "FK_Animals_Enclosures_EnclosureId",
                table: "Animals",
                column: "EnclosureId",
                principalTable: "Enclosures",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Animals_Enclosures_EnclosureModelid",
                table: "Animals",
                column: "EnclosureModelid",
                principalTable: "Enclosures",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Enclosures_EnclosureTypes_TypeId",
                table: "Enclosures",
                column: "TypeId",
                principalTable: "EnclosureTypes",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_TaskCategories_CategoryId",
                table: "Tasks",
                column: "CategoryId",
                principalTable: "TaskCategories",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketEntryTypes_EntryTypes_EntryTypeId",
                table: "TicketEntryTypes",
                column: "EntryTypeId",
                principalTable: "EntryTypes",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animals_Enclosures_EnclosureId",
                table: "Animals");

            migrationBuilder.DropForeignKey(
                name: "FK_Animals_Enclosures_EnclosureModelid",
                table: "Animals");

            migrationBuilder.DropForeignKey(
                name: "FK_Enclosures_EnclosureTypes_TypeId",
                table: "Enclosures");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_TaskCategories_CategoryId",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketEntryTypes_EntryTypes_EntryTypeId",
                table: "TicketEntryTypes");

            migrationBuilder.DropIndex(
                name: "IX_Animals_EnclosureModelid",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "EnclosureModelid",
                table: "Animals");

            migrationBuilder.AlterColumn<int>(
                name: "EntryTypeId",
                table: "TicketEntryTypes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Animals_Enclosures_EnclosureId",
                table: "Animals",
                column: "EnclosureId",
                principalTable: "Enclosures",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Enclosures_EnclosureTypes_TypeId",
                table: "Enclosures",
                column: "TypeId",
                principalTable: "EnclosureTypes",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_TaskCategories_CategoryId",
                table: "Tasks",
                column: "CategoryId",
                principalTable: "TaskCategories",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketEntryTypes_EntryTypes_EntryTypeId",
                table: "TicketEntryTypes",
                column: "EntryTypeId",
                principalTable: "EntryTypes",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
