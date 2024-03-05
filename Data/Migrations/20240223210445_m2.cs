using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoanBook.Data.Migrations
{
    /// <inheritdoc />
    public partial class m2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Book",
                keyColumn: "BookId",
                keyValue: 4);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Book",
                columns: new[] { "BookId", "AuthorName", "PublishedYear", "Quantity", "Title" },
                values: new object[] { 4, "Andrew Chevallier", 2016, 1, "Encyclopedia of Herbal Medicine: 550 Herbs and Remedies for Common Ailments" });
        }
    }
}
