using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GlowAl.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class skinproblem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductProblems_CareProducts_ProductId",
                table: "ProductProblems");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductProblems_SkinProblems_ProblemId",
                table: "ProductProblems");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "ProductProblems",
                newName: "SkinProblemId");

            migrationBuilder.RenameColumn(
                name: "ProblemId",
                table: "ProductProblems",
                newName: "CareProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductProblems_ProductId",
                table: "ProductProblems",
                newName: "IX_ProductProblems_SkinProblemId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductProblems_ProblemId",
                table: "ProductProblems",
                newName: "IX_ProductProblems_CareProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductProblems_CareProducts_CareProductId",
                table: "ProductProblems",
                column: "CareProductId",
                principalTable: "CareProducts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductProblems_SkinProblems_SkinProblemId",
                table: "ProductProblems",
                column: "SkinProblemId",
                principalTable: "SkinProblems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductProblems_CareProducts_CareProductId",
                table: "ProductProblems");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductProblems_SkinProblems_SkinProblemId",
                table: "ProductProblems");

            migrationBuilder.RenameColumn(
                name: "SkinProblemId",
                table: "ProductProblems",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "CareProductId",
                table: "ProductProblems",
                newName: "ProblemId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductProblems_SkinProblemId",
                table: "ProductProblems",
                newName: "IX_ProductProblems_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductProblems_CareProductId",
                table: "ProductProblems",
                newName: "IX_ProductProblems_ProblemId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductProblems_CareProducts_ProductId",
                table: "ProductProblems",
                column: "ProductId",
                principalTable: "CareProducts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductProblems_SkinProblems_ProblemId",
                table: "ProductProblems",
                column: "ProblemId",
                principalTable: "SkinProblems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
