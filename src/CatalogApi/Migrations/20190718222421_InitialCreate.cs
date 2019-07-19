using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CatalogApi.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    id = table.Column<long>(type: "smallint", nullable: false),
                    title = table.Column<string>(nullable: false),
                    description = table.Column<string>(nullable: false),
                    image_url = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "sub_category",
                columns: table => new
                {
                    id = table.Column<long>(type: "smallint", nullable: false),
                    title = table.Column<string>(nullable: false),
                    description = table.Column<string>(nullable: false),
                    image_url = table.Column<string>(nullable: false),
                    category_id = table.Column<long>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sub_category", x => x.id);
                    table.ForeignKey(
                        name: "FK_sub_category_Categories_category_id",
                        column: x => x.category_id,
                        principalTable: "Categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "product",
                columns: table => new
                {
                    id = table.Column<string>(type: "uuid", nullable: false),
                    name = table.Column<string>(nullable: false),
                    description = table.Column<string>(nullable: false),
                    main_image_url = table.Column<string>(nullable: false),
                    sub_category_id = table.Column<long>(nullable: false),
                    price = table.Column<decimal>(nullable: false),
                    creation_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product", x => x.id);
                    table.ForeignKey(
                        name: "FK_product_sub_category_sub_category_id",
                        column: x => x.sub_category_id,
                        principalTable: "sub_category",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "review",
                columns: table => new
                {
                    id = table.Column<string>(type: "uuid", nullable: false),
                    author = table.Column<string>(nullable: false),
                    comment = table.Column<string>(nullable: false),
                    score = table.Column<int>(nullable: false),
                    product_id = table.Column<string>(type: "uuid", nullable: false),
                    creation_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_review", x => x.id);
                    table.ForeignKey(
                        name: "FK_review_product_product_id",
                        column: x => x.product_id,
                        principalTable: "product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sub_image",
                columns: table => new
                {
                    id = table.Column<string>(type: "uuid", nullable: false),
                    url = table.Column<string>(nullable: false),
                    product_id = table.Column<string>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sub_image", x => x.id);
                    table.ForeignKey(
                        name: "FK_sub_image_product_product_id",
                        column: x => x.product_id,
                        principalTable: "product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_product_sub_category_id",
                table: "product",
                column: "sub_category_id");

            migrationBuilder.CreateIndex(
                name: "IX_review_product_id",
                table: "review",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_sub_category_category_id",
                table: "sub_category",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "IX_sub_image_product_id",
                table: "sub_image",
                column: "product_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "review");

            migrationBuilder.DropTable(
                name: "sub_image");

            migrationBuilder.DropTable(
                name: "product");

            migrationBuilder.DropTable(
                name: "sub_category");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
