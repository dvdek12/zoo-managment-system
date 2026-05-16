using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZooManagmentSystem.Migrations
{
    /// <inheritdoc />
    public partial class clientticketrelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Tickets_ClientId",
                table: "Tickets",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Clients_ClientId",
                table: "Tickets",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Clients_ClientId",
                table: "Tickets");

            migrationBuilder.DropIndex(
                name: "IX_Tickets_ClientId",
                table: "Tickets");
        }
    }
}
