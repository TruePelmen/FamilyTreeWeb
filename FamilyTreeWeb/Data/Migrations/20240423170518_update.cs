using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FamilyTreeWeb.Data.Migrations
{
    /// <inheritdoc />
    public partial class update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_People_Trees_TreeId",
                table: "People");

            migrationBuilder.RenameColumn(
                name: "TreeId",
                table: "People",
                newName: "IdTree");

            migrationBuilder.RenameIndex(
                name: "IX_People_TreeId",
                table: "People",
                newName: "IX_People_IdTree");

            migrationBuilder.AddForeignKey(
                name: "FK_People_Trees_IdTree",
                table: "People",
                column: "IdTree",
                principalTable: "Trees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_People_Trees_IdTree",
                table: "People");

            migrationBuilder.RenameColumn(
                name: "IdTree",
                table: "People",
                newName: "TreeId");

            migrationBuilder.RenameIndex(
                name: "IX_People_IdTree",
                table: "People",
                newName: "IX_People_TreeId");

            migrationBuilder.AddForeignKey(
                name: "FK_People_Trees_TreeId",
                table: "People",
                column: "TreeId",
                principalTable: "Trees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
