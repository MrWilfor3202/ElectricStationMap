using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElectricStationMap.Migrations
{
    /// <inheritdoc />
    public partial class _init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IconUrl",
                table: "Requirements");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Requirements");

            migrationBuilder.AddColumn<int>(
                name: "Distance",
                table: "Requirements",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IconId",
                table: "Requirements",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Icons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    URL = table.Column<string>(type: "TEXT", nullable: false),
                    BuildingType = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Icons", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Requirements_IconId",
                table: "Requirements",
                column: "IconId");

            migrationBuilder.AddForeignKey(
                name: "FK_Requirements_Icons_IconId",
                table: "Requirements",
                column: "IconId",
                principalTable: "Icons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requirements_Icons_IconId",
                table: "Requirements");

            migrationBuilder.DropTable(
                name: "Icons");

            migrationBuilder.DropIndex(
                name: "IX_Requirements_IconId",
                table: "Requirements");

            migrationBuilder.DropColumn(
                name: "Distance",
                table: "Requirements");

            migrationBuilder.DropColumn(
                name: "IconId",
                table: "Requirements");

            migrationBuilder.AddColumn<string>(
                name: "IconUrl",
                table: "Requirements",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Requirements",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
