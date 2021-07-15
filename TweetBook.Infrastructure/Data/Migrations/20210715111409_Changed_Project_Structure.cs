using Microsoft.EntityFrameworkCore.Migrations;

namespace TweetBook.Data.Migrations
{
    public partial class Changed_Project_Structure : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Tag",
                table: "Tags",
                newName: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Tags",
                newName: "Tag");
        }
    }
}
