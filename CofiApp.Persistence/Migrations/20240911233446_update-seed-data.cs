using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CofiApp.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class updateseeddata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProductMenuCategories",
                keyColumns: new[] { "MenuCategoryId", "ProductId" },
                keyValues: new object[] { new Guid("7e96c6ac-2c98-4a46-a2d8-707f42771fb2"), new Guid("173b8030-db4d-44a2-bb80-629842a4d7a2") });

            migrationBuilder.DeleteData(
                table: "ProductMenuCategories",
                keyColumns: new[] { "MenuCategoryId", "ProductId" },
                keyValues: new object[] { new Guid("22326620-890e-4528-b363-025eabc5779b"), new Guid("3e4fa8f5-6b33-4503-9034-73e565e04619") });

            migrationBuilder.DeleteData(
                table: "ProductMenuCategories",
                keyColumns: new[] { "MenuCategoryId", "ProductId" },
                keyValues: new object[] { new Guid("39eeace4-99f7-494f-88d3-c6c84f2e50b7"), new Guid("3e4fa8f5-6b33-4503-9034-73e565e04619") });

            migrationBuilder.DeleteData(
                table: "ProductMenuCategories",
                keyColumns: new[] { "MenuCategoryId", "ProductId" },
                keyValues: new object[] { new Guid("7e96c6ac-2c98-4a46-a2d8-707f42771fb2"), new Guid("8357b246-fc5d-4092-882e-9564fad8c46d") });

            migrationBuilder.DeleteData(
                table: "ProductMenuCategories",
                keyColumns: new[] { "MenuCategoryId", "ProductId" },
                keyValues: new object[] { new Guid("22326620-890e-4528-b363-025eabc5779b"), new Guid("f395abdb-b9d9-4fae-a691-e083809a6172") });

            migrationBuilder.DeleteData(
                table: "ProductMenuCategories",
                keyColumns: new[] { "MenuCategoryId", "ProductId" },
                keyValues: new object[] { new Guid("39eeace4-99f7-494f-88d3-c6c84f2e50b7"), new Guid("f395abdb-b9d9-4fae-a691-e083809a6172") });

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 1, new Guid("6dd96f70-f6de-4434-97f2-cf611d3cb8a0") });

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("6dd96f70-f6de-4434-97f2-cf611d3cb8a0"), new Guid("9e935ced-b2c7-4f71-8e44-393e5736f9f0") });

            migrationBuilder.DeleteData(
                table: "MenuCategories",
                keyColumn: "Id",
                keyValue: new Guid("22326620-890e-4528-b363-025eabc5779b"));

            migrationBuilder.DeleteData(
                table: "MenuCategories",
                keyColumn: "Id",
                keyValue: new Guid("39eeace4-99f7-494f-88d3-c6c84f2e50b7"));

            migrationBuilder.DeleteData(
                table: "MenuCategories",
                keyColumn: "Id",
                keyValue: new Guid("7e96c6ac-2c98-4a46-a2d8-707f42771fb2"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("173b8030-db4d-44a2-bb80-629842a4d7a2"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("3e4fa8f5-6b33-4503-9034-73e565e04619"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("8357b246-fc5d-4092-882e-9564fad8c46d"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("f395abdb-b9d9-4fae-a691-e083809a6172"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("6dd96f70-f6de-4434-97f2-cf611d3cb8a0"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("9e935ced-b2c7-4f71-8e44-393e5736f9f0"));

            migrationBuilder.InsertData(
                table: "MenuCategories",
                columns: new[] { "Id", "CreatedOnUtc", "ModifiedOnUtc", "Name" },
                values: new object[,]
                {
                    { new Guid("42a04f74-89a4-4bd0-84d8-58fd46b33174"), new DateTime(2024, 9, 11, 23, 34, 45, 718, DateTimeKind.Utc).AddTicks(6371), null, "Sıcak Kahveler" },
                    { new Guid("499e3a63-c35b-4f75-a54e-033ea920d720"), new DateTime(2024, 9, 11, 23, 34, 45, 718, DateTimeKind.Utc).AddTicks(6372), null, "Soğuk Kahveler" },
                    { new Guid("8d98413c-318a-4090-a4bd-d5e9b8d121f9"), new DateTime(2024, 9, 11, 23, 34, 45, 718, DateTimeKind.Utc).AddTicks(6370), null, "Öne Çıkaranlar" }
                });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 25, "GetProductOptionGroupsWithOptions" },
                    { 26, "CreateProductOptionGroup" },
                    { 27, "UpdateProductOptionGroup" },
                    { 28, "RemoveProductOptionGroup" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CreatedOnUtc", "DeletedOnUtc", "Description", "ModifiedOnUtc", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("04f7281b-7032-4f16-b96a-336462a95fcc"), new DateTime(2024, 9, 11, 23, 34, 45, 718, DateTimeKind.Utc).AddTicks(6425), null, null, null, "Latte", 15m },
                    { new Guid("67cdabd3-e8af-4d4e-82ad-28f6c1f8ed64"), new DateTime(2024, 9, 11, 23, 34, 45, 718, DateTimeKind.Utc).AddTicks(6428), null, null, null, "Ice Americano", 10m },
                    { new Guid("8ba3cc24-0ba2-49aa-b27b-4b36d3b0b830"), new DateTime(2024, 9, 11, 23, 34, 45, 718, DateTimeKind.Utc).AddTicks(6427), null, null, null, "Mocha", 20m },
                    { new Guid("a4726fae-db8f-4640-8e9e-b92613ce9313"), new DateTime(2024, 9, 11, 23, 34, 45, 718, DateTimeKind.Utc).AddTicks(6429), null, null, null, "Ice Latte", 10m }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedOnUtc", "ModifiedOnUtc", "Name" },
                values: new object[] { new Guid("c617d943-3587-4dbc-a808-0dd40649cbda"), new DateTime(2024, 9, 11, 23, 34, 45, 714, DateTimeKind.Utc).AddTicks(7422), null, "Administrator" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedOnUtc", "DeletedOnUtc", "Email", "EmailConfirmed", "FirstName", "LastName", "ModifiedOnUtc", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed" },
                values: new object[] { new Guid("4e1fe578-d637-48e3-85b9-aed06dceb7d3"), new DateTime(2024, 9, 11, 23, 34, 45, 718, DateTimeKind.Utc).AddTicks(5948), null, "eneskrdnz28@gmail.com", true, "Enes", "Karadeniz", null, "84EC87F0660FE72D18ABDFDDECE3EA6F0947E17CC31D9FD5118CBD42DF22086F-5BD03E4883FA460E2569473FAC6B5456", null, false });

            migrationBuilder.InsertData(
                table: "ProductMenuCategories",
                columns: new[] { "MenuCategoryId", "ProductId" },
                values: new object[,]
                {
                    { new Guid("42a04f74-89a4-4bd0-84d8-58fd46b33174"), new Guid("04f7281b-7032-4f16-b96a-336462a95fcc") },
                    { new Guid("8d98413c-318a-4090-a4bd-d5e9b8d121f9"), new Guid("04f7281b-7032-4f16-b96a-336462a95fcc") },
                    { new Guid("499e3a63-c35b-4f75-a54e-033ea920d720"), new Guid("67cdabd3-e8af-4d4e-82ad-28f6c1f8ed64") },
                    { new Guid("42a04f74-89a4-4bd0-84d8-58fd46b33174"), new Guid("8ba3cc24-0ba2-49aa-b27b-4b36d3b0b830") },
                    { new Guid("8d98413c-318a-4090-a4bd-d5e9b8d121f9"), new Guid("8ba3cc24-0ba2-49aa-b27b-4b36d3b0b830") },
                    { new Guid("499e3a63-c35b-4f75-a54e-033ea920d720"), new Guid("a4726fae-db8f-4640-8e9e-b92613ce9313") }
                });

            migrationBuilder.InsertData(
                table: "ProductOptionGroups",
                columns: new[] { "Id", "CreatedOnUtc", "IsRequired", "ModifiedOnUtc", "Name", "ProductId" },
                values: new object[,]
                {
                    { new Guid("91d78c92-d27d-495e-95a2-4d0ab1ed8e83"), new DateTime(2024, 9, 11, 23, 34, 45, 718, DateTimeKind.Utc).AddTicks(6471), true, null, "Boy Seçimi", new Guid("a4726fae-db8f-4640-8e9e-b92613ce9313") },
                    { new Guid("ae7f6c29-0733-49a9-9afa-5b383fd86c46"), new DateTime(2024, 9, 11, 23, 34, 45, 718, DateTimeKind.Utc).AddTicks(6466), true, null, "Boy Seçimi", new Guid("04f7281b-7032-4f16-b96a-336462a95fcc") },
                    { new Guid("b81d4d80-14ad-4665-b5bd-526f5a30e0fd"), new DateTime(2024, 9, 11, 23, 34, 45, 718, DateTimeKind.Utc).AddTicks(6541), true, null, "Boy Seçimi", new Guid("67cdabd3-e8af-4d4e-82ad-28f6c1f8ed64") },
                    { new Guid("d3d1ab3c-8f74-4693-83b0-b1fe4847d27f"), new DateTime(2024, 9, 11, 23, 34, 45, 718, DateTimeKind.Utc).AddTicks(6470), true, null, "Boy Seçimi", new Guid("8ba3cc24-0ba2-49aa-b27b-4b36d3b0b830") }
                });

            migrationBuilder.InsertData(
                table: "RolePermission",
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[] { 1, new Guid("c617d943-3587-4dbc-a808-0dd40649cbda") });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("c617d943-3587-4dbc-a808-0dd40649cbda"), new Guid("4e1fe578-d637-48e3-85b9-aed06dceb7d3") });

            migrationBuilder.InsertData(
                table: "ProductOptions",
                columns: new[] { "Id", "CreatedOnUtc", "ModifiedOnUtc", "Name", "Price", "ProductOptionGroupId" },
                values: new object[,]
                {
                    { new Guid("1a0553df-1d62-4ae0-bee1-bb35687b8a26"), new DateTime(2024, 9, 11, 23, 34, 45, 718, DateTimeKind.Utc).AddTicks(6578), null, "Standart", 0m, new Guid("ae7f6c29-0733-49a9-9afa-5b383fd86c46") },
                    { new Guid("9864ef79-d827-43f6-98fa-ad4fc29a20f0"), new DateTime(2024, 9, 11, 23, 34, 45, 718, DateTimeKind.Utc).AddTicks(6582), null, "Büyük", 4m, new Guid("ae7f6c29-0733-49a9-9afa-5b383fd86c46") },
                    { new Guid("a06ce83a-d94f-4837-9c96-7e8a98888324"), new DateTime(2024, 9, 11, 23, 34, 45, 718, DateTimeKind.Utc).AddTicks(6589), null, "Orta", 2m, new Guid("91d78c92-d27d-495e-95a2-4d0ab1ed8e83") },
                    { new Guid("bfcac1ad-0a14-4242-a92d-7b93b9804f6a"), new DateTime(2024, 9, 11, 23, 34, 45, 718, DateTimeKind.Utc).AddTicks(6590), null, "Büyük", 4m, new Guid("91d78c92-d27d-495e-95a2-4d0ab1ed8e83") },
                    { new Guid("cba85f86-84c2-4ac1-bf83-3b9098c82ba2"), new DateTime(2024, 9, 11, 23, 34, 45, 718, DateTimeKind.Utc).AddTicks(6593), null, "Büyük", 4m, new Guid("b81d4d80-14ad-4665-b5bd-526f5a30e0fd") },
                    { new Guid("ccf6b094-4adf-4c84-83c7-7f86811f0227"), new DateTime(2024, 9, 11, 23, 34, 45, 718, DateTimeKind.Utc).AddTicks(6584), null, "Orta", 2m, new Guid("d3d1ab3c-8f74-4693-83b0-b1fe4847d27f") },
                    { new Guid("d51291ed-9af7-4f8c-9a93-c7c0f285a84a"), new DateTime(2024, 9, 11, 23, 34, 45, 718, DateTimeKind.Utc).AddTicks(6586), null, "Büyük", 4m, new Guid("d3d1ab3c-8f74-4693-83b0-b1fe4847d27f") },
                    { new Guid("ed0297a8-bee7-4f5b-a8ed-338a7362d267"), new DateTime(2024, 9, 11, 23, 34, 45, 718, DateTimeKind.Utc).AddTicks(6588), null, "Standart", 0m, new Guid("91d78c92-d27d-495e-95a2-4d0ab1ed8e83") },
                    { new Guid("efd2c12a-b069-4bef-a389-64c7ea00221e"), new DateTime(2024, 9, 11, 23, 34, 45, 718, DateTimeKind.Utc).AddTicks(6592), null, "Orta", 2m, new Guid("b81d4d80-14ad-4665-b5bd-526f5a30e0fd") },
                    { new Guid("f4f48afc-70c2-4eff-a990-42c9f06e0e02"), new DateTime(2024, 9, 11, 23, 34, 45, 718, DateTimeKind.Utc).AddTicks(6580), null, "Orta", 2m, new Guid("ae7f6c29-0733-49a9-9afa-5b383fd86c46") },
                    { new Guid("f53b6d69-12e3-4c4e-97d3-7f4cbc7b5fa7"), new DateTime(2024, 9, 11, 23, 34, 45, 718, DateTimeKind.Utc).AddTicks(6583), null, "Standart", 0m, new Guid("d3d1ab3c-8f74-4693-83b0-b1fe4847d27f") },
                    { new Guid("fe7da4e7-8c4d-4e81-b3b3-67096ba3ca3c"), new DateTime(2024, 9, 11, 23, 34, 45, 718, DateTimeKind.Utc).AddTicks(6591), null, "Standart", 0m, new Guid("b81d4d80-14ad-4665-b5bd-526f5a30e0fd") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "ProductMenuCategories",
                keyColumns: new[] { "MenuCategoryId", "ProductId" },
                keyValues: new object[] { new Guid("42a04f74-89a4-4bd0-84d8-58fd46b33174"), new Guid("04f7281b-7032-4f16-b96a-336462a95fcc") });

            migrationBuilder.DeleteData(
                table: "ProductMenuCategories",
                keyColumns: new[] { "MenuCategoryId", "ProductId" },
                keyValues: new object[] { new Guid("8d98413c-318a-4090-a4bd-d5e9b8d121f9"), new Guid("04f7281b-7032-4f16-b96a-336462a95fcc") });

            migrationBuilder.DeleteData(
                table: "ProductMenuCategories",
                keyColumns: new[] { "MenuCategoryId", "ProductId" },
                keyValues: new object[] { new Guid("499e3a63-c35b-4f75-a54e-033ea920d720"), new Guid("67cdabd3-e8af-4d4e-82ad-28f6c1f8ed64") });

            migrationBuilder.DeleteData(
                table: "ProductMenuCategories",
                keyColumns: new[] { "MenuCategoryId", "ProductId" },
                keyValues: new object[] { new Guid("42a04f74-89a4-4bd0-84d8-58fd46b33174"), new Guid("8ba3cc24-0ba2-49aa-b27b-4b36d3b0b830") });

            migrationBuilder.DeleteData(
                table: "ProductMenuCategories",
                keyColumns: new[] { "MenuCategoryId", "ProductId" },
                keyValues: new object[] { new Guid("8d98413c-318a-4090-a4bd-d5e9b8d121f9"), new Guid("8ba3cc24-0ba2-49aa-b27b-4b36d3b0b830") });

            migrationBuilder.DeleteData(
                table: "ProductMenuCategories",
                keyColumns: new[] { "MenuCategoryId", "ProductId" },
                keyValues: new object[] { new Guid("499e3a63-c35b-4f75-a54e-033ea920d720"), new Guid("a4726fae-db8f-4640-8e9e-b92613ce9313") });

            migrationBuilder.DeleteData(
                table: "ProductOptions",
                keyColumn: "Id",
                keyValue: new Guid("1a0553df-1d62-4ae0-bee1-bb35687b8a26"));

            migrationBuilder.DeleteData(
                table: "ProductOptions",
                keyColumn: "Id",
                keyValue: new Guid("9864ef79-d827-43f6-98fa-ad4fc29a20f0"));

            migrationBuilder.DeleteData(
                table: "ProductOptions",
                keyColumn: "Id",
                keyValue: new Guid("a06ce83a-d94f-4837-9c96-7e8a98888324"));

            migrationBuilder.DeleteData(
                table: "ProductOptions",
                keyColumn: "Id",
                keyValue: new Guid("bfcac1ad-0a14-4242-a92d-7b93b9804f6a"));

            migrationBuilder.DeleteData(
                table: "ProductOptions",
                keyColumn: "Id",
                keyValue: new Guid("cba85f86-84c2-4ac1-bf83-3b9098c82ba2"));

            migrationBuilder.DeleteData(
                table: "ProductOptions",
                keyColumn: "Id",
                keyValue: new Guid("ccf6b094-4adf-4c84-83c7-7f86811f0227"));

            migrationBuilder.DeleteData(
                table: "ProductOptions",
                keyColumn: "Id",
                keyValue: new Guid("d51291ed-9af7-4f8c-9a93-c7c0f285a84a"));

            migrationBuilder.DeleteData(
                table: "ProductOptions",
                keyColumn: "Id",
                keyValue: new Guid("ed0297a8-bee7-4f5b-a8ed-338a7362d267"));

            migrationBuilder.DeleteData(
                table: "ProductOptions",
                keyColumn: "Id",
                keyValue: new Guid("efd2c12a-b069-4bef-a389-64c7ea00221e"));

            migrationBuilder.DeleteData(
                table: "ProductOptions",
                keyColumn: "Id",
                keyValue: new Guid("f4f48afc-70c2-4eff-a990-42c9f06e0e02"));

            migrationBuilder.DeleteData(
                table: "ProductOptions",
                keyColumn: "Id",
                keyValue: new Guid("f53b6d69-12e3-4c4e-97d3-7f4cbc7b5fa7"));

            migrationBuilder.DeleteData(
                table: "ProductOptions",
                keyColumn: "Id",
                keyValue: new Guid("fe7da4e7-8c4d-4e81-b3b3-67096ba3ca3c"));

            migrationBuilder.DeleteData(
                table: "RolePermission",
                keyColumns: new[] { "PermissionId", "RoleId" },
                keyValues: new object[] { 1, new Guid("c617d943-3587-4dbc-a808-0dd40649cbda") });

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("c617d943-3587-4dbc-a808-0dd40649cbda"), new Guid("4e1fe578-d637-48e3-85b9-aed06dceb7d3") });

            migrationBuilder.DeleteData(
                table: "MenuCategories",
                keyColumn: "Id",
                keyValue: new Guid("42a04f74-89a4-4bd0-84d8-58fd46b33174"));

            migrationBuilder.DeleteData(
                table: "MenuCategories",
                keyColumn: "Id",
                keyValue: new Guid("499e3a63-c35b-4f75-a54e-033ea920d720"));

            migrationBuilder.DeleteData(
                table: "MenuCategories",
                keyColumn: "Id",
                keyValue: new Guid("8d98413c-318a-4090-a4bd-d5e9b8d121f9"));

            migrationBuilder.DeleteData(
                table: "ProductOptionGroups",
                keyColumn: "Id",
                keyValue: new Guid("91d78c92-d27d-495e-95a2-4d0ab1ed8e83"));

            migrationBuilder.DeleteData(
                table: "ProductOptionGroups",
                keyColumn: "Id",
                keyValue: new Guid("ae7f6c29-0733-49a9-9afa-5b383fd86c46"));

            migrationBuilder.DeleteData(
                table: "ProductOptionGroups",
                keyColumn: "Id",
                keyValue: new Guid("b81d4d80-14ad-4665-b5bd-526f5a30e0fd"));

            migrationBuilder.DeleteData(
                table: "ProductOptionGroups",
                keyColumn: "Id",
                keyValue: new Guid("d3d1ab3c-8f74-4693-83b0-b1fe4847d27f"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("c617d943-3587-4dbc-a808-0dd40649cbda"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("4e1fe578-d637-48e3-85b9-aed06dceb7d3"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("04f7281b-7032-4f16-b96a-336462a95fcc"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("67cdabd3-e8af-4d4e-82ad-28f6c1f8ed64"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("8ba3cc24-0ba2-49aa-b27b-4b36d3b0b830"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("a4726fae-db8f-4640-8e9e-b92613ce9313"));

            migrationBuilder.InsertData(
                table: "MenuCategories",
                columns: new[] { "Id", "CreatedOnUtc", "ModifiedOnUtc", "Name" },
                values: new object[,]
                {
                    { new Guid("22326620-890e-4528-b363-025eabc5779b"), new DateTime(2024, 9, 11, 22, 6, 29, 485, DateTimeKind.Utc).AddTicks(3823), null, "Öne Çıkaranlar" },
                    { new Guid("39eeace4-99f7-494f-88d3-c6c84f2e50b7"), new DateTime(2024, 9, 11, 22, 6, 29, 485, DateTimeKind.Utc).AddTicks(3825), null, "Sıcak Kahveler" },
                    { new Guid("7e96c6ac-2c98-4a46-a2d8-707f42771fb2"), new DateTime(2024, 9, 11, 22, 6, 29, 485, DateTimeKind.Utc).AddTicks(3827), null, "Soğuk Kahveler" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CreatedOnUtc", "DeletedOnUtc", "Description", "ModifiedOnUtc", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("173b8030-db4d-44a2-bb80-629842a4d7a2"), new DateTime(2024, 9, 11, 22, 6, 29, 485, DateTimeKind.Utc).AddTicks(3874), null, null, null, "Ice Americano", 10m },
                    { new Guid("3e4fa8f5-6b33-4503-9034-73e565e04619"), new DateTime(2024, 9, 11, 22, 6, 29, 485, DateTimeKind.Utc).AddTicks(3872), null, null, null, "Mocha", 20m },
                    { new Guid("8357b246-fc5d-4092-882e-9564fad8c46d"), new DateTime(2024, 9, 11, 22, 6, 29, 485, DateTimeKind.Utc).AddTicks(3875), null, null, null, "Ice Latte", 10m },
                    { new Guid("f395abdb-b9d9-4fae-a691-e083809a6172"), new DateTime(2024, 9, 11, 22, 6, 29, 485, DateTimeKind.Utc).AddTicks(3860), null, null, null, "Latte", 15m }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedOnUtc", "ModifiedOnUtc", "Name" },
                values: new object[] { new Guid("6dd96f70-f6de-4434-97f2-cf611d3cb8a0"), new DateTime(2024, 9, 11, 22, 6, 29, 481, DateTimeKind.Utc).AddTicks(6784), null, "Administrator" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedOnUtc", "DeletedOnUtc", "Email", "EmailConfirmed", "FirstName", "LastName", "ModifiedOnUtc", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed" },
                values: new object[] { new Guid("9e935ced-b2c7-4f71-8e44-393e5736f9f0"), new DateTime(2024, 9, 11, 22, 6, 29, 485, DateTimeKind.Utc).AddTicks(3670), null, "eneskrdnz28@gmail.com", true, "Enes", "Karadeniz", null, "E53C20DA2C98C2F99EEDF39B2EBBDF34A8A915C612E5933276FB581809D6CF53-863C433952E958EDB9CA67DD8504CD70", null, false });

            migrationBuilder.InsertData(
                table: "ProductMenuCategories",
                columns: new[] { "MenuCategoryId", "ProductId" },
                values: new object[,]
                {
                    { new Guid("7e96c6ac-2c98-4a46-a2d8-707f42771fb2"), new Guid("173b8030-db4d-44a2-bb80-629842a4d7a2") },
                    { new Guid("22326620-890e-4528-b363-025eabc5779b"), new Guid("3e4fa8f5-6b33-4503-9034-73e565e04619") },
                    { new Guid("39eeace4-99f7-494f-88d3-c6c84f2e50b7"), new Guid("3e4fa8f5-6b33-4503-9034-73e565e04619") },
                    { new Guid("7e96c6ac-2c98-4a46-a2d8-707f42771fb2"), new Guid("8357b246-fc5d-4092-882e-9564fad8c46d") },
                    { new Guid("22326620-890e-4528-b363-025eabc5779b"), new Guid("f395abdb-b9d9-4fae-a691-e083809a6172") },
                    { new Guid("39eeace4-99f7-494f-88d3-c6c84f2e50b7"), new Guid("f395abdb-b9d9-4fae-a691-e083809a6172") }
                });

            migrationBuilder.InsertData(
                table: "RolePermission",
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[] { 1, new Guid("6dd96f70-f6de-4434-97f2-cf611d3cb8a0") });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("6dd96f70-f6de-4434-97f2-cf611d3cb8a0"), new Guid("9e935ced-b2c7-4f71-8e44-393e5736f9f0") });
        }
    }
}
