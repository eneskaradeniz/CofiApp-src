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
        UpdateMenuCategoriesDisplayOrder = 19,

        GetProducts = 20,
        GetProductById = 21,
        CreateProduct = 22,
        UpdateProduct = 23,
        RemoveProduct = 24,
        UpdateProductMenuCategories = 25,

        GetProductOptionGroupsWithOptions = 26,
        CreateProductOptionGroup = 27,
        UpdateProductOptionGroup = 28,
        RemoveProductOptionGroup = 29,

        CreateProductOption = 30,
        UpdateProductOption = 31,
        RemoveProductOption = 32,
        
        GetActiveBasket = 33,
        CreateBasketItem = 34,
        UpdateBasketItem = 35,
        UpdateBasketItemQuantity = 36,
        ClearBasket = 37,

        GetShopOrders = 38,
        GetShopOrderById = 39,
        CancelShopOrder = 40,
        ProcessShopOrder = 41,
        CompleteShopOrder = 42,

        GetCustomerOrders = 43,
        GetCustomerOrderById = 44,
        CreateCustomerOrder = 45,
        CancelCustomerOrder = 46,
        
        UploadProductImageFile = 47,
        RemoveProductImageFile = 48,
    }
}
