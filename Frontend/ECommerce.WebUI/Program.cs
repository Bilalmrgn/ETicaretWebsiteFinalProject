using ECommerce.WebUI.Services;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

//IHTTPClientFactory k�sm�n� kullanabilmem i�in bunu entegre ettim. IHttpClientFactory nin amac� farkl� api lere istek g�ndermek ve bu istekleri i�lemek
builder.Services.AddHttpClient();

//Service Registration (IoC)
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddHttpContextAccessor();
//Authentication konfigurasyonlar�

//cookie authentication ekle. ��nk� MVC uygulamas�, kullan�c�y� cookie �zerinden tan�yacakt�r
builder.Services.AddAuthentication("Cookies")
    .AddCookie("Cookies", options =>
    {
        options.Cookie.Name = "ECommerceCookie"; // �ereze bir isim ver
        options.LoginPath = "/Login/Index";
        options.LogoutPath = "/Login/LogOut";
        options.AccessDeniedPath = "/Error/Index";
        options.ExpireTimeSpan = TimeSpan.FromHours(3);
        options.SlidingExpiration = true; // Kullan�c� i�lem yapt�k�a s�re uzas�n
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        options.Cookie.HttpOnly = true; // G�venlik i�in (XSS korumas�)
        options.Events = new Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationEvents
        {
            OnSigningIn = context =>
            {
                // Tokenlar�n mevcut oldu�undan emin olur
                return Task.CompletedTask;
            }
        };
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
