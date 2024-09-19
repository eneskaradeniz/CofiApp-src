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
                name: "Files",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ContainerName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Size = table.Column<long>(type: "bigint", nullable: false),
                    CreatedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.Id);
                });

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
                name: "ProductImageFiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImageFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductImageFiles_Files_Id",
                        column: x => x.Id,
                        principalTable: "Files",
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
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProductImageFileId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_ProductImageFiles_ProductImageFileId",
                        column: x => x.ProductImageFileId,
                        principalTable: "ProductImageFiles",
                        principalColumn: "Id");
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
                    { new Guid("23f2de07-47f2-406b-b4fd-b7b4e9d6e473"), new DateTime(2024, 9, 19, 1, 11, 47, 153, DateTimeKind.Utc).AddTicks(9330), 3, null, "Soğuk Kahveler" },
                    { new Guid("c1fea2aa-e375-42d5-a3b8-248c08b616f5"), new DateTime(2024, 9, 19, 1, 11, 47, 153, DateTimeKind.Utc).AddTicks(9329), 2, null, "Sıcak Kahveler" },
                    { new Guid("f5643a9f-30ef-41b1-8e10-72fcbf867e0b"), new DateTime(2024, 9, 19, 1, 11, 47, 153, DateTimeKind.Utc).AddTicks(9327), 1, null, "Öne Çıkaranlar" }
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
                    { 40, "CancelShopOrder" },
                    { 41, "ProcessShopOrder" },
                    { 42, "CompleteShopOrder" },
                    { 43, "GetCustomerOrders" },
                    { 44, "GetCustomerOrderById" },
                    { 45, "CreateCustomerOrder" },
                    { 46, "CancelCustomerOrder" },
                    { 47, "UploadProductImageFile" },
                    { 48, "RemoveProductImageFile" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CreatedOnUtc", "DeletedOnUtc", "Description", "ModifiedOnUtc", "Name", "Price", "ProductImageFileId" },
                values: new object[,]
                {
                    { new Guid("43d731fc-5a48-4dac-afc7-8b8da9343fd5"), new DateTime(2024, 9, 19, 1, 11, 47, 153, DateTimeKind.Utc).AddTicks(9429), null, null, null, "Latte", 15m, null },
                    { new Guid("a42d9966-8b77-4e2d-9286-025406f0ae74"), new DateTime(2024, 9, 19, 1, 11, 47, 153, DateTimeKind.Utc).AddTicks(9433), null, null, null, "Ice Americano", 10m, null },
                    { new Guid("bd9e7c0b-5e5d-47f7-80cb-5ae28dbb6d2d"), new DateTime(2024, 9, 19, 1, 11, 47, 153, DateTimeKind.Utc).AddTicks(9434), null, null, null, "Ice Latte", 10m, null },
                    { new Guid("f682b687-bcec-41aa-8b16-63e5af1f5fef"), new DateTime(2024, 9, 19, 1, 11, 47, 153, DateTimeKind.Utc).AddTicks(9431), null, null, null, "Mocha", 20m, null }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedOnUtc", "ModifiedOnUtc", "Name" },
                values: new object[] { new Guid("ac78158f-9853-4b13-b6a7-5014fc05ab86"), new DateTime(2024, 9, 19, 1, 11, 47, 150, DateTimeKind.Utc).AddTicks(2361), null, "Administrator" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedOnUtc", "DeletedOnUtc", "Email", "EmailConfirmed", "FirstName", "LastName", "ModifiedOnUtc", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed" },
                values: new object[] { new Guid("1b171ff7-1bd9-4b5e-bf87-23117bb667a5"), new DateTime(2024, 9, 19, 1, 11, 47, 153, DateTimeKind.Utc).AddTicks(9194), null, "eneskrdnz28@gmail.com", true, "Enes", "Karadeniz", null, "5BBD21B47C970917FE1F5E6CF7A86FE69BD63E69CFEB8F73BE788F9C49264C40-310FB5E7423D30B2D14DB1E339C6B309", null, false });

            migrationBuilder.InsertData(
                table: "ProductMenuCategories",
                columns: new[] { "MenuCategoryId", "ProductId" },
                values: new object[,]
                {
                    { new Guid("c1fea2aa-e375-42d5-a3b8-248c08b616f5"), new Guid("43d731fc-5a48-4dac-afc7-8b8da9343fd5") },
                    { new Guid("f5643a9f-30ef-41b1-8e10-72fcbf867e0b"), new Guid("43d731fc-5a48-4dac-afc7-8b8da9343fd5") },
                    { new Guid("23f2de07-47f2-406b-b4fd-b7b4e9d6e473"), new Guid("a42d9966-8b77-4e2d-9286-025406f0ae74") },
                    { new Guid("23f2de07-47f2-406b-b4fd-b7b4e9d6e473"), new Guid("bd9e7c0b-5e5d-47f7-80cb-5ae28dbb6d2d") },
                    { new Guid("c1fea2aa-e375-42d5-a3b8-248c08b616f5"), new Guid("f682b687-bcec-41aa-8b16-63e5af1f5fef") },
                    { new Guid("f5643a9f-30ef-41b1-8e10-72fcbf867e0b"), new Guid("f682b687-bcec-41aa-8b16-63e5af1f5fef") }
                });

            migrationBuilder.InsertData(
                table: "ProductOptionGroups",
                columns: new[] { "Id", "CreatedOnUtc", "IsRequired", "ModifiedOnUtc", "Name", "ProductId" },
                values: new object[,]
                {
                    { new Guid("0e72f2e8-85da-4a9b-8453-066239412fce"), new DateTime(2024, 9, 19, 1, 11, 47, 153, DateTimeKind.Utc).AddTicks(9467), true, null, "Boy Seçimi", new Guid("43d731fc-5a48-4dac-afc7-8b8da9343fd5") },
                    { new Guid("7a992126-0a2a-4cd5-a9fb-acf5c1b1d1bd"), new DateTime(2024, 9, 19, 1, 11, 47, 153, DateTimeKind.Utc).AddTicks(9469), true, null, "Boy Seçimi", new Guid("bd9e7c0b-5e5d-47f7-80cb-5ae28dbb6d2d") },
                    { new Guid("c533fa0e-8810-46ec-aafd-21b9c2a76312"), new DateTime(2024, 9, 19, 1, 11, 47, 153, DateTimeKind.Utc).AddTicks(9468), true, null, "Boy Seçimi", new Guid("f682b687-bcec-41aa-8b16-63e5af1f5fef") },
                    { new Guid("dbee91a8-882b-447a-9552-0d677503018c"), new DateTime(2024, 9, 19, 1, 11, 47, 153, DateTimeKind.Utc).AddTicks(9471), true, null, "Boy Seçimi", new Guid("a42d9966-8b77-4e2d-9286-025406f0ae74") }
                });

            migrationBuilder.InsertData(
                table: "RolePermission",
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[] { 1, new Guid("ac78158f-9853-4b13-b6a7-5014fc05ab86") });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("ac78158f-9853-4b13-b6a7-5014fc05ab86"), new Guid("1b171ff7-1bd9-4b5e-bf87-23117bb667a5") });

            migrationBuilder.InsertData(
                table: "ProductOptions",
                columns: new[] { "Id", "CreatedOnUtc", "ModifiedOnUtc", "Name", "Price", "ProductOptionGroupId" },
                values: new object[,]
                {
                    { new Guid("262d1ce8-033c-44ca-80f5-72247290a9f9"), new DateTime(2024, 9, 19, 1, 11, 47, 153, DateTimeKind.Utc).AddTicks(9569), null, "Orta", 2m, new Guid("7a992126-0a2a-4cd5-a9fb-acf5c1b1d1bd") },
                    { new Guid("32902b00-d2ab-42aa-af6d-5e29642b9b40"), new DateTime(2024, 9, 19, 1, 11, 47, 153, DateTimeKind.Utc).AddTicks(9559), null, "Standart", 0m, new Guid("0e72f2e8-85da-4a9b-8453-066239412fce") },
                    { new Guid("34f951d6-b0d3-453c-8e8e-bf953f5111f4"), new DateTime(2024, 9, 19, 1, 11, 47, 153, DateTimeKind.Utc).AddTicks(9568), null, "Standart", 0m, new Guid("7a992126-0a2a-4cd5-a9fb-acf5c1b1d1bd") },
                    { new Guid("49e6579e-3a01-4a56-883b-e743a4b9b613"), new DateTime(2024, 9, 19, 1, 11, 47, 153, DateTimeKind.Utc).AddTicks(9572), null, "Büyük", 4m, new Guid("7a992126-0a2a-4cd5-a9fb-acf5c1b1d1bd") },
                    { new Guid("7c8b384b-e2f7-41ae-a57b-c118bc560d53"), new DateTime(2024, 9, 19, 1, 11, 47, 153, DateTimeKind.Utc).AddTicks(9564), null, "Standart", 0m, new Guid("c533fa0e-8810-46ec-aafd-21b9c2a76312") },
                    { new Guid("89ac0a76-75eb-48cb-8f5f-3a86f66a6955"), new DateTime(2024, 9, 19, 1, 11, 47, 153, DateTimeKind.Utc).AddTicks(9567), null, "Büyük", 4m, new Guid("c533fa0e-8810-46ec-aafd-21b9c2a76312") },
                    { new Guid("aceec0ee-3c9e-410d-8f92-f99bde4a9aca"), new DateTime(2024, 9, 19, 1, 11, 47, 153, DateTimeKind.Utc).AddTicks(9574), null, "Standart", 0m, new Guid("dbee91a8-882b-447a-9552-0d677503018c") },
                    { new Guid("d07e026f-62d0-4ca7-a291-88111dde3fd5"), new DateTime(2024, 9, 19, 1, 11, 47, 153, DateTimeKind.Utc).AddTicks(9575), null, "Orta", 2m, new Guid("dbee91a8-882b-447a-9552-0d677503018c") },
                    { new Guid("d220a72d-159a-4d67-8641-de417f0fa3f2"), new DateTime(2024, 9, 19, 1, 11, 47, 153, DateTimeKind.Utc).AddTicks(9577), null, "Büyük", 4m, new Guid("dbee91a8-882b-447a-9552-0d677503018c") },
                    { new Guid("d4dc1bc0-665a-4900-bc4d-41c298b5e940"), new DateTime(2024, 9, 19, 1, 11, 47, 153, DateTimeKind.Utc).AddTicks(9566), null, "Orta", 2m, new Guid("c533fa0e-8810-46ec-aafd-21b9c2a76312") },
                    { new Guid("e43f45f1-5986-4b9a-b142-ca712480b322"), new DateTime(2024, 9, 19, 1, 11, 47, 153, DateTimeKind.Utc).AddTicks(9563), null, "Büyük", 4m, new Guid("0e72f2e8-85da-4a9b-8453-066239412fce") },
                    { new Guid("fa761f9e-91d9-4dec-96aa-9e2756c8033c"), new DateTime(2024, 9, 19, 1, 11, 47, 153, DateTimeKind.Utc).AddTicks(9562), null, "Orta", 2m, new Guid("0e72f2e8-85da-4a9b-8453-066239412fce") }
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
                name: "IX_Products_ProductImageFileId",
                table: "Products",
                column: "ProductImageFileId",
                unique: true,
                filter: "[ProductImageFileId] IS NOT NULL");

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
                name: "ProductImageFiles");

            migrationBuilder.DropTable(
                name: "Baskets");

            migrationBuilder.DropTable(
                name: "Files");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
