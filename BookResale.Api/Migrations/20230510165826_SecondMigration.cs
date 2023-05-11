using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookResale.Api.Migrations
{
    /// <inheritdoc />
    public partial class SecondMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "Birthplace", "Born", "Died", "FirstName", "ImageURL", "LastName" },
                values: new object[,]
                {
                    { 1, "America", "March 9, 1984", "", "Mark", "/Images/Beauty/MarkManson.png", "Manson" },
                    { 2, "Hilo, Hawaii, U.S", "April 8, 1947", "", "Robert", "/Images/Beauty/Robert.png", "Kiyosaki" },
                    { 3, "America", "November 7, 1874", "April 5, 1957", "George", "/Images/Beauty/SamuelClason.png", "Samuel Clason" }
                });

            migrationBuilder.InsertData(
                table: "BookStates",
                columns: new[] { "Id", "State" },
                values: new object[,]
                {
                    { 1, "New" },
                    { 2, "Second-hand" },
                    { 3, "Old" }
                });

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1214689745812L,
                column: "AuthorId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 5687421875751L,
                column: "Description",
                value: "A classic personal finance book written by   . The book uses parables set in ancient Babylon to teach timeless lessons about money management and wealth creation.");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 5687421938751L,
                column: "AuthorId",
                value: 2);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "BookStates",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "BookStates",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "BookStates",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1214689745812L,
                column: "AuthorId",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 5687421875751L,
                column: "Description",
                value: "A classic personal finance book written by George S. Clason. The book uses parables set in ancient Babylon to teach timeless lessons about money management and wealth creation.");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 5687421938751L,
                column: "AuthorId",
                value: 1);
        }
    }
}
