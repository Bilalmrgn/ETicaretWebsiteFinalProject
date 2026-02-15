using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace IdentityServer.WebAPI
{
    /*
     Config sınıfının amacı:
     IdentityServer'a şunları öğretmek   
     yani hangi mikroservisler var
     bu servislerde hangi yetkiler olacak
     
     */

    //program.cs dosyasında add lendi
    public static class Config
    {
        //bu sınıfın amacı: benim catolog mikroservisim var ve bu mikroserviste ful ve sadece okuma izinleri yapacağım
        //ApiResource --> Hangi API'ler korunuyor
        public static IEnumerable<ApiResource> ApiSResources => new ApiResource[]
        {
            new ApiResource("catalog_microservice")//benim resourceCatolog isminde bir mikroservisim var ve bu mikroservisimi korumaya alacağım
            {
                Scopes = {"catalog.full","catalog.read"},//yani bu catolog servisimde tam yetki alabilir, sadece okuma yetkisi alabilir
            },
            new ApiResource("discount_microservice")
            {
                Scopes = { "discount.full"}
            },
            new ApiResource("order_microservice")
            {
                Scopes = {"order.full","order.getAllOrder"}
            },
            new ApiResource(IdentityServerConstants.LocalApi.ScopeName)
            /*
             new ApiResource("catalog_microservice")
             {
                    Scopes = { "catalog.full", "catalog.read" }
             }

            catolog_mivroservice api si şu scope lar ile korunuyor.


                ApiScope  →  Scope tanımı
                ApiResource → Scope hangi API’ye ait
                Client → Scope’u kim alabilir
             */

        };

        public static IEnumerable<IdentityResource> IdentityResources => new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Email(),
            new IdentityResources.Profile(),
        };


        public static IEnumerable<ApiScope> ApiScopes => new ApiScope[]
        {
            new ApiScope("catalog.full","katolog işlemlerine tam yetki"),
            new ApiScope("catalog.read","Katolog işlemlerinde okuma yetkisi"),
            new ApiScope("discount.full","discount' a tam yetki"),
            new ApiScope("order.full","Order' a tam yetki"),
            new ApiScope("order.getAllOrder","Order'ı getirme işlemi"),
            new ApiScope(IdentityServerConstants.LocalApi.ScopeName)
        };

        public static IEnumerable<Client> Clients => new Client[]
        {
            //ziyaretçinin sahip olduğu izinler burada verilecek
            new Client
            {
                ClientId = "ECommerceVisitorId",
                ClientName = "ECommerce visitor user",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = {new Secret("ecommercesecret".Sha256())},
                AllowedScopes = {"catalog.read"},//visitor katalogları okuyabilme yetkisi verdik
            },

            //Manager'ın sahip olacağı izinler burada verilecek
            new Client
            {
                ClientId = "ECommerceManagerId",
                ClientName = "ECommerce manager user",
                AllowedGrantTypes= GrantTypes.ClientCredentials,
                ClientSecrets = {new Secret("ecommercesecret".Sha256())},
                AllowedScopes = {"catalog.full", "order.getAllOrder" }

            },

            //Admin in sahip olacağı izinler burada verilecek.
            new Client
            {
                ClientId = "ECommerceAdminId",
                ClientName = "ECommerce admin user",
                AllowedGrantTypes= GrantTypes.ClientCredentials,
                ClientSecrets = {new Secret("ecommercesecret".Sha256())},
                AllowedScopes = {"catalog.full", "order.full" , "discount.full" ,IdentityServerConstants.LocalApi.ScopeName,
                IdentityServerConstants.LocalApi.ScopeName,
                IdentityServerConstants.StandardScopes.Email,
                IdentityServerConstants.StandardScopes.OpenId,
                IdentityServerConstants.StandardScopes.Profile},
                AccessTokenLifetime = 600//token süresi

            },
        };
    }
}


