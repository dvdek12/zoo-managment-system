using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZooManagmentSystem.Migrations
{
    /// <inheritdoc />
    public partial class CascadeDeleteOnTickets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketEntryTypeModel_Tickets_TicketModelid",
                table: "TicketEntryTypeModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TicketEntryTypeModel",
                table: "TicketEntryTypeModel");

            migrationBuilder.DropIndex(
                name: "IX_TicketEntryTypeModel_TicketModelid",
                table: "TicketEntryTypeModel");

            migrationBuilder.DropColumn(
                name: "TicketModelid",
                table: "TicketEntryTypeModel");

            migrationBuilder.RenameTable(
                name: "TicketEntryTypeModel",
                newName: "TicketEntryTypes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TicketEntryTypes",
                table: "TicketEntryTypes",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_TicketEntryTypes_EntryTypeId",
                table: "TicketEntryTypes",
                column: "EntryTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketEntryTypes_TicketId",
                table: "TicketEntryTypes",
                column: "TicketId");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketEntryTypes_EntryTypes_EntryTypeId",
                table: "TicketEntryTypes",
                column: "EntryTypeId",
                principalTable: "EntryTypes",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketEntryTypes_Tickets_TicketId",
                table: "TicketEntryTypes",
                column: "TicketId",
                principalTable: "Tickets",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketEntryTypes_EntryTypes_EntryTypeId",
                table: "TicketEntryTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketEntryTypes_Tickets_TicketId",
                table: "TicketEntryTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TicketEntryTypes",
                table: "TicketEntryTypes");

            migrationBuilder.DropIndex(
                name: "IX_TicketEntryTypes_EntryTypeId",
                table: "TicketEntryTypes");

            migrationBuilder.DropIndex(
                name: "IX_TicketEntryTypes_TicketId",
                table: "TicketEntryTypes");

            migrationBuilder.RenameTable(
                name: "TicketEntryTypes",
                newName: "TicketEntryTypeModel");

            migrationBuilder.AddColumn<int>(
                name: "TicketModelid",
                table: "TicketEntryTypeModel",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TicketEntryTypeModel",
                table: "TicketEntryTypeModel",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_TicketEntryTypeModel_TicketModelid",
                table: "TicketEntryTypeModel",
                column: "TicketModelid");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketEntryTypeModel_Tickets_TicketModelid",
                table: "TicketEntryTypeModel",
                column: "TicketModelid",
                principalTable: "Tickets",
                principalColumn: "id");
        }
    }
}
