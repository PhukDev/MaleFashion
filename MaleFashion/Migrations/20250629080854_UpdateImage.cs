using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MaleFashion.Migrations
{
    /// <inheritdoc />
    public partial class UpdateImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "BlogPosts",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrl",
                value: "/img/blog/blog-1.jpg");

            migrationBuilder.UpdateData(
                table: "BlogPosts",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageUrl",
                value: "/img/blog/blog-2.jpg");

            migrationBuilder.UpdateData(
                table: "BlogPosts",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImageUrl",
                value: "/img/blog/blog-3.jpg");

            migrationBuilder.UpdateData(
                table: "BlogPosts",
                keyColumn: "Id",
                keyValue: 4,
                column: "ImageUrl",
                value: "/img/blog/blog-4.jpg");

            migrationBuilder.UpdateData(
                table: "BlogPosts",
                keyColumn: "Id",
                keyValue: 5,
                column: "ImageUrl",
                value: "/img/blog/blog-5.jpg");

            migrationBuilder.UpdateData(
                table: "BlogPosts",
                keyColumn: "Id",
                keyValue: 7,
                column: "ImageUrl",
                value: "/img/blog/blog-7.jpg");

            migrationBuilder.UpdateData(
                table: "BlogPosts",
                keyColumn: "Id",
                keyValue: 8,
                column: "ImageUrl",
                value: "/img/blog/blog-8.jpg");

            migrationBuilder.UpdateData(
                table: "BlogPosts",
                keyColumn: "Id",
                keyValue: 9,
                column: "ImageUrl",
                value: "/img/blog/blog-9.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrl",
                value: "/img/product/product-1.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageUrl",
                value: "/img/product/product-2.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImageUrl",
                value: "/img/product/product-3.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                column: "ImageUrl",
                value: "/img/product/product-4.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                column: "ImageUrl",
                value: "/img/product/product-5.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                column: "ImageUrl",
                value: "/img/product/product-6.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7,
                column: "ImageUrl",
                value: "/img/product/product-7.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                column: "ImageUrl",
                value: "/img/product/product-8.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9,
                column: "ImageUrl",
                value: "/img/product/product-9.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10,
                column: "ImageUrl",
                value: "/img/product/product-10.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11,
                column: "ImageUrl",
                value: "/img/product/product-11.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12,
                column: "ImageUrl",
                value: "/img/product/product-12.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 13,
                column: "ImageUrl",
                value: "/img/product/product-13.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 14,
                column: "ImageUrl",
                value: "/img/product/product-14.jpg");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "BlogPosts",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrl",
                value: "~/img/blog/blog-1.jpg");

            migrationBuilder.UpdateData(
                table: "BlogPosts",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageUrl",
                value: "~/img/blog/blog-2.jpg");

            migrationBuilder.UpdateData(
                table: "BlogPosts",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImageUrl",
                value: "~/img/blog/blog-3.jpg");

            migrationBuilder.UpdateData(
                table: "BlogPosts",
                keyColumn: "Id",
                keyValue: 4,
                column: "ImageUrl",
                value: "~/img/blog/blog-4.jpg");

            migrationBuilder.UpdateData(
                table: "BlogPosts",
                keyColumn: "Id",
                keyValue: 5,
                column: "ImageUrl",
                value: "~/img/blog/blog-5.jpg");

            migrationBuilder.UpdateData(
                table: "BlogPosts",
                keyColumn: "Id",
                keyValue: 7,
                column: "ImageUrl",
                value: "~/img/blog/blog-7.jpg");

            migrationBuilder.UpdateData(
                table: "BlogPosts",
                keyColumn: "Id",
                keyValue: 8,
                column: "ImageUrl",
                value: "~/img/blog/blog-8.jpg");

            migrationBuilder.UpdateData(
                table: "BlogPosts",
                keyColumn: "Id",
                keyValue: 9,
                column: "ImageUrl",
                value: "~/img/blog/blog-9.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrl",
                value: "~/img/product/product-1.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageUrl",
                value: "~/img/product/product-2.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImageUrl",
                value: "~/img/product/product-3.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                column: "ImageUrl",
                value: "~/img/product/product-4.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                column: "ImageUrl",
                value: "~/img/product/product-5.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                column: "ImageUrl",
                value: "~/img/product/product-6.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7,
                column: "ImageUrl",
                value: "~/img/product/product-7.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                column: "ImageUrl",
                value: "~/img/product/product-8.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9,
                column: "ImageUrl",
                value: "~/img/product/product-9.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10,
                column: "ImageUrl",
                value: "~/img/product/product-10.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11,
                column: "ImageUrl",
                value: "~/img/product/product-11.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12,
                column: "ImageUrl",
                value: "~/img/product/product-12.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 13,
                column: "ImageUrl",
                value: "~/img/product/product-13.jpg");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 14,
                column: "ImageUrl",
                value: "~/img/product/product-14.jpg");
        }
    }
}
