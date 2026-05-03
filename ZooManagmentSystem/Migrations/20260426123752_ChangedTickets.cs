using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZooManagmentSystem.Migrations
{
    /// <inheritdoc />
    public partial class ChangedTickets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EntryTypes_Tickets_TicketModelid",
                table: "EntryTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Clients_Clientid",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_Clientid",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_EntryTypes_TicketModelid",
                table: "EntryTypes");

            migrationBuilder.DropColumn(
                name: "TicketModelid",
                table: "EntryTypes");

            migrationBuilder.RenameColumn(
                name: "Clientid",
                table: "Tickets",
                newName: "ClientId");

            migrationBuilder.AddColumn<string>(
                name: "EntryTypeIds",
                table: "Tickets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "EntryTypes",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EntryTypeIds",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "EntryTypes");

            migrationBuilder.RenameColumn(
                name: "ClientId",
                table: "Tickets",
                newName: "Clientid");

            migrationBuilder.AddColumn<int>(
                name: "TicketModelid",
                table: "EntryTypes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_Clientid",
                table: "Tickets",
                column: "Clientid");

            migrationBuilder.CreateIndex(
                name: "IX_EntryTypes_TicketModelid",
                table: "EntryTypes",
                column: "TicketModelid");

            migrationBuilder.AddForeignKey(
                name: "FK_EntryTypes_Tickets_TicketModelid",
                table: "EntryTypes",
                column: "TicketModelid",
                principalTable: "Tickets",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Clients_Clientid",
                table: "Tickets",
                column: "Clientid",
                principalTable: "Clients",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
