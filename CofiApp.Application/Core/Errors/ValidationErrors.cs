using CofiApp.Domain.Core.Primitives;

namespace CofiApp.Application.Core.Errors
{
    internal static class ValidationErrors
    {
        internal static class Users
        {
            internal static Error UserIdIsRequired = new("Authentication.UserIdIsRequired", "The user id is required.");

            internal static Error FirstNameIsRequired = new("Authentication.FirstNameIsRequired", "The first name is required.");

            internal static Error LastNameIsRequired = new("Authentication.LastNameIsRequired", "The last name is required.");
        }

        internal static class Authentication
        {
            internal static Error UserIdIsRequired = new("Authentication.UserIdIsRequired", "The user id is required.");

            internal static Error FirstNameIsRequired = new("Authentication.FirstNameIsRequired", "The first name is required.");

            internal static Error LastNameIsRequired = new("Authentication.LastNameIsRequired", "The last name is required.");

            internal static Error EmailIsRequired = new("Authentication.EmailIsRequired", "The email is required.");

            internal static Error EmailIsInvalid = new("Authentication.EmailIsInvalid", "The email is invalid.");

            internal static Error PasswordIsRequired = new("Authentication.PasswordIsRequired", "The password is required.");

            internal static Error PasswordIsTooShort = new("Authentication.PasswordIsTooShort", "The password is too short.");

            internal static Error ConfirmPasswordIsRequired = new("Authentication.ConfirmPasswordIsRequired", "The confirm password is required.");

            internal static Error ConfirmPasswordIsDifferent = new("Authentication.ConfirmPasswordIsDifferent", "The confirm password is different from the password.");

            internal static Error RefreshTokenIsRequired = new("Authentication.RefreshTokenIsRequired", "The refresh token is required.");

            internal static Error TokenIsRequired = new("Authentication.TokenIsRequired", "The token is required.");
        }

        internal static class Roles
        {
            internal static Error RoleIdIsRequired = new("Roles.RoleIdIsRequired", "The role id is required.");

            internal static Error PermissionIdIsRequired = new("Roles.PermissionIdIsRequired", "The permission id is required.");

            internal static Error UserIdIsRequired = new("Roles.UserIdIsRequired", "The user id is required.");

            internal static Error NameIsRequired = new("Roles.NameIsRequired", "The name is required.");

            internal static Error NameIsTooLong = new("Roles.NameTooLong", "The name is too long.");
        }

        internal static class MenuCategories
        {
            internal static Error IdIsRequired = new("MenuCategories.IdIsRequired", "The id is required.");
            internal static Error NameIsRequired = new("MenuCategories.NameIsRequired", "The name is required.");
            internal static Error NameIsTooLong = new("MenuCategories.NameIsTooLong", "The name is too long.");
            internal static Error UpdateMenuCategoriesDisplayOrdersIsRequired = new("MenuCategories.UpdateMenuCategoriesDisplayOrdersIsRequired", "The update menu categories display orders is required.");
            internal static Error UpdateMenuCategoriesDisplayOrdersMustBeUnique = new("MenuCategories.UpdateMenuCategoriesDisplayOrdersMustBeUnique", "The update menu categories display orders must be unique.");

        }

        internal static class Products
        {
            internal static Error MenuCategoryIdIsRequired = new("Products.MenuCategoryIdIsRequired", "The menu category id is required.");
            internal static Error IdIsRequired = new("Products.IdIsRequired", "The id is required.");
            internal static Error NameIsRequired = new("Products.NameIsRequired", "The name is required.");
            internal static Error NameIsTooLong = new("Products.NameIsTooLong", "The name is too long.");
            internal static Error DescriptionIsTooLong = new("Products.DescriptionIsTooLong", "The description is too long.");
            internal static Error PriceIsRequired = new("Products.PriceIsRequired", "The price is required.");
            internal static Error PriceIsInvalid = new("Products.PriceIsInvalid", "The price is invalid.");
        }

        internal static class ProductMenuCategories
        {
            internal static Error ProductIdIsRequired = new("ProductMenuCategories.ProductIdIsRequired", "The product id is required.");
            internal static Error MenuCategoryIdsIsRequired = new("ProductMenuCategories.MenuCategoryIdsIsRequired", "The menu category ids is required.");
        }

        internal static class ProductOptionGroups
        {
            internal static Error IdIsRequired = new("ProductOptionGroups.IdIsRequired", "The id is required.");
            internal static Error NameIsRequired = new("ProductOptionGroups.NameIsRequired", "The name is required.");
            internal static Error NameIsTooLong = new("ProductOptionGroups.NameIsTooLong", "The name is too long.");
            internal static Error IsRequiredIsRequired = new("ProductOptionGroups.IsRequiredIsRequired", "The is required is required.");
            internal static Error AllowMultipleIsRequired = new("ProductOptionGroups.AllowMultipleIsRequired", "The allow multiple is required.");
        }

        internal static class ProductOptions
        {
            internal static Error ProductOptionGroupIdIsRequired = new("ProductOptions.ProductOptionGroupIdIsRequired", "The product option group id is required.");
            internal static Error IdIsRequired = new("ProductOptions.IdIsRequired", "The id is required.");
            internal static Error NameIsRequired = new("ProductOptions.NameIsRequired", "The name is required.");
            internal static Error NameIsTooLong = new("ProductOptions.NameIsTooLong", "The name is too long.");
            internal static Error PriceIsRequired = new("ProductOptions.PriceIsRequired", "The price is required.");
        }

        internal static class Baskets
        {
            internal static Error ProductIdIsRequired = new("Baskets.ProductIdIsRequired", "The product id is required.");
            internal static Error QuantityMustBeGreaterThanZero = new("Baskets.QuantityMustBeGreaterThanZero", "The quantity must be greater than zero.");
            internal static Error BasketItemIdIsRequired = new("Baskets.BasketItemIdIsRequired", "The basket item id is required.");
            internal static Error IsIncreaseIsRequired = new("Baskets.IsIncreaseIsRequired", "The is increase is required.");
        }

        internal static class Orders
        {
            internal static Error OrderIdIsRequired = new("Orders.OrderIdIsRequired", "The order id is required.");
            internal static Error OrderStatusIsRequired = new("Orders.OrderStatusIsRequired", "The order status is required.");
            internal static Error OrderStatusIsInvalid = new("Orders.OrderStatusIsInvalid", "The order status is invalid.");
        }

        internal static class ProductImageFiles
        {
            internal static Error ProductIdIsRequired = new("ProductImageFiles.ProductIdIsRequired", "The product id is required.");
            internal static Error ImageFileIsRequired = new("ProductImageFiles.ImageFileIsRequired", "The image file is required.");
        }
    }
}
