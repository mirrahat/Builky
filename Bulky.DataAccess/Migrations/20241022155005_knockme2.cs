using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BulkyBook.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class knockme2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Company",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Company",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Company",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.InsertData(
                table: "Company",
                columns: new[] { "Id", "City", "Name", "PhoneNumber", "PostalCode", "StreeAddress" },
                values: new object[,]
                {
                    { 11, "Newport", "Mir", "018454594509", "3015", "lonnn" },
                    { 22, "Newport", "Mir", "018454594509", "3015", "lonnn" },
                    { 33, "Newport", "Mir", "018454594509", "3015", "lonnn" },
                    { 111, "Newpo1rt", "Mir1", "0118454594509", "30115", "lonn1n" },
                    { 222, "Newpor2t", "Mir2", "0218454594509", "32015", "l2onnn" },
                    { 333, "Newport3", "Mir3", "0318454594509", "30315", "lonnn3" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Company",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Company",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Company",
                keyColumn: "Id",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "Company",
                keyColumn: "Id",
                keyValue: 111);

            migrationBuilder.DeleteData(
                table: "Company",
                keyColumn: "Id",
                keyValue: 222);

            migrationBuilder.DeleteData(
                table: "Company",
                keyColumn: "Id",
                keyValue: 333);

            migrationBuilder.InsertData(
                table: "Company",
                columns: new[] { "Id", "City", "Name", "PhoneNumber", "PostalCode", "StreeAddress" },
                values: new object[,]
                {
                    { 1, "Newport", "Mir", "018454594509", "3015", "lonnn" },
                    { 2, "Newport", "Mir", "018454594509", "3015", "lonnn" },
                    { 3, "Newport", "Mir", "018454594509", "3015", "lonnn" }
                });
        }
    }
}
