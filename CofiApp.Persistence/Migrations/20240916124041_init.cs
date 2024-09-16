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
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
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
                columns: new[] { "Id", "CreatedOnUtc", "DisplayOrder", "ModifiedOnUtc", "Name" },
                values: new object[,]
                {
                    { new Guid("0b75f0a3-a098-49aa-8f2f-67894aaaaa17"), new DateTime(2024, 9, 16, 12, 40, 40, 708, DateTimeKind.Utc).AddTicks(5741), 2, null, "Sıcak Kahveler" },
                    { new Guid("1a0c9d85-9a56-4e70-be81-6037407e4987"), new DateTime(2024, 9, 16, 12, 40, 40, 708, DateTimeKind.Utc).AddTicks(5751), 3, null, "Soğuk Kahveler" },
                    { new Guid("2d961f13-b7a7-4d79-a863-6331d5eb3f65"), new DateTime(2024, 9, 16, 12, 40, 40, 708, DateTimeKind.Utc).AddTicks(5738), 1, null, "Öne Çıkaranlar" }
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
                    { 19, "UpdateMenuCategoriesDisplayOrder" },
                    { 20, "GetProducts" },
                    { 21, "GetProductById" },
                    { 22, "CreateProduct" },
                    { 23, "UpdateProduct" },
                    { 24, "RemoveProduct" },
                    { 25, "UpdateProductMenuCategories" },
                    { 26, "GetProductOptionGroupsWithOptions" },
                    { 27, "CreateProductOptionGroup" },
                    { 28, "UpdateProductOptionGroup" },
                    { 29, "RemoveProductOptionGroup" },
                    { 30, "CreateProductOption" },
                    { 31, "UpdateProductOption" },
                    { 32, "RemoveProductOption" },
                    { 33, "GetActiveBasket" },
                    { 34, "CreateBasketItem" },
                    { 35, "UpdateBasketItem" },
                    { 36, "UpdateBasketItemQuantity" },
                    { 37, "ClearBasket" },
                    { 38, "GetShopOrders" },
                    { 39, "GetShopOrderById" },
                    { 40, "UpdateShopOrderStatus" },
                    { 41, "GetCustomerOrders" },
                    { 42, "GetCustomerOrderById" },
                    { 43, "CreateCustomerOrder" },
                    { 44, "CancelCustomerOrder" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CreatedOnUtc", "DeletedOnUtc", "Description", "ModifiedOnUtc", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("1e7aea90-f574-4c9f-813f-fea47f22433c"), new DateTime(2024, 9, 16, 12, 40, 40, 708, DateTimeKind.Utc).AddTicks(5795), null, null, null, "Ice Latte", 10m },
                    { new Guid("29d5ad9f-1a84-44a2-b7cf-59b9b8c6ad18"), new DateTime(2024, 9, 16, 12, 40, 40, 708, DateTimeKind.Utc).AddTicks(5790), null, null, null, "Latte", 15m },
                    { new Guid("563e0732-83e9-4715-9b37-ea4d7526758d"), new DateTime(2024, 9, 16, 12, 40, 40, 708, DateTimeKind.Utc).AddTicks(5794), null, null, null, "Ice Americano", 10m },
                    { new Guid("936024fc-d24e-41da-8a05-16302b162ff0"), new DateTime(2024, 9, 16, 12, 40, 40, 708, DateTimeKind.Utc).AddTicks(5793), null, null, null, "Mocha", 20m }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedOnUtc", "ModifiedOnUtc", "Name" },
                values: new object[] { new Guid("7478f32f-75b9-4f61-8141-1343f1f2957e"), new DateTime(2024, 9, 16, 12, 40, 40, 704, DateTimeKind.Utc).AddTicks(6123), null, "Administrator" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedOnUtc", "DeletedOnUtc", "Email", "EmailConfirmed", "FirstName", "LastName", "ModifiedOnUtc", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed" },
                values: new object[] { new Guid("911cb4e8-997e-4dfd-a85b-c8e91cd6fca9"), new DateTime(2024, 9, 16, 12, 40, 40, 708, DateTimeKind.Utc).AddTicks(5479), null, "eneskrdnz28@gmail.com", true, "Enes", "Karadeniz", null, "2989900E22F3B2EB5E62412E941CF157B914B0EE866C39A9D1B8C92825FE1DA1-3DEFFE92324E874ACC59122A1D44D366", null, false });

            migrationBuilder.InsertData(
                table: "ProductMenuCategories",
                columns: new[] { "MenuCategoryId", "ProductId" },
                values: new object[,]
                {
                    { new Guid("1a0c9d85-9a56-4e70-be81-6037407e4987"), new Guid("1e7aea90-f574-4c9f-813f-fea47f22433c") },
                    { new Guid("0b75f0a3-a098-49aa-8f2f-67894aaaaa17"), new Guid("29d5ad9f-1a84-44a2-b7cf-59b9b8c6ad18") },
                    { new Guid("2d961f13-b7a7-4d79-a863-6331d5eb3f65"), new Guid("29d5ad9f-1a84-44a2-b7cf-59b9b8c6ad18") },
                    { new Guid("1a0c9d85-9a56-4e70-be81-6037407e4987"), new Guid("563e0732-83e9-4715-9b37-ea4d7526758d") },
                    { new Guid("0b75f0a3-a098-49aa-8f2f-67894aaaaa17"), new Guid("936024fc-d24e-41da-8a05-16302b162ff0") },
                    { new Guid("2d961f13-b7a7-4d79-a863-6331d5eb3f65"), new Guid("936024fc-d24e-41da-8a05-16302b162ff0") }
                });

            migrationBuilder.InsertData(
                table: "ProductOptionGroups",
                columns: new[] { "Id", "CreatedOnUtc", "IsRequired", "ModifiedOnUtc", "Name", "ProductId" },
                values: new object[,]
                {
                    { new Guid("4c75965d-1977-4449-98be-6ce8fdd4e1d9"), new DateTime(2024, 9, 16, 12, 40, 40, 708, DateTimeKind.Utc).AddTicks(5900), true, null, "Boy Seçimi", new Guid("29d5ad9f-1a84-44a2-b7cf-59b9b8c6ad18") },
                    { new Guid("51a4a541-5c99-45d4-9d32-72a6e92442b0"), new DateTime(2024, 9, 16, 12, 40, 40, 708, DateTimeKind.Utc).AddTicks(5904), true, null, "Boy Seçimi", new Guid("1e7aea90-f574-4c9f-813f-fea47f22433c") },
                    { new Guid("8fad5075-71f4-48ad-bf0e-3a2de9b975b6"), new DateTime(2024, 9, 16, 12, 40, 40, 708, DateTimeKind.Utc).AddTicks(5903), true, null, "Boy Seçimi", new Guid("936024fc-d24e-41da-8a05-16302b162ff0") },
                    { new Guid("f3bb791e-b1a0-4f0b-96a3-8c9dc70e12d1"), new DateTime(2024, 9, 16, 12, 40, 40, 708, DateTimeKind.Utc).AddTicks(5908), true, null, "Boy Seçimi", new Guid("563e0732-83e9-4715-9b37-ea4d7526758d") }
                });

            migrationBuilder.InsertData(
                table: "RolePermission",
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[] { 1, new Guid("7478f32f-75b9-4f61-8141-1343f1f2957e") });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("7478f32f-75b9-4f61-8141-1343f1f2957e"), new Guid("911cb4e8-997e-4dfd-a85b-c8e91cd6fca9") });

            migrationBuilder.InsertData(
                table: "ProductOptions",
                columns: new[] { "Id", "CreatedOnUtc", "ModifiedOnUtc", "Name", "Price", "ProductOptionGroupId" },
                values: new object[,]
                {
                    { new Guid("2d7c3095-d013-44fd-a97c-ef5bc3e82242"), new DateTime(2024, 9, 16, 12, 40, 40, 708, DateTimeKind.Utc).AddTicks(5946), null, "Büyük", 4m, new Guid("8fad5075-71f4-48ad-bf0e-3a2de9b975b6") },
                    { new Guid("34b4e723-ffec-4e0e-aadf-6c9f8cde8d98"), new DateTime(2024, 9, 16, 12, 40, 40, 708, DateTimeKind.Utc).AddTicks(5944), null, "Standart", 0m, new Guid("8fad5075-71f4-48ad-bf0e-3a2de9b975b6") },
                    { new Guid("3c1b7d64-169d-4c6d-b773-59d4ecabea96"), new DateTime(2024, 9, 16, 12, 40, 40, 708, DateTimeKind.Utc).AddTicks(5955), null, "Büyük", 4m, new Guid("f3bb791e-b1a0-4f0b-96a3-8c9dc70e12d1") },
                    { new Guid("41823911-7c34-4558-b339-8193f61a82ad"), new DateTime(2024, 9, 16, 12, 40, 40, 708, DateTimeKind.Utc).AddTicks(5941), null, "Orta", 2m, new Guid("4c75965d-1977-4449-98be-6ce8fdd4e1d9") },
                    { new Guid("5c495d37-3078-45fe-a30f-544ecb3447b2"), new DateTime(2024, 9, 16, 12, 40, 40, 708, DateTimeKind.Utc).AddTicks(5954), null, "Orta", 2m, new Guid("f3bb791e-b1a0-4f0b-96a3-8c9dc70e12d1") },
                    { new Guid("6c012c11-b8f3-47cb-865d-2a3371f2a1f6"), new DateTime(2024, 9, 16, 12, 40, 40, 708, DateTimeKind.Utc).AddTicks(5945), null, "Orta", 2m, new Guid("8fad5075-71f4-48ad-bf0e-3a2de9b975b6") },
                    { new Guid("7d70ac35-97b6-46a8-891a-0cec1d6ba5f7"), new DateTime(2024, 9, 16, 12, 40, 40, 708, DateTimeKind.Utc).AddTicks(5947), null, "Standart", 0m, new Guid("51a4a541-5c99-45d4-9d32-72a6e92442b0") },
                    { new Guid("8d591048-d156-4f8d-a1ae-3afb2ecbb3d5"), new DateTime(2024, 9, 16, 12, 40, 40, 708, DateTimeKind.Utc).AddTicks(5951), null, "Büyük", 4m, new Guid("51a4a541-5c99-45d4-9d32-72a6e92442b0") },
                    { new Guid("9458cfc0-f4b0-4245-bc6a-767b69a2b35e"), new DateTime(2024, 9, 16, 12, 40, 40, 708, DateTimeKind.Utc).AddTicks(5940), null, "Standart", 0m, new Guid("4c75965d-1977-4449-98be-6ce8fdd4e1d9") },
                    { new Guid("a476f05f-46a3-4f4f-bfe9-c887c6ce1c71"), new DateTime(2024, 9, 16, 12, 40, 40, 708, DateTimeKind.Utc).AddTicks(5950), null, "Orta", 2m, new Guid("51a4a541-5c99-45d4-9d32-72a6e92442b0") },
                    { new Guid("aa96cdb2-bacb-4a7e-9d0a-f2261c33492e"), new DateTime(2024, 9, 16, 12, 40, 40, 708, DateTimeKind.Utc).AddTicks(5942), null, "Büyük", 4m, new Guid("4c75965d-1977-4449-98be-6ce8fdd4e1d9") },
                    { new Guid("cd13ad25-73d1-4e5a-be42-357bcfacf42a"), new DateTime(2024, 9, 16, 12, 40, 40, 708, DateTimeKind.Utc).AddTicks(5953), null, "Standart", 0m, new Guid("f3bb791e-b1a0-4f0b-96a3-8c9dc70e12d1") }
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
