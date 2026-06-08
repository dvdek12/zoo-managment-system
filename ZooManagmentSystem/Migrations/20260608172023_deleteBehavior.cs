using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZooManagmentSystem.Migrations
{
    /// <inheritdoc />
    public partial class deleteBehavior : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketEntryTypes_EntryTypes_EntryTypeId",
                table: "TicketEntryTypes");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketEntryTypes_EntryTypes_EntryTypeId",
                table: "TicketEntryTypes",
                column: "EntryTypeId",
                principalTable: "EntryTypes",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketEntryTypes_EntryTypes_EntryTypeId",
                table: "TicketEntryTypes");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketEntryTypes_EntryTypes_EntryTypeId",
                table: "TicketEntryTypes",
                column: "EntryTypeId",
                principalTable: "EntryTypes",
                principalColumn: "id");
        }
    }
}
