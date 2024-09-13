namespace CofiApp.Api.Contracts
{
    public static class ApiRoutes
    {
        public static class Authentication
        {
            public const string Login = "auth/login";

            public const string Register = "auth/register";

            public const string RefreshToken = "auth/refresh-token";

            public const string ForgotPassword = "auth/forgot-password";

            public const string ResetPassword = "auth/reset-password";

            public const string EmailConfirmation = "auth/email-confirmation";
        }

        public static class Users
        {
            // Admin

            public const string Get = "users";

            public const string GetById = "users/{userId:guid}";

            public const string Create = "users";

            public const string Update = "users/{userId:guid}";

            public const string Remove = "users/{userId:guid}";

            // Customer

            public const string ChangePassword = "users/{userId:guid}/change-password";

            public const string UpdateMyProfile = "users/my-profile";

            public const string GetMyProfile = "users/my-profile";
        }

        public static class Roles
        {
            public const string Get = "roles";

            public const string Create = "roles";

            public const string Update = "roles/{roleId:guid}";

            public const string Remove = "roles/{roleId:guid}";

            public const string GetPermissions = "roles/permissions";

            public const string AssignPermission = "roles/{roleId:guid}/permissions/{permissionId}";

            public const string AssignUser = "roles/{roleId:guid}/users/{userId:guid}";
        }

        public static class MenuCategories
        {
            // Shop

            public const string Get = "menu-categories";

            public const string GetById = "menu-categories/{menuCategoryId:guid}";

            public const string Create = "menu-categories";

            public const string Update = "menu-categories/{menuCategoryId:guid}";

            public const string Remove = "menu-categories/{menuCategoryId:guid}";

            // Customer

            public const string PublicGet = "public/menu-categories";

            public const string PublicGetWithProducts = "public/menu-categories/{menuCategoryId:guid}/products";
        }

        public static class Products
        {
            // Shop

            public const string Get = "products";

            public const string GetById = "products/{productId:guid}";

            public const string Create = "products";

            public const string Update = "products/{productId:guid}";

            public const string Remove = "products/{productId:guid}";

            public const string UpdateProductMenuCategories = "products/{productId:guid}/menu-categories";

            // Customer

            public const string PublicGetById = "public/products/{productId:guid}"; // ürünü ve tüm opsiyonlarla birlikte getir
        }

        public static class ProductOptionGroups
        {
            // Shop

            public const string GetWithOptions = "product-option-groups/{productId:guid}/options"; // ürünün gruplarını opsiyonları ile birlikte getir

            public const string Create = "product-option-groups/{productId:guid}"; // ürüne grup oluştur

            public const string Update = "product-option-groups/{productOptionGroupId:guid}"; // ürünün oluşan grubunu düzenle

            public const string Remove = "product-option-groups/{productOptionGroupId:guid}"; // ürünün oluşan grubunu sil
        }

        public static class ProductOptions
        {
            public const string Create = "product-options/{productOptionGroupId:guid}"; // ürünün grubuna opsiyon ekle

            public const string Update = "product-options/{productOptionId:guid}"; // ürünün grubunun opsiyonunu düzenle

            public const string Remove = "product-options/{productOptionId:guid}"; // ürünün grubunun opsiyonunu sil
        }

        public static class Baskets
        {
            // Customer

            public const string GetActiveBasket = "baskets"; // sepetteki ürünleri getir

            public const string CreateBasketItem = "baskets"; // sepete ürün ekle

            public const string UpdateBasketItem = "baskets/{basketItemId:guid}"; // sepetteki ürünü güncelle
         
            public const string UpdateBasketItemQuantity = "baskets/{basketItemId:guid}/quantity"; // sepetteki ürünün miktarını arttır azalt

            public const string Clear = "baskets/clear"; // sepeti temizle
        }

        public static class Orders
        {
            // Shop

            public const string Get = "orders"; // siparişleri getir

            public const string GetById = "orders/{orderId:guid}"; // siparişi detaylı getir

            public const string Update = "orders/{orderId:guid}"; // siparişi güncelle

            // Customer        

            public const string PublicGet = "orders"; // siparişleri getir

            public const string PublicGetById = "orders/{orderId:guid}"; // siparişi detaylı getir

            public const string PublicCreate = "orders"; // sipariş oluştur

            public const string PublicCancel = "orders/{orderId:guid}/cancel"; // siparişi iptal et
        }
    }
}
