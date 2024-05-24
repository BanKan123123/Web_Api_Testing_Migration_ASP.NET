using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web_Api_Testing_Migration.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_categoriesonbook_book_bookId",
                table: "categoriesonbook");

            migrationBuilder.DropForeignKey(
                name: "FK_categoriesonbook_category_categoryId",
                table: "categoriesonbook");

            migrationBuilder.DropPrimaryKey(
                name: "PK_categoriesonbook",
                table: "categoriesonbook");

            migrationBuilder.RenameTable(
                name: "categoriesonbook",
                newName: "categoriesonbooks");

            migrationBuilder.RenameIndex(
                name: "IX_categoriesonbook_categoryId",
                table: "categoriesonbooks",
                newName: "IX_categoriesonbooks_categoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_categoriesonbooks",
                table: "categoriesonbooks",
                columns: new[] { "bookId", "categoryId" });

            migrationBuilder.AddForeignKey(
                name: "FK_categoriesonbooks_book_bookId",
                table: "categoriesonbooks",
                column: "bookId",
                principalTable: "book",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_categoriesonbooks_category_categoryId",
                table: "categoriesonbooks",
                column: "categoryId",
                principalTable: "category",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_categoriesonbooks_book_bookId",
                table: "categoriesonbooks");

            migrationBuilder.DropForeignKey(
                name: "FK_categoriesonbooks_category_categoryId",
                table: "categoriesonbooks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_categoriesonbooks",
                table: "categoriesonbooks");

            migrationBuilder.RenameTable(
                name: "categoriesonbooks",
                newName: "categoriesonbook");

            migrationBuilder.RenameIndex(
                name: "IX_categoriesonbooks_categoryId",
                table: "categoriesonbook",
                newName: "IX_categoriesonbook_categoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_categoriesonbook",
                table: "categoriesonbook",
                columns: new[] { "bookId", "categoryId" });

            migrationBuilder.AddForeignKey(
                name: "FK_categoriesonbook_book_bookId",
                table: "categoriesonbook",
                column: "bookId",
                principalTable: "book",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_categoriesonbook_category_categoryId",
                table: "categoriesonbook",
                column: "categoryId",
                principalTable: "category",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
