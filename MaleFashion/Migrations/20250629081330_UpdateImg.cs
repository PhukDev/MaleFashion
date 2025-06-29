using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MaleFashion.Migrations
{
    /// <inheritdoc />
    public partial class UpdateImg : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "BlogPosts",
                keyColumn: "Id",
                keyValue: 6,
                column: "ImageUrl",
                value: "/img/blog/blog-6.jpg");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "BlogPosts",
                keyColumn: "Id",
                keyValue: 6,
                column: "ImageUrl",
                value: "~/img/blog/blog-6.jpg");
        }
    }
}
