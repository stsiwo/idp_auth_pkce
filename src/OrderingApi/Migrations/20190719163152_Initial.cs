using Microsoft.EntityFrameworkCore.Migrations;

namespace OrderingApi.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    id = table.Column<string>(type: "uuid", nullable: false),
                    first_name = table.Column<string>(nullable: true),
                    last_name = table.Column<string>(nullable: true),
                    home_address_street = table.Column<string>(nullable: true),
                    home_address_city = table.Column<string>(nullable: true),
                    home_address_state = table.Column<string>(nullable: true),
                    home_address_country = table.Column<string>(nullable: true),
                    home_address_postal_code = table.Column<string>(nullable: true),
                    phone_number = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Cart",
                columns: table => new
                {
                    id = table.Column<string>(type: "uuid", nullable: false),
                    user_id = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cart", x => x.id);
                    table.ForeignKey(
                        name: "FK_Cart_Users_user_id",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    id = table.Column<string>(type: "uuid", nullable: false),
                    status = table.Column<int>(nullable: false),
                    user_id = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.id);
                    table.ForeignKey(
                        name: "FK_Orders_Users_user_id",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    id = table.Column<string>(type: "uuid", nullable: false),
                    name = table.Column<string>(nullable: false),
                    description = table.Column<string>(nullable: false),
                    main_image_url = table.Column<string>(nullable: false),
                    price = table.Column<decimal>(nullable: false),
                    stock = table.Column<int>(nullable: false),
                    available_stock = table.Column<int>(nullable: false),
                    cart_id = table.Column<string>(nullable: false),
                    order_id = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.id);
                    table.ForeignKey(
                        name: "FK_Products_Cart_cart_id",
                        column: x => x.cart_id,
                        principalTable: "Cart",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_Orders_order_id",
                        column: x => x.order_id,
                        principalTable: "Orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cart_user_id",
                table: "Cart",
                column: "user_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_user_id",
                table: "Orders",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Products_cart_id",
                table: "Products",
                column: "cart_id");

            migrationBuilder.CreateIndex(
                name: "IX_Products_order_id",
                table: "Products",
                column: "order_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Cart");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
