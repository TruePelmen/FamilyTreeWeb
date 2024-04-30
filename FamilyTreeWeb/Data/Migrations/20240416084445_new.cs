using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FamilyTreeWeb.Data.Migrations
{
    /// <inheritdoc />
    public partial class @new : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Trees",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TreeId",
                table: "People",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_People_TreeId",
                table: "People",
                column: "TreeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropIndex(
                name: "IX_People_TreeId",
                table: "People");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Trees");

            migrationBuilder.DropColumn(
                name: "TreeId",
                table: "People");
        }
    }
}
