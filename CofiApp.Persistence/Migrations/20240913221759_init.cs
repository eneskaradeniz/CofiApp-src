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
                name: "OrderItemOptions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductOptionName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ProductOptionPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItemOptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItemOptions_OrderItems_OrderItemId",
                        column: x => x.OrderItemId,
                        principalTable: "OrderItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "MenuCategories",
                columns: new[] { "Id", "CreatedOnUtc", "ModifiedOnUtc", "Name" },
                values: new object[,]
                {
                    { new Guid("3ced96f4-16b6-42fe-9834-c43c4a9a45f3"), new DateTime(2024, 9, 13, 22, 17, 59, 510, DateTimeKind.Utc).AddTicks(8876), null, "Öne Çıkaranlar" },
                    { new Guid("a6270d62-e249-4950-9149-73a23c6a0794"), new DateTime(2024, 9, 13, 22, 17, 59, 510, DateTimeKind.Utc).AddTicks(8886), null, "Sıcak Kahveler" },
                    { new Guid("bee109d8-b4eb-446f-855b-ea45d2ff279b"), new DateTime(2024, 9, 13, 22, 17, 59, 510, DateTimeKind.Utc).AddTicks(8887), null, "Soğuk Kahveler" }
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
                    { 32, "GetActiveBasketByUser" },
                    { 33, "CreateBasketItem" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CreatedOnUtc", "DeletedOnUtc", "Description", "ModifiedOnUtc", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("39c73dda-4b0a-4bae-b801-2cc36a980a54"), new DateTime(2024, 9, 13, 22, 17, 59, 510, DateTimeKind.Utc).AddTicks(8925), null, null, null, "Ice Latte", 10m },
                    { new Guid("89460128-431a-402a-af93-a99bda22a2b5"), new DateTime(2024, 9, 13, 22, 17, 59, 510, DateTimeKind.Utc).AddTicks(8923), null, null, null, "Mocha", 20m },
                    { new Guid("acb90efa-e40a-4be1-9fac-4f3e597d260f"), new DateTime(2024, 9, 13, 22, 17, 59, 510, DateTimeKind.Utc).AddTicks(8924), null, null, null, "Ice Americano", 10m },
                    { new Guid("ce91c203-e70f-48ad-a42d-44451bc3b1bb"), new DateTime(2024, 9, 13, 22, 17, 59, 510, DateTimeKind.Utc).AddTicks(8921), null, null, null, "Latte", 15m }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedOnUtc", "ModifiedOnUtc", "Name" },
                values: new object[] { new Guid("8b77d3c0-2886-4296-8e22-402d3c5c8e93"), new DateTime(2024, 9, 13, 22, 17, 59, 507, DateTimeKind.Utc).AddTicks(2018), null, "Administrator" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedOnUtc", "DeletedOnUtc", "Email", "EmailConfirmed", "FirstName", "LastName", "ModifiedOnUtc", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed" },
                values: new object[] { new Guid("87c9272e-2046-470d-9ce5-9d32fd9fcc06"), new DateTime(2024, 9, 13, 22, 17, 59, 510, DateTimeKind.Utc).AddTicks(8708), null, "eneskrdnz28@gmail.com", true, "Enes", "Karadeniz", null, "9A5466A4F194CD39813C886BFB59F6AF35FCB22AD76B12A1572C2C02D989B8EB-582BB6949CA31B538FAC47ABE7B804FB", null, false });

            migrationBuilder.InsertData(
                table: "ProductMenuCategories",
                columns: new[] { "MenuCategoryId", "ProductId" },
                values: new object[,]
                {
                    { new Guid("bee109d8-b4eb-446f-855b-ea45d2ff279b"), new Guid("39c73dda-4b0a-4bae-b801-2cc36a980a54") },
                    { new Guid("3ced96f4-16b6-42fe-9834-c43c4a9a45f3"), new Guid("89460128-431a-402a-af93-a99bda22a2b5") },
                    { new Guid("a6270d62-e249-4950-9149-73a23c6a0794"), new Guid("89460128-431a-402a-af93-a99bda22a2b5") },
                    { new Guid("bee109d8-b4eb-446f-855b-ea45d2ff279b"), new Guid("acb90efa-e40a-4be1-9fac-4f3e597d260f") },
                    { new Guid("3ced96f4-16b6-42fe-9834-c43c4a9a45f3"), new Guid("ce91c203-e70f-48ad-a42d-44451bc3b1bb") },
                    { new Guid("a6270d62-e249-4950-9149-73a23c6a0794"), new Guid("ce91c203-e70f-48ad-a42d-44451bc3b1bb") }
                });

            migrationBuilder.InsertData(
                table: "ProductOptionGroups",
                columns: new[] { "Id", "CreatedOnUtc", "IsRequired", "ModifiedOnUtc", "Name", "ProductId" },
                values: new object[,]
                {
                    { new Guid("59f87686-122e-4095-a58a-26caea726ee1"), new DateTime(2024, 9, 13, 22, 17, 59, 510, DateTimeKind.Utc).AddTicks(8953), true, null, "Boy Seçimi", new Guid("89460128-431a-402a-af93-a99bda22a2b5") },
                    { new Guid("6e0b55db-2b02-4500-8ef6-fa6d294e8821"), new DateTime(2024, 9, 13, 22, 17, 59, 510, DateTimeKind.Utc).AddTicks(8952), true, null, "Boy Seçimi", new Guid("ce91c203-e70f-48ad-a42d-44451bc3b1bb") },
                    { new Guid("a811c569-e7a9-43ad-8624-50e8e2b3b4b1"), new DateTime(2024, 9, 13, 22, 17, 59, 510, DateTimeKind.Utc).AddTicks(8958), true, null, "Boy Seçimi", new Guid("acb90efa-e40a-4be1-9fac-4f3e597d260f") },
                    { new Guid("f39d399e-7233-445f-bd87-328acb055387"), new DateTime(2024, 9, 13, 22, 17, 59, 510, DateTimeKind.Utc).AddTicks(8956), true, null, "Boy Seçimi", new Guid("39c73dda-4b0a-4bae-b801-2cc36a980a54") }
                });

            migrationBuilder.InsertData(
                table: "RolePermission",
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[] { 1, new Guid("8b77d3c0-2886-4296-8e22-402d3c5c8e93") });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("8b77d3c0-2886-4296-8e22-402d3c5c8e93"), new Guid("87c9272e-2046-470d-9ce5-9d32fd9fcc06") });

            migrationBuilder.InsertData(
                table: "ProductOptions",
                columns: new[] { "Id", "CreatedOnUtc", "ModifiedOnUtc", "Name", "Price", "ProductOptionGroupId" },
                values: new object[,]
                {
                    { new Guid("15557090-2056-42a1-b02d-0c88dec78ac0"), new DateTime(2024, 9, 13, 22, 17, 59, 510, DateTimeKind.Utc).AddTicks(8990), null, "Orta", 2m, new Guid("6e0b55db-2b02-4500-8ef6-fa6d294e8821") },
                    { new Guid("16c12caa-d4b6-438d-ba31-3bd76780a008"), new DateTime(2024, 9, 13, 22, 17, 59, 510, DateTimeKind.Utc).AddTicks(9001), null, "Standart", 0m, new Guid("a811c569-e7a9-43ad-8624-50e8e2b3b4b1") },
                    { new Guid("22bbeb5b-da8f-4558-9ccb-4ea98ae949d0"), new DateTime(2024, 9, 13, 22, 17, 59, 510, DateTimeKind.Utc).AddTicks(8999), null, "Orta", 2m, new Guid("f39d399e-7233-445f-bd87-328acb055387") },
                    { new Guid("255f050f-b503-44e0-8cd8-6975ff1572f9"), new DateTime(2024, 9, 13, 22, 17, 59, 510, DateTimeKind.Utc).AddTicks(8998), null, "Standart", 0m, new Guid("f39d399e-7233-445f-bd87-328acb055387") },
                    { new Guid("2b1cb234-0c24-4226-af7f-4395ac387775"), new DateTime(2024, 9, 13, 22, 17, 59, 510, DateTimeKind.Utc).AddTicks(8994), null, "Orta", 2m, new Guid("59f87686-122e-4095-a58a-26caea726ee1") },
                    { new Guid("5b89316a-4df4-48c1-8745-a9427ca9af86"), new DateTime(2024, 9, 13, 22, 17, 59, 510, DateTimeKind.Utc).AddTicks(8995), null, "Büyük", 4m, new Guid("59f87686-122e-4095-a58a-26caea726ee1") },
                    { new Guid("61de6614-ba08-466f-9bfe-02f04c94fcf1"), new DateTime(2024, 9, 13, 22, 17, 59, 510, DateTimeKind.Utc).AddTicks(8993), null, "Standart", 0m, new Guid("59f87686-122e-4095-a58a-26caea726ee1") },
                    { new Guid("840709df-e884-4cd6-979e-fff71a8e7180"), new DateTime(2024, 9, 13, 22, 17, 59, 510, DateTimeKind.Utc).AddTicks(8987), null, "Standart", 0m, new Guid("6e0b55db-2b02-4500-8ef6-fa6d294e8821") },
                    { new Guid("933f3898-9b65-43e3-9384-a7ab4218d370"), new DateTime(2024, 9, 13, 22, 17, 59, 510, DateTimeKind.Utc).AddTicks(9003), null, "Büyük", 4m, new Guid("a811c569-e7a9-43ad-8624-50e8e2b3b4b1") },
                    { new Guid("9512b611-fad8-43d1-80b5-12568ed4fe20"), new DateTime(2024, 9, 13, 22, 17, 59, 510, DateTimeKind.Utc).AddTicks(9002), null, "Orta", 2m, new Guid("a811c569-e7a9-43ad-8624-50e8e2b3b4b1") },
                    { new Guid("d0459463-dd7f-4816-8327-c47a08eaf6a7"), new DateTime(2024, 9, 13, 22, 17, 59, 510, DateTimeKind.Utc).AddTicks(9000), null, "Büyük", 4m, new Guid("f39d399e-7233-445f-bd87-328acb055387") },
                    { new Guid("d2fca26d-e4cf-415c-a157-97d2824681b3"), new DateTime(2024, 9, 13, 22, 17, 59, 510, DateTimeKind.Utc).AddTicks(8991), null, "Büyük", 4m, new Guid("6e0b55db-2b02-4500-8ef6-fa6d294e8821") }
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
                name: "IX_OrderItemOptions_OrderItemId",
                table: "OrderItemOptions",
                column: "OrderItemId");

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
                name: "OrderItems");

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
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Baskets");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
