using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class BlogScope : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Blogs",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    BlogName = table.Column<string>(type: "TEXT", nullable: false),
                    Published = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreatedOnDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Content = table.Column<string>(type: "TEXT", nullable: false),
                    BlogId = table.Column<string>(type: "TEXT", nullable: false),
                    AuthorName_First = table.Column<string>(type: "TEXT", nullable: false),
                    AuthorName_Last = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Posts_Blogs_BlogId",
                        column: x => x.BlogId,
                        principalTable: "Blogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Blogs",
                columns: new[] { "Id", "BlogName", "CreatedOnDate", "Published" },
                values: new object[,]
                {
                    { "5b1c2b4d-48c7-402a-80c3-cc796ad49c6b", "First Seeded Blog", new DateTime(2023, 5, 15, 13, 30, 46, 225, DateTimeKind.Utc).AddTicks(2285), true },
                    { "d28888e9-2ba9-473a-a40f-e38cb54f9b35", "Second Seeded Blog", new DateTime(2023, 5, 15, 13, 30, 46, 225, DateTimeKind.Utc).AddTicks(2301), true }
                });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "AuthorName_First", "AuthorName_Last", "BlogId", "Content", "Title" },
                values: new object[,]
                {
                    { "24810dfc-2d94-4cc7-aab5-cdf98b83f0c9", "Diego", "Vega", "d28888e9-2ba9-473a-a40f-e38cb54f9b35", "Test 2 Seeded anonymouspost content", "Second post" },
                    { "493c3228-3444-4a49-9cc0-e8532edc59b2", "Andriy", "Svyryd", "d28888e9-2ba9-473a-a40f-e38cb54f9b35", "Test 1 Seeded content", "First Post Seeded" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Posts_BlogId",
                table: "Posts",
                column: "BlogId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Blogs");
        }
    }
}
