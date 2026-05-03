using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZooManagmentSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddedTicketEntryTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EntryTypeIds",
                table: "Tickets");

            migrationBuilder.CreateTable(
                name: "TicketEntryTypeModel",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TicketId = table.Column<int>(type: "int", nullable: false),
                    EntryTypeId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    TicketModelid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketEntryTypeModel", x => x.id);
                    table.ForeignKey(
                        name: "FK_TicketEntryTypeModel_Tickets_TicketModelid",
                        column: x => x.TicketModelid,
                        principalTable: "Tickets",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TicketEntryTypeModel_TicketModelid",
                table: "TicketEntryTypeModel",
                column: "TicketModelid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TicketEntryTypeModel");

            migrationBuilder.AddColumn<string>(
                name: "EntryTypeIds",
                table: "Tickets",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
