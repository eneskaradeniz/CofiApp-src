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
                    Path = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Size = table.Column<long>(type: "bigint", nullable: false),
                    StorageType = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    { new Guid("9035a449-e905-4e67-88cc-244521986ee1"), new DateTime(2024, 9, 19, 14, 4, 56, 765, DateTimeKind.Utc).AddTicks(3838), 2, null, "Sıcak Kahveler" },
                    { new Guid("930afb5b-cb65-4ad7-b54a-4e94900bf3df"), new DateTime(2024, 9, 19, 14, 4, 56, 765, DateTimeKind.Utc).AddTicks(3846), 3, null, "Soğuk Kahveler" },
                    { new Guid("b878e4c2-87e0-4108-b550-5185359428b3"), new DateTime(2024, 9, 19, 14, 4, 56, 765, DateTimeKind.Utc).AddTicks(3836), 1, null, "Öne Çıkaranlar" }
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
                    { new Guid("77c54e6c-509d-45be-9c68-a538159bd417"), new DateTime(2024, 9, 19, 14, 4, 56, 765, DateTimeKind.Utc).AddTicks(3879), null, null, null, "Mocha", 20m, null },
                    { new Guid("98822077-d47d-49aa-bd83-a16d87a29437"), new DateTime(2024, 9, 19, 14, 4, 56, 765, DateTimeKind.Utc).AddTicks(3898), null, null, null, "Ice Latte", 10m, null },
                    { new Guid("c1b1caa3-1153-414e-83a2-ccec45491202"), new DateTime(2024, 9, 19, 14, 4, 56, 765, DateTimeKind.Utc).AddTicks(3897), null, null, null, "Ice Americano", 10m, null },
                    { new Guid("f1545f8d-739c-4902-848b-efff5fc01cc2"), new DateTime(2024, 9, 19, 14, 4, 56, 765, DateTimeKind.Utc).AddTicks(3877), null, null, null, "Latte", 15m, null }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedOnUtc", "ModifiedOnUtc", "Name" },
                values: new object[] { new Guid("38d625fb-6965-41da-a302-2a595829231c"), new DateTime(2024, 9, 19, 14, 4, 56, 761, DateTimeKind.Utc).AddTicks(6894), null, "Administrator" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedOnUtc", "DeletedOnUtc", "Email", "EmailConfirmed", "FirstName", "LastName", "ModifiedOnUtc", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed" },
                values: new object[] { new Guid("f8c0b3e4-febe-450b-baff-bea13fcdd59c"), new DateTime(2024, 9, 19, 14, 4, 56, 765, DateTimeKind.Utc).AddTicks(3699), null, "eneskrdnz28@gmail.com", true, "Enes", "Karadeniz", null, "5CC26F643A36666DA687F8C28BAEBE9FEBAD3A37353B99500CE00B798D51D639-5E77AD3E3CBA5BDAFC6B592CE6A8D871", null, false });

            migrationBuilder.InsertData(
                table: "ProductMenuCategories",
                columns: new[] { "MenuCategoryId", "ProductId" },
                values: new object[,]
                {
                    { new Guid("9035a449-e905-4e67-88cc-244521986ee1"), new Guid("77c54e6c-509d-45be-9c68-a538159bd417") },
                    { new Guid("b878e4c2-87e0-4108-b550-5185359428b3"), new Guid("77c54e6c-509d-45be-9c68-a538159bd417") },
                    { new Guid("930afb5b-cb65-4ad7-b54a-4e94900bf3df"), new Guid("98822077-d47d-49aa-bd83-a16d87a29437") },
                    { new Guid("930afb5b-cb65-4ad7-b54a-4e94900bf3df"), new Guid("c1b1caa3-1153-414e-83a2-ccec45491202") },
                    { new Guid("9035a449-e905-4e67-88cc-244521986ee1"), new Guid("f1545f8d-739c-4902-848b-efff5fc01cc2") },
                    { new Guid("b878e4c2-87e0-4108-b550-5185359428b3"), new Guid("f1545f8d-739c-4902-848b-efff5fc01cc2") }
                });

            migrationBuilder.InsertData(
                table: "ProductOptionGroups",
                columns: new[] { "Id", "CreatedOnUtc", "IsRequired", "ModifiedOnUtc", "Name", "ProductId" },
                values: new object[,]
                {
                    { new Guid("32b68ebe-5c25-43d2-99a5-5b5109497a78"), new DateTime(2024, 9, 19, 14, 4, 56, 765, DateTimeKind.Utc).AddTicks(3925), true, null, "Boy Seçimi", new Guid("98822077-d47d-49aa-bd83-a16d87a29437") },
                    { new Guid("5131273b-981f-4c7c-9b12-ad4f06c15a20"), new DateTime(2024, 9, 19, 14, 4, 56, 765, DateTimeKind.Utc).AddTicks(3924), true, null, "Boy Seçimi", new Guid("77c54e6c-509d-45be-9c68-a538159bd417") },
                    { new Guid("77a83bdf-2773-4af3-aa6b-1700b3f187fb"), new DateTime(2024, 9, 19, 14, 4, 56, 765, DateTimeKind.Utc).AddTicks(3923), true, null, "Boy Seçimi", new Guid("f1545f8d-739c-4902-848b-efff5fc01cc2") },
                    { new Guid("9ab6d76e-6218-42ff-97d7-eac060bc052a"), new DateTime(2024, 9, 19, 14, 4, 56, 765, DateTimeKind.Utc).AddTicks(3931), true, null, "Boy Seçimi", new Guid("c1b1caa3-1153-414e-83a2-ccec45491202") }
                });

            migrationBuilder.InsertData(
                table: "RolePermission",
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[] { 1, new Guid("38d625fb-6965-41da-a302-2a595829231c") });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("38d625fb-6965-41da-a302-2a595829231c"), new Guid("f8c0b3e4-febe-450b-baff-bea13fcdd59c") });

            migrationBuilder.InsertData(
                table: "ProductOptions",
                columns: new[] { "Id", "CreatedOnUtc", "ModifiedOnUtc", "Name", "Price", "ProductOptionGroupId" },
                values: new object[,]
                {
                    { new Guid("1d010e38-039d-4b87-b184-2bf2b4782e4d"), new DateTime(2024, 9, 19, 14, 4, 56, 765, DateTimeKind.Utc).AddTicks(3960), null, "Büyük", 4m, new Guid("77a83bdf-2773-4af3-aa6b-1700b3f187fb") },
                    { new Guid("288c8d06-c729-47a5-b9ab-48a3cfd1548c"), new DateTime(2024, 9, 19, 14, 4, 56, 765, DateTimeKind.Utc).AddTicks(3970), null, "Standart", 0m, new Guid("9ab6d76e-6218-42ff-97d7-eac060bc052a") },
                    { new Guid("3024dbd8-873d-485c-ae41-66ef74eacbde"), new DateTime(2024, 9, 19, 14, 4, 56, 765, DateTimeKind.Utc).AddTicks(3957), null, "Standart", 0m, new Guid("77a83bdf-2773-4af3-aa6b-1700b3f187fb") },
                    { new Guid("4acd8f6f-ed49-40f7-bb99-61da60218586"), new DateTime(2024, 9, 19, 14, 4, 56, 765, DateTimeKind.Utc).AddTicks(3967), null, "Orta", 2m, new Guid("32b68ebe-5c25-43d2-99a5-5b5109497a78") },
                    { new Guid("660481c8-4895-49fb-a450-a1d38b22cbb5"), new DateTime(2024, 9, 19, 14, 4, 56, 765, DateTimeKind.Utc).AddTicks(3961), null, "Standart", 0m, new Guid("5131273b-981f-4c7c-9b12-ad4f06c15a20") },
                    { new Guid("7a42c8ec-cf79-426c-987b-ad4e7abbccb3"), new DateTime(2024, 9, 19, 14, 4, 56, 765, DateTimeKind.Utc).AddTicks(3959), null, "Orta", 2m, new Guid("77a83bdf-2773-4af3-aa6b-1700b3f187fb") },
                    { new Guid("9584d23b-9f0b-4572-8bff-726bfa61bb2a"), new DateTime(2024, 9, 19, 14, 4, 56, 765, DateTimeKind.Utc).AddTicks(3972), null, "Büyük", 4m, new Guid("9ab6d76e-6218-42ff-97d7-eac060bc052a") },
                    { new Guid("a156020b-320e-4571-8d23-5ea284ed8bcf"), new DateTime(2024, 9, 19, 14, 4, 56, 765, DateTimeKind.Utc).AddTicks(3962), null, "Orta", 2m, new Guid("5131273b-981f-4c7c-9b12-ad4f06c15a20") },
                    { new Guid("a63c9a21-0787-4a62-9820-99e39c0802e7"), new DateTime(2024, 9, 19, 14, 4, 56, 765, DateTimeKind.Utc).AddTicks(3968), null, "Büyük", 4m, new Guid("32b68ebe-5c25-43d2-99a5-5b5109497a78") },
                    { new Guid("b63a2a09-745b-499d-946b-2a0f824d74d9"), new DateTime(2024, 9, 19, 14, 4, 56, 765, DateTimeKind.Utc).AddTicks(3971), null, "Orta", 2m, new Guid("9ab6d76e-6218-42ff-97d7-eac060bc052a") },
                    { new Guid("cb3a83a7-b5c3-4957-b1c1-1fd87a389768"), new DateTime(2024, 9, 19, 14, 4, 56, 765, DateTimeKind.Utc).AddTicks(3964), null, "Standart", 0m, new Guid("32b68ebe-5c25-43d2-99a5-5b5109497a78") },
                    { new Guid("df35ba80-01f6-4476-8f60-ed2dc2bd913a"), new DateTime(2024, 9, 19, 14, 4, 56, 765, DateTimeKind.Utc).AddTicks(3963), null, "Büyük", 4m, new Guid("5131273b-981f-4c7c-9b12-ad4f06c15a20") }
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
