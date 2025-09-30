using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GlowAl.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class aiquery : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductName",
                table: "AIQueryHistories",
                newName: "Prompt");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Prompt",
                table: "AIQueryHistories",
                newName: "ProductName");
        }
    }
}
