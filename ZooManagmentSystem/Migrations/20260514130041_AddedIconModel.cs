using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZooManagmentSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddedIconModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IconId",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IconId",
                table: "Animals",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Icons",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageData = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Icons", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_IconId",
                table: "Employees",
                column: "IconId");

            migrationBuilder.CreateIndex(
                name: "IX_Animals_IconId",
                table: "Animals",
                column: "IconId");

            migrationBuilder.AddForeignKey(
                name: "FK_Animals_Icons_IconId",
                table: "Animals",
                column: "IconId",
                principalTable: "Icons",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Icons_IconId",
                table: "Employees",
                column: "IconId",
                principalTable: "Icons",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animals_Icons_IconId",
                table: "Animals");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Icons_IconId",
                table: "Employees");

            migrationBuilder.DropTable(
                name: "Icons");

            migrationBuilder.DropIndex(
                name: "IX_Employees_IconId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Animals_IconId",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "IconId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "IconId",
                table: "Animals");
        }
    }
}
