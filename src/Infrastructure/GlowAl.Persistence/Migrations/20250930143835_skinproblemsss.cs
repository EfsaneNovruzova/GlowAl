using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GlowAl.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class skinproblemsss : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_SkinProblems_SkinProblemId",
                table: "Articles");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductProblems_SkinProblems_SkinProblemId",
                table: "ProductProblems");

            migrationBuilder.AlterColumn<string>(
                name: "Severity",
                table: "SkinProblems",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "Medium",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "SkinProblems",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "SkinProblems",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "SkinProblems",
                columns: new[] { "Id", "CreatedAt", "CreatedUser", "Description", "Name", "Severity", "UpdatedAt", "UpdatedUser" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), null, null, "Yağ balansını tənzimləmək üçün məhsullar", "Yağlı dəri", "Medium", null, null },
                    { new Guid("22222222-2222-2222-2222-222222222222"), null, null, "Nəmləndirici məhsullar", "Quru dəri", "Medium", null, null },
                    { new Guid("33333333-3333-3333-3333-333333333333"), null, null, "Akne qarşısı üçün məhsullar", "Akne", "Medium", null, null }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_SkinProblems_SkinProblemId",
                table: "Articles",
                column: "SkinProblemId",
                principalTable: "SkinProblems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductProblems_SkinProblems_SkinProblemId",
                table: "ProductProblems",
                column: "SkinProblemId",
                principalTable: "SkinProblems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_SkinProblems_SkinProblemId",
                table: "Articles");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductProblems_SkinProblems_SkinProblemId",
                table: "ProductProblems");

            migrationBuilder.DeleteData(
                table: "SkinProblems",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "SkinProblems",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"));

            migrationBuilder.DeleteData(
                table: "SkinProblems",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"));

            migrationBuilder.AlterColumn<string>(
                name: "Severity",
                table: "SkinProblems",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldDefaultValue: "Medium");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "SkinProblems",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "SkinProblems",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000);

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_SkinProblems_SkinProblemId",
                table: "Articles",
                column: "SkinProblemId",
                principalTable: "SkinProblems",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductProblems_SkinProblems_SkinProblemId",
                table: "ProductProblems",
                column: "SkinProblemId",
                principalTable: "SkinProblems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
