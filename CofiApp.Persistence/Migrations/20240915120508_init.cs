using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CofiApp.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MenuCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreatedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductMenuCategories",
                columns: table => new
                {
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MenuCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductMenuCategories", x => new { x.ProductId, x.MenuCategoryId });
                    table.ForeignKey(
                        name: "FK_ProductMenuCategories_MenuCategories_MenuCategoryId",
                        column: x => x.MenuCategoryId,
                        principalTable: "MenuCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductMenuCategories_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductOptionGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsRequired = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    AllowMultiple = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreatedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductOptionGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductOptionGroups_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RolePermission",
                columns: table => new
                {
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PermissionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermission", x => new { x.RoleId, x.PermissionId });
                    table.ForeignKey(
                        name: "FK_RolePermission_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolePermission_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Baskets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Baskets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Baskets_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRefreshTokens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpiresOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRefreshTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRole_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRole_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserVerificationTokens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpiresOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TokenType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsUsed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserVerificationTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserVerificationTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductOptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProductOptionGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductOptions_ProductOptionGroups_ProductOptionGroupId",
                        column: x => x.ProductOptionGroupId,
                        principalTable: "ProductOptionGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BasketItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BasketId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    CreatedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasketItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BasketItems_Baskets_BasketId",
                        column: x => x.BasketId,
                        principalTable: "Baskets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BasketItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BasketId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Baskets_BasketId",
                        column: x => x.BasketId,
                        principalTable: "Baskets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BasketItemOptionGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BasketItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductOptionGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasketItemOptionGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BasketItemOptionGroups_BasketItems_BasketItemId",
                        column: x => x.BasketItemId,
                        principalTable: "BasketItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BasketItemOptionGroups_ProductOptionGroups_ProductOptionGroupId",
                        column: x => x.ProductOptionGroupId,
                        principalTable: "ProductOptionGroups",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ProductPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BasketItemOptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BasketItemOptionGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductOptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasketItemOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BasketItemOptions_BasketItemOptionGroups_BasketItemOptionGroupId",
                        column: x => x.BasketItemOptionGroupId,
                        principalTable: "BasketItemOptionGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BasketItemOptions_ProductOptions_ProductOptionId",
                        column: x => x.ProductOptionId,
                        principalTable: "ProductOptions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OrderItemOptionGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductOptionGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductOptionGroupName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ProductOptionGroupIsRequired = table.Column<bool>(type: "bit", nullable: false),
                    ProductOptionGroupAllowMultiple = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItemOptionGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItemOptionGroups_OrderItems_OrderItemId",
                        column: x => x.OrderItemId,
                        principalTable: "OrderItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItemOptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderItemOptionGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductOptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductOptionName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ProductOptionPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItemOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItemOptions_OrderItemOptionGroups_OrderItemOptionGroupId",
                        column: x => x.OrderItemOptionGroupId,
                        principalTable: "OrderItemOptionGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "MenuCategories",
                columns: new[] { "Id", "CreatedOnUtc", "ModifiedOnUtc", "Name" },
                values: new object[,]
                {
                    { new Guid("4e7aa678-4b40-46d9-8027-7cf07c0c4592"), new DateTime(2024, 9, 15, 12, 5, 8, 7, DateTimeKind.Utc).AddTicks(9000), null, "Sıcak Kahveler" },
                    { new Guid("e85a32df-ed18-4577-b90e-8a61d60b9238"), new DateTime(2024, 9, 15, 12, 5, 8, 7, DateTimeKind.Utc).AddTicks(9001), null, "Soğuk Kahveler" },
                    { new Guid("f2df5d81-e7d9-401d-818f-90dd73d23745"), new DateTime(2024, 9, 15, 12, 5, 8, 7, DateTimeKind.Utc).AddTicks(8998), null, "Öne Çıkaranlar" }
                });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "All" },
                    { 2, "GetUsers" },
                    { 3, "GetUserById" },
                    { 4, "CreateUser" },
                    { 5, "UpdateUser" },
                    { 6, "RemoveUser" },
                    { 7, "GetRoles" },
                    { 8, "CreateRole" },
                    { 9, "UpdateRole" },
                    { 10, "RemoveRole" },
                    { 11, "GetRolesPermissions" },
                    { 12, "AssignPermission" },
                    { 13, "AssignUser" },
                    { 14, "GetMenuCategories" },
                    { 15, "GetMenuCategoryById" },
                    { 16, "CreateMenuCategory" },
                    { 17, "UpdateMenuCategory" },
                    { 18, "RemoveMenuCategory" },
                    { 19, "GetProducts" },
                    { 20, "GetProductById" },
                    { 21, "CreateProduct" },
                    { 22, "UpdateProduct" },
                    { 23, "RemoveProduct" },
                    { 24, "UpdateProductMenuCategories" },
                    { 25, "GetProductOptionGroupsWithOptions" },
                    { 26, "CreateProductOptionGroup" },
                    { 27, "UpdateProductOptionGroup" },
                    { 28, "RemoveProductOptionGroup" },
                    { 29, "CreateProductOption" },
                    { 30, "UpdateProductOption" },
                    { 31, "RemoveProductOption" },
                    { 32, "GetActiveBasket" },
                    { 33, "CreateBasketItem" },
                    { 34, "UpdateBasketItem" },
                    { 35, "UpdateBasketItemQuantity" },
                    { 36, "ClearBasket" },
                    { 37, "GetShopOrders" },
                    { 38, "GetShopOrderById" },
                    { 39, "UpdateShopOrderStatus" },
                    { 40, "GetCustomerOrders" },
                    { 41, "GetCustomerOrderById" },
                    { 42, "CreateCustomerOrder" },
                    { 43, "CancelCustomerOrder" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CreatedOnUtc", "DeletedOnUtc", "Description", "ModifiedOnUtc", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("9fc0883a-bf2d-496c-a74b-2479d12c3f1f"), new DateTime(2024, 9, 15, 12, 5, 8, 7, DateTimeKind.Utc).AddTicks(9040), null, null, null, "Latte", 15m },
                    { new Guid("b173d0c5-4dd6-4549-824f-9be3373e6adb"), new DateTime(2024, 9, 15, 12, 5, 8, 7, DateTimeKind.Utc).AddTicks(9045), null, null, null, "Ice Latte", 10m },
                    { new Guid("d4cf9f27-b26a-4505-8778-ad2c542cc79c"), new DateTime(2024, 9, 15, 12, 5, 8, 7, DateTimeKind.Utc).AddTicks(9044), null, null, null, "Ice Americano", 10m },
                    { new Guid("d57f1c2e-343b-4608-b9fb-1886cee23d9b"), new DateTime(2024, 9, 15, 12, 5, 8, 7, DateTimeKind.Utc).AddTicks(9042), null, null, null, "Mocha", 20m }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedOnUtc", "ModifiedOnUtc", "Name" },
                values: new object[] { new Guid("2c323a58-da8d-407e-a4d8-9ea3d15d66b4"), new DateTime(2024, 9, 15, 12, 5, 8, 4, DateTimeKind.Utc).AddTicks(1778), null, "Administrator" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedOnUtc", "DeletedOnUtc", "Email", "EmailConfirmed", "FirstName", "LastName", "ModifiedOnUtc", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed" },
                values: new object[] { new Guid("676fb5db-8315-4592-a06e-ab9720e53aa5"), new DateTime(2024, 9, 15, 12, 5, 8, 7, DateTimeKind.Utc).AddTicks(8808), null, "eneskrdnz28@gmail.com", true, "Enes", "Karadeniz", null, "74B4CF32C649297FD6CE091B6F3F59A83CDD85C718D956B312A590C14D40E20E-86140FFF7D68BE64025CC224D03C1BFB", null, false });

            migrationBuilder.InsertData(
                table: "ProductMenuCategories",
                columns: new[] { "MenuCategoryId", "ProductId" },
                values: new object[,]
                {
                    { new Guid("4e7aa678-4b40-46d9-8027-7cf07c0c4592"), new Guid("9fc0883a-bf2d-496c-a74b-2479d12c3f1f") },
                    { new Guid("f2df5d81-e7d9-401d-818f-90dd73d23745"), new Guid("9fc0883a-bf2d-496c-a74b-2479d12c3f1f") },
                    { new Guid("e85a32df-ed18-4577-b90e-8a61d60b9238"), new Guid("b173d0c5-4dd6-4549-824f-9be3373e6adb") },
                    { new Guid("e85a32df-ed18-4577-b90e-8a61d60b9238"), new Guid("d4cf9f27-b26a-4505-8778-ad2c542cc79c") },
                    { new Guid("4e7aa678-4b40-46d9-8027-7cf07c0c4592"), new Guid("d57f1c2e-343b-4608-b9fb-1886cee23d9b") },
                    { new Guid("f2df5d81-e7d9-401d-818f-90dd73d23745"), new Guid("d57f1c2e-343b-4608-b9fb-1886cee23d9b") }
                });

            migrationBuilder.InsertData(
                table: "ProductOptionGroups",
                columns: new[] { "Id", "CreatedOnUtc", "IsRequired", "ModifiedOnUtc", "Name", "ProductId" },
                values: new object[,]
                {
                    { new Guid("08dd8ba5-2c42-414c-bd30-42abb832d2e6"), new DateTime(2024, 9, 15, 12, 5, 8, 7, DateTimeKind.Utc).AddTicks(9090), true, null, "Boy Seçimi", new Guid("b173d0c5-4dd6-4549-824f-9be3373e6adb") },
                    { new Guid("6d648357-b733-4882-b1a7-1a97ac543b3d"), new DateTime(2024, 9, 15, 12, 5, 8, 7, DateTimeKind.Utc).AddTicks(9088), true, null, "Boy Seçimi", new Guid("d57f1c2e-343b-4608-b9fb-1886cee23d9b") },
                    { new Guid("7381447f-0880-4891-bb6e-281624d27818"), new DateTime(2024, 9, 15, 12, 5, 8, 7, DateTimeKind.Utc).AddTicks(9085), true, null, "Boy Seçimi", new Guid("9fc0883a-bf2d-496c-a74b-2479d12c3f1f") },
                    { new Guid("841154da-db07-4f75-9fa5-8ca2d5070cf9"), new DateTime(2024, 9, 15, 12, 5, 8, 7, DateTimeKind.Utc).AddTicks(9091), true, null, "Boy Seçimi", new Guid("d4cf9f27-b26a-4505-8778-ad2c542cc79c") }
                });

            migrationBuilder.InsertData(
                table: "RolePermission",
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[] { 1, new Guid("2c323a58-da8d-407e-a4d8-9ea3d15d66b4") });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("2c323a58-da8d-407e-a4d8-9ea3d15d66b4"), new Guid("676fb5db-8315-4592-a06e-ab9720e53aa5") });

            migrationBuilder.InsertData(
                table: "ProductOptions",
                columns: new[] { "Id", "CreatedOnUtc", "ModifiedOnUtc", "Name", "Price", "ProductOptionGroupId" },
                values: new object[,]
                {
                    { new Guid("1d84823f-556e-4587-92e5-40abb356b154"), new DateTime(2024, 9, 15, 12, 5, 8, 7, DateTimeKind.Utc).AddTicks(9135), null, "Büyük", 4m, new Guid("841154da-db07-4f75-9fa5-8ca2d5070cf9") },
                    { new Guid("29d141f4-8877-4c1e-82dd-4019bbbbdf91"), new DateTime(2024, 9, 15, 12, 5, 8, 7, DateTimeKind.Utc).AddTicks(9124), null, "Standart", 0m, new Guid("6d648357-b733-4882-b1a7-1a97ac543b3d") },
                    { new Guid("454dcac1-227d-4361-bd5d-cc695bc8930c"), new DateTime(2024, 9, 15, 12, 5, 8, 7, DateTimeKind.Utc).AddTicks(9131), null, "Büyük", 4m, new Guid("08dd8ba5-2c42-414c-bd30-42abb832d2e6") },
                    { new Guid("5a1b873d-3dc7-4174-9021-bd677b0af437"), new DateTime(2024, 9, 15, 12, 5, 8, 7, DateTimeKind.Utc).AddTicks(9130), null, "Orta", 2m, new Guid("08dd8ba5-2c42-414c-bd30-42abb832d2e6") },
                    { new Guid("61b3677e-187f-4d37-9c13-2477302d4410"), new DateTime(2024, 9, 15, 12, 5, 8, 7, DateTimeKind.Utc).AddTicks(9119), null, "Standart", 0m, new Guid("7381447f-0880-4891-bb6e-281624d27818") },
                    { new Guid("771f2cce-e875-41f5-91cf-ad4ca73f9a1e"), new DateTime(2024, 9, 15, 12, 5, 8, 7, DateTimeKind.Utc).AddTicks(9127), null, "Orta", 2m, new Guid("6d648357-b733-4882-b1a7-1a97ac543b3d") },
                    { new Guid("816c45ce-1f9f-43a1-8be7-8249cef1d761"), new DateTime(2024, 9, 15, 12, 5, 8, 7, DateTimeKind.Utc).AddTicks(9128), null, "Büyük", 4m, new Guid("6d648357-b733-4882-b1a7-1a97ac543b3d") },
                    { new Guid("8394efbc-ec9a-4ae4-8f0a-752304966dc6"), new DateTime(2024, 9, 15, 12, 5, 8, 7, DateTimeKind.Utc).AddTicks(9122), null, "Büyük", 4m, new Guid("7381447f-0880-4891-bb6e-281624d27818") },
                    { new Guid("906b6099-2309-498d-86b7-1c30aca1518a"), new DateTime(2024, 9, 15, 12, 5, 8, 7, DateTimeKind.Utc).AddTicks(9121), null, "Orta", 2m, new Guid("7381447f-0880-4891-bb6e-281624d27818") },
                    { new Guid("cf71a397-1dcb-4609-90c5-3bea3e37b2ff"), new DateTime(2024, 9, 15, 12, 5, 8, 7, DateTimeKind.Utc).AddTicks(9129), null, "Standart", 0m, new Guid("08dd8ba5-2c42-414c-bd30-42abb832d2e6") },
                    { new Guid("e891e902-e9a3-4db8-8bac-280b5f6912ae"), new DateTime(2024, 9, 15, 12, 5, 8, 7, DateTimeKind.Utc).AddTicks(9134), null, "Orta", 2m, new Guid("841154da-db07-4f75-9fa5-8ca2d5070cf9") },
                    { new Guid("f984402f-33b3-4fb2-b5b8-473bba0ac7be"), new DateTime(2024, 9, 15, 12, 5, 8, 7, DateTimeKind.Utc).AddTicks(9133), null, "Standart", 0m, new Guid("841154da-db07-4f75-9fa5-8ca2d5070cf9") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BasketItemOptionGroups_BasketItemId",
                table: "BasketItemOptionGroups",
                column: "BasketItemId");

            migrationBuilder.CreateIndex(
                name: "IX_BasketItemOptionGroups_ProductOptionGroupId",
                table: "BasketItemOptionGroups",
                column: "ProductOptionGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_BasketItemOptions_BasketItemOptionGroupId",
                table: "BasketItemOptions",
                column: "BasketItemOptionGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_BasketItemOptions_ProductOptionId",
                table: "BasketItemOptions",
                column: "ProductOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_BasketItems_BasketId",
                table: "BasketItems",
                column: "BasketId");

            migrationBuilder.CreateIndex(
                name: "IX_BasketItems_ProductId",
                table: "BasketItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Baskets_UserId",
                table: "Baskets",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuCategories_Name",
                table: "MenuCategories",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderItemOptionGroups_OrderItemId",
                table: "OrderItemOptionGroups",
                column: "OrderItemId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItemOptions_OrderItemOptionGroupId",
                table: "OrderItemOptions",
                column: "OrderItemOptionGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_BasketId",
                table: "Orders",
                column: "BasketId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductMenuCategories_MenuCategoryId",
                table: "ProductMenuCategories",
                column: "MenuCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOptionGroups_ProductId",
                table: "ProductOptionGroups",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOptions_ProductOptionGroupId",
                table: "ProductOptions",
                column: "ProductOptionGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermission_PermissionId",
                table: "RolePermission",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_Name",
                table: "Roles",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserRefreshTokens_UserId",
                table: "UserRefreshTokens",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_RoleId",
                table: "UserRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserVerificationTokens_UserId",
                table: "UserVerificationTokens",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BasketItemOptions");

            migrationBuilder.DropTable(
                name: "OrderItemOptions");

            migrationBuilder.DropTable(
                name: "ProductMenuCategories");

            migrationBuilder.DropTable(
                name: "RolePermission");

            migrationBuilder.DropTable(
                name: "UserRefreshTokens");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropTable(
                name: "UserVerificationTokens");

            migrationBuilder.DropTable(
                name: "BasketItemOptionGroups");

            migrationBuilder.DropTable(
                name: "ProductOptions");

            migrationBuilder.DropTable(
                name: "OrderItemOptionGroups");

            migrationBuilder.DropTable(
                name: "MenuCategories");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "BasketItems");

            migrationBuilder.DropTable(
                name: "ProductOptionGroups");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Baskets");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
