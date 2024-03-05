using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LoanBook.Data.Migrations
{
    /// <inheritdoc />
    public partial class m6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Book",
                columns: new[] { "BookId", "AuthorName", "PublishedYear", "Quantity", "Title" },
                values: new object[,]
                {
                    { 6, "Michael T. Murray M.D. and Joseph Pizzorno", 2012, 3, "The Encyclopedia of Natural Medicine Third Edition" },
                    { 7, "Thomas Easley and Steven Horne", 2016, 1, "The Modern Herbal Dispensatory: A Medicine- Making Guide" },
                    { 8, "Cat Ellis", 2015, 2, "Prepper's Natural Medicine: Life-Saving Herbs, Essential Oils and Natural Remedies for When There is No Doctor" },
                    { 9, "Rosemary Gladstar", 2012, 1, "Rosemary Gladstar's Medicinal Herbs: A Beginner's Guide: 33 Healing Herbs to Know, Grow, and Use" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Book",
                keyColumn: "BookId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Book",
                keyColumn: "BookId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Book",
                keyColumn: "BookId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Book",
                keyColumn: "BookId",
                keyValue: 9);
        }
    }
}
