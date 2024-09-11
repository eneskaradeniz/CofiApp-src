namespace CofiApp.Domain.Enums
{
    public enum Permission
    {
        All = 1,

        GetUsers = 2,
        GetUserById = 3,
        CreateUser = 4,
        UpdateUser = 5,
        RemoveUser = 6,

        GetRoles = 7,
        CreateRole = 8,
        UpdateRole = 9,
        RemoveRole = 10,
        GetRolesPermissions = 11,
        AssignPermission = 12,
        AssignUser = 13,

        GetMenuCategories = 14,
        GetMenuCategoryById = 15,
        CreateMenuCategory = 16,
        UpdateMenuCategory = 17,
        RemoveMenuCategory = 18,

        GetProducts = 19,
        GetProductById = 20,
        CreateProduct = 21,
        UpdateProduct = 22,
        RemoveProduct = 23,
        UpdateProductMenuCategories = 24,

        GetProductOptionGroupsWithOptions = 25,
        CreateProductOptionGroup = 26,
        UpdateProductOptionGroup = 27,
        RemoveProductOptionGroup = 28,
    }
}
