namespace CofiApp.Api.Contracts
{
    public static class ApiRoutes
    {
        public static class Public
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

            public static class MenuCategories
            {
                public const string Get = "menu-categories";

                public const string GetByIdWithProducts = "menu-categories/{menuCategoryId:guid}/products";
            }

            public static class Products
            {
                public const string GetByIdWithDetails = "products/{productId:guid}/details";
            }
        }
        
        public static class Admin
        {
            public static class Users
            {
                public const string Get = "users";

                public const string GetById = "users/{userId:guid}";

                public const string Create = "users";

                public const string Update = "users/{userId:guid}";

                public const string Remove = "users/{userId:guid}";
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
        }

        public static class Shop
        {
            public static class MenuCategories
            {
                public const string Get = "shop/menu-categories";

                public const string GetById = "shop/menu-categories/{menuCategoryId:guid}";

                public const string Create = "shop/menu-categories";

                public const string Update = "shop/menu-categories/{menuCategoryId:guid}";

                public const string Remove = "shop/menu-categories/{menuCategoryId:guid}";

                public const string UpdateDisplayOrder = "shop/menu-categories/display-order";
            }

            public static class Products
            {
                public const string Get = "shop/products";

                public const string GetById = "shop/products/{productId:guid}";

                public const string Create = "shop/products";

                public const string Update = "shop/products/{productId:guid}";

                public const string Remove = "shop/products/{productId:guid}";

                public const string UpdateProductMenuCategories = "shop/products/{productId:guid}/menu-categories";
            }

            public static class ProductOptionGroups
            {
                public const string GetWithOptions = "shop/product-option-groups/{productId:guid}/options"; // ürünün gruplarını opsiyonları ile birlikte getir

                public const string Create = "shop/product-option-groups/{productId:guid}"; // ürüne grup oluştur

                public const string Update = "shop/product-option-groups/{productOptionGroupId:guid}"; // ürünün oluşan grubunu düzenle

                public const string Remove = "shop/product-option-groups/{productOptionGroupId:guid}"; // ürünün oluşan grubunu sil
            }

            public static class ProductOptions
            {
                public const string Create = "shop/product-options/{productOptionGroupId:guid}"; // ürünün grubuna opsiyon ekle

                public const string Update = "shop/product-options/{productOptionId:guid}"; // ürünün grubunun opsiyonunu düzenle

                public const string Remove = "shop/product-options/{productOptionId:guid}"; // ürünün grubunun opsiyonunu sil
            }

            public static class Orders
            {
                public const string Get = "shop/orders"; // GET siparişleri getir

                public const string GetById = "shop/orders/{orderId:guid}"; // GET siparişi detaylı getir

                public const string Cancel = "shop/orders/{orderId:guid}/cancel"; // PATCH siparişi iptal et

                public const string Process = "shop/orders/{orderId:guid}/process"; // PATCH siparişi işleme al

                public const string Complete = "shop/orders/{orderId:guid}/complete"; // PATCH siparişi tamamla
            }
        }

        public static class Customer
        {
            public static class Users
            {
                public const string GetMyProfile = "users/my-profile";

                public const string UpdateMyProfile = "users/my-profile";
            }

            public static class Baskets
            {
                public const string GetActiveBasket = "customer/baskets"; // GET sepetteki ürünleri getir

                public const string CreateBasketItem = "customer/baskets"; // POST sepete ürün ekle

                public const string UpdateBasketItem = "customer/baskets/{basketItemId:guid}"; // PUT sepetteki ürünü güncelle

                public const string UpdateBasketItemQuantity = "customer/baskets/{basketItemId:guid}/quantity"; // PATCH sepetteki ürünün miktarını arttır azalt

                public const string ClearBasket = "customer/baskets/clear"; // POST sepeti temizle
            }

            public static class Orders
            {
                public const string Get = "customer/orders"; // GET siparişleri getir

                public const string GetById = "customer/orders/{orderId:guid}"; // GET siparişi detaylı getir

                public const string Create = "customer/orders"; // POST sipariş oluştur

                public const string Cancel = "customer/orders/{orderId:guid}/cancel"; // PATCH siparişi iptal et
            }
        }
    }
}
