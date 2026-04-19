using ECommerce.WebUI.Handlers;
using ECommerce.WebUI.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using static Duende.IdentityModel.ClaimComparer;
using TokenHandler = ECommerce.WebUI.Handlers.TokenHandler;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient();

//handler sınıfları
builder.Services.AddTransient<TokenHandler>();

//brand api portu
builder.Services.AddHttpClient("CatalogClient", client =>
{
    client.BaseAddress = new Uri("https://localhost:7185/");
}).AddHttpMessageHandler<TokenHandler>();

//contact
builder.Services.AddHttpClient("ContactClient", client =>
{
    client.BaseAddress = new Uri("https://localhost:7185/");
}).AddHttpMessageHandler<TokenHandler>();


builder.Services.AddHttpClient("IdentityClient", client =>
{
    client.BaseAddress = new Uri("https://localhost:7185/");
}).AddHttpMessageHandler<TokenHandler>();


builder.Services.AddHttpClient("CommentClient", client =>
{
    client.BaseAddress = new Uri("https://localhost:7185");
}).AddHttpMessageHandler<TokenHandler>();

builder.Services.AddHttpClient("FavoriteClient", client =>
{
    client.BaseAddress = new Uri("https://localhost:7135");
}).AddHttpMessageHandler<TokenHandler>();

builder.Services.AddHttpClient("BasketClient", client =>
{
    client.BaseAddress = new Uri("https://localhost:7178");
}).AddHttpMessageHandler<TokenHandler>();






















//Service Registration (IoC)
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddHttpContextAccessor();
//Authentication konfigurasyonlar�
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
builder.Services.AddAuthentication(option =>
{
    option.DefaultScheme = "Cookies";
    option.DefaultChallengeScheme = "oidc";
})
    .AddCookie("Cookies")
    .AddOpenIdConnect("oidc", options =>
    {
        options.Authority = "https://localhost:7222"; // IdentityServer Portun
        options.ClientId = "ECommerceAdminId"; // En geniş yetkili Client
        options.ClientSecret = "ecommercesecret";
        options.ResponseType = "code"; // Authorization Code flow

        options.SaveTokens = true;
        options.GetClaimsFromUserInfoEndpoint = false;

        options.Scope.Add("openid");
        options.Scope.Add("profile");
        options.Scope.Add("email");
        options.Scope.Add("catalog.full");
        options.Scope.Add("catalog.read");
        options.Scope.Add("order.full");
        options.Scope.Add("order.getAllOrder");
        options.Scope.Add("cargo.full");
        options.Scope.Add("basket.full");
        options.Scope.Add("comment.full");
        options.Scope.Add("comment.read");
        options.Scope.Add("discount.full");
        options.Scope.Add("contact.full");
        options.Scope.Add("offline_access");
        options.Scope.Add("favorite.full");
        options.Scope.Add("Identity.full");
        options.Scope.Add("roles");

        // 1. JWT içindeki "role" claim'ini yakala ve sistemin "Role" tipine eşle
        options.ClaimActions.MapJsonKey("role", "role"); // JSON içindeki role'ü claim'e haritala
        options.TokenValidationParameters = new TokenValidationParameters
        {
            NameClaimType = "name",
            RoleClaimType = "role" // User.IsInRole("Admin") çalışması için şart
        };

        // Eğer roller hala gelmiyorsa şunu ekle (IdentityServer rollerini UserInfo endpoint'inden çeker):
        options.GetClaimsFromUserInfoEndpoint = false;
    });




var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
