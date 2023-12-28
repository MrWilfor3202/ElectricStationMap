using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ElectricStationMap.Migrations
{
    /// <inheritdoc />
    public partial class _test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Requirements",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Requirements");
        }
    }
}
