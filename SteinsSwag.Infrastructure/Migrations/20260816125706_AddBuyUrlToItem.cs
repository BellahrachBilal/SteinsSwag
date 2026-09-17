using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SteinsSwag.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddBuyUrlToItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BuyUrl",
                table: "Items",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BuyUrl",
                table: "Items");
        }
    }
}
