using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApplicationCore.Migrations
{
    public partial class _23_09_2022 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    floor = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Menu",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menu", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Store",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    phone = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Store", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Slot",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    menu_id = table.Column<int>(type: "int", nullable: false),
                    start = table.Column<TimeSpan>(type: "time", nullable: false),
                    end = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Slot", x => x.id);
                    table.ForeignKey(
                        name: "FK_Slot_Menu",
                        column: x => x.menu_id,
                        principalTable: "Menu",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    role_id = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    email = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    phone = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    image = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.id);
                    table.ForeignKey(
                        name: "FK_User_Role",
                        column: x => x.role_id,
                        principalTable: "Role",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    category_id = table.Column<int>(type: "int", nullable: false),
                    store_id = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    price = table.Column<decimal>(type: "numeric(6,0)", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.id);
                    table.ForeignKey(
                        name: "FK_Product_Category",
                        column: x => x.category_id,
                        principalTable: "Category",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Product_Store",
                        column: x => x.store_id,
                        principalTable: "Store",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Shipper",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    salary = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shipper", x => x.id);
                    table.ForeignKey(
                        name: "FK_Shipper_User",
                        column: x => x.user_id,
                        principalTable: "User",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "MenuDetails",
                columns: table => new
                {
                    product_id = table.Column<int>(type: "int", nullable: false),
                    menu_id = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuDetails", x => new { x.product_id, x.menu_id });
                    table.ForeignKey(
                        name: "FK_MenuDetails_Menu",
                        column: x => x.menu_id,
                        principalTable: "Menu",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_MenuDetails_Product",
                        column: x => x.product_id,
                        principalTable: "Product",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    shipper_id = table.Column<int>(type: "int", nullable: false),
                    location_id = table.Column<int>(type: "int", nullable: false),
                    slot_id = table.Column<int>(type: "int", nullable: false),
                    date = table.Column<DateTime>(type: "date", nullable: false),
                    status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.id);
                    table.ForeignKey(
                        name: "FK_Orders_Location",
                        column: x => x.location_id,
                        principalTable: "Location",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Orders_Shipper",
                        column: x => x.shipper_id,
                        principalTable: "Shipper",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Orders_Slot",
                        column: x => x.slot_id,
                        principalTable: "Slot",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Orders_User",
                        column: x => x.user_id,
                        principalTable: "User",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Shift",
                columns: table => new
                {
                    slot_id = table.Column<int>(type: "int", nullable: false),
                    shipper_id = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shift", x => new { x.slot_id, x.shipper_id });
                    table.ForeignKey(
                        name: "FK_Shift_Shipper",
                        column: x => x.shipper_id,
                        principalTable: "Shipper",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Shift_Slot",
                        column: x => x.slot_id,
                        principalTable: "Slot",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    order_id = table.Column<int>(type: "int", nullable: false),
                    product_id = table.Column<int>(type: "int", nullable: false),
                    price = table.Column<decimal>(type: "numeric(6,0)", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => new { x.order_id, x.product_id });
                    table.ForeignKey(
                        name: "FK_OrderDetails_Orders",
                        column: x => x.order_id,
                        principalTable: "Orders",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_OrderDetails_Product",
                        column: x => x.product_id,
                        principalTable: "Product",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MenuDetails_menu_id",
                table: "MenuDetails",
                column: "menu_id");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_product_id",
                table: "OrderDetails",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_location_id",
                table: "Orders",
                column: "location_id");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_shipper_id",
                table: "Orders",
                column: "shipper_id");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_slot_id",
                table: "Orders",
                column: "slot_id");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_user_id",
                table: "Orders",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Product_category_id",
                table: "Product",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "IX_Product_store_id",
                table: "Product",
                column: "store_id");

            migrationBuilder.CreateIndex(
                name: "IX_Shift_shipper_id",
                table: "Shift",
                column: "shipper_id");

            migrationBuilder.CreateIndex(
                name: "IX_Shipper_user_id",
                table: "Shipper",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Slot_menu_id",
                table: "Slot",
                column: "menu_id");

            migrationBuilder.CreateIndex(
                name: "IX_User_role_id",
                table: "User",
                column: "role_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MenuDetails");

            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "Shift");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.DropTable(
                name: "Shipper");

            migrationBuilder.DropTable(
                name: "Slot");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Store");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Menu");

            migrationBuilder.DropTable(
                name: "Role");
        }
    }
}
