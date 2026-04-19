using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace IdentityServer.WebAPI
{

    public static class Config
    {
        #region Resources
        public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
        {
            new ApiResource("catalog_microservice")//benim resourceCatolog isminde bir mikroservisim var ve bu mikroservisimi korumaya alacağım
            {
                Scopes = {"catalog.full","catalog.read"},
                UserClaims = { "role" }//yani bu catolog servisimde tam yetki alabilir, sadece okuma yetkisi alabilir
            },

            //favorite işlemleri için
            new ApiResource("favorite_microservice")
            {
                Scopes = {"favorite.full"},
                UserClaims = { "role" }
            },
            new ApiResource("Identity_microservice")
            {
                Scopes = {"Identity.full"},
                UserClaims = { "role" }
            },

            new ApiResource("discount_microservice")
            {
                Scopes = { "discount.full"},
                UserClaims = { "role" }
            },

            new ApiResource("comment_microservice")
            {
                Scopes = { "comment.full","comment.read"},
                UserClaims = { "role" }
            },

            new ApiResource("order_microservice")
            {
                Scopes = {"order.full","order.getAllOrder"},
                UserClaims = { "role" }
            },

            new ApiResource("basket_microservice")
            {
                Scopes = { "basket.full"},
                UserClaims = { "role" } // Basket mikroservisi için "role" claim'ini ekleyelim
            },

            new ApiResource("contact_microservice")
            {
                Scopes = { "contact.full","contact.create"},
                UserClaims = { "role" }
            },

            new ApiResource("cargo_microservice")//benim catalog_cargo isminde bir mikroservisim var ve bu mikroservisimi korumaya alacağım
            {
                Scopes = {"cargo.full","cargo.read"},//yani bu catolog servisimde tam yetki alabilir, sadece okuma yetkisi alabilir
            },
            new ApiResource(IdentityServerConstants.LocalApi.ScopeName)

        };

        #endregion

        public static IEnumerable<IdentityResource> IdentityResources => new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Email(),
            new IdentityResources.Profile(),
            new IdentityResource("roles", "User Roles", new[] { "role" })
        };


        #region Scopes
        public static IEnumerable<ApiScope> ApiScopes => new ApiScope[]
        {
            new ApiScope("catalog.full","katolog işlemlerine tam yetki"),
            new ApiScope("Identity.full","identity işlemlerine tam yetki"),
            new ApiScope("favorite.full","favori işlemlerine tam yetki"),
            new ApiScope("catalog.read","Katolog işlemlerinde okuma yetkisi"),
            new ApiScope("discount.full","discount' a tam yetki"),
            new ApiScope("order.full","Order' a tam yetki"),
            new ApiScope("order.getAllOrder","Order'ı getirme işlemi"),
            new ApiScope("cargo.full","kargo işlemlerine tam yetki"),
            new ApiScope("cargo.read","kargo işlemlerine okuma yetkisi"),
            new ApiScope("basket.full","basket işlemlerine full işlem yetkisi"),
            new ApiScope("comment.full","comment işlemlerine full işlem yetkisi"),
            new ApiScope("comment.read","comment işlemlerine okuma işlem yetkisi"),
            new ApiScope("contact.full","contact işlemlerine tam işlem yetkisi"),
            new ApiScope("contact.create","contact işlemlerine oluşturma işlem yetkisi"),
            new ApiScope(IdentityServerConstants.LocalApi.ScopeName)
        };
        #endregion

        public static IEnumerable<Client> Clients => new Client[]
        {
            //ziyaretçinin sahip olduğu izinler burada verilecek
            new Client
            {
                ClientId = "ECommerceVisitorId",
                ClientName = "ECommerce visitor user",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = {new Secret("ecommercesecret".Sha256())},
                AllowedScopes = {"catalog.read","cargo.read","comment.read","contact.create",IdentityServerConstants.StandardScopes.OfflineAccess},//token içerisine bu alanları yazdırdım scope kısmına
            
            },

            //Manager'ın sahip olacağı izinler burada verilecek
            new Client
            {
                ClientId = "ECommerceCustomerId",
                ClientName = "ECommerce Customer user",
                AllowedGrantTypes= GrantTypes.Code,
                RequirePkce = false,
                RequireClientSecret = true,

                ClientSecrets = {new Secret("ecommercesecret".Sha256())},

                //giriş yaptıktan sonra dönülecek adres
                RedirectUris = { "https://localhost:7145/signin-oidc", "https://oauth.pstmn.io/v1/browser-callback", "https://oauth.pstmn.io/v1/callback" },

                //çıkış yaptıktan sonra dönülecek adres
                PostLogoutRedirectUris = { "https://localhost:7145/signout-callback-oidc" },

                AllowedScopes = {
                    "favorite.full",
                    "Identity.full",
                    "catalog.full",
                    "catalog.read",
                    "order.getAllOrder",
                    "comment.full",
                    "basket.full",
                    "contact.create",
                    "roles",
                    IdentityServerConstants.StandardScopes.OfflineAccess,
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile
                },
                AllowOfflineAccess = true,
            },

            //Admin in sahip olacağı izinler burada verilecek.
            new Client
            {
                ClientId = "ECommerceAdminId",
                ClientName = "ECommerce admin user",
                AllowedGrantTypes= GrantTypes.Code,
                RequirePkce = false,
                RequireClientSecret = true,

                ClientSecrets = {new Secret("ecommercesecret".Sha256())},

                //giriş yaptıktan sonra dönülecek adres
                RedirectUris = { "https://localhost:7145/signin-oidc", "https://oauth.pstmn.io/v1/browser-callback", "https://oauth.pstmn.io/v1/callback" },

                //çıkış yaptıktan sonra dönülecek adres
                PostLogoutRedirectUris = { "https://localhost:7145/signout-callback-oidc" },

                AllowedScopes = {
                    "favorite.full",
    "roles",
    "catalog.full",
    "Identity.full",
    "catalog.read",
    "order.full",
    "order.getAllOrder",
    "discount.full",
    "cargo.full",
    "basket.full",
    "comment.full",
    "comment.read",
    "contact.full", // <--- Burayı kontrol et, listede olduğundan emin ol
    IdentityServerConstants.StandardScopes.OfflineAccess,
    IdentityServerConstants.LocalApi.ScopeName,
    IdentityServerConstants.StandardScopes.Email,
    IdentityServerConstants.StandardScopes.OpenId,
    IdentityServerConstants.StandardScopes.Profile
},
                /*AccessTokenLifetime = 604800,//token süresi
                AllowOfflineAccess = true//token imin süresi bittiğinde bu satır sayesinde refresh token üretirim*/

                AccessTokenLifetime = 2592000, // 30 gün

                AllowOfflineAccess = true,

                AbsoluteRefreshTokenLifetime = 7776000, // 90 gün
                SlidingRefreshTokenLifetime = 2592000,  // 30 gün

            },
        };
    }
}


