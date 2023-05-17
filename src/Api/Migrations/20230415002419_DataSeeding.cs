using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class DataSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AuthorName_First",
                table: "Posts",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AuthorName_Last",
                table: "Posts",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Blogs",
                columns: new[] { "Id", "BlogName", "CreatedOnDate", "Published" },
                values: new object[,]
                {
                    { "5b1c2b4d-48c7-402a-80c3-cc796ad49c6b", "First Seeded Blog", new DateTime(2023, 4, 15, 0, 24, 19, 851, DateTimeKind.Utc).AddTicks(4737), true },
                    { "d28888e9-2ba9-473a-a40f-e38cb54f9b35", "Second Seeded Blog", new DateTime(2023, 4, 15, 0, 24, 19, 851, DateTimeKind.Utc).AddTicks(4763), true }
                });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "AuthorName_First", "AuthorName_Last", "BlogId", "Content", "Title" },
                values: new object[,]
                {
                    { "24810dfc-2d94-4cc7-aab5-cdf98b83f0c9", "Diego", "Vega", "d28888e9-2ba9-473a-a40f-e38cb54f9b35", "Test 2 Seeded anonymouspost content", "Second post" },
                    { "493c3228-3444-4a49-9cc0-e8532edc59b2", "Andriy", "Svyryd", "d28888e9-2ba9-473a-a40f-e38cb54f9b35", "Test 1 Seeded content", "First Post Seeded" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: "5b1c2b4d-48c7-402a-80c3-cc796ad49c6b");

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: "24810dfc-2d94-4cc7-aab5-cdf98b83f0c9");

            migrationBuilder.DeleteData(
                table: "Posts",
                keyColumn: "Id",
                keyValue: "493c3228-3444-4a49-9cc0-e8532edc59b2");

            migrationBuilder.DeleteData(
                table: "Blogs",
                keyColumn: "Id",
                keyValue: "d28888e9-2ba9-473a-a40f-e38cb54f9b35");

            migrationBuilder.DropColumn(
                name: "AuthorName_First",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "AuthorName_Last",
                table: "Posts");
        }
    }
}
