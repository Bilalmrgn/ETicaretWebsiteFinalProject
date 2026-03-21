using ECommerce.WebUI.Services;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

//IHTTPClientFactory kýsmýný kullanabilmem için bunu entegre ettim. IHttpClientFactory nin amacý farklý api lere istek göndermek ve bu istekleri iþlemek
builder.Services.AddHttpClient();

//Service Registration (IoC)
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddHttpContextAccessor();
//Authentication konfigurasyonlarý

//cookie authentication ekle. Çünkü MVC uygulamasý, kullanýcýyý cookie üzerinden tanýyacaktýr
builder.Services.AddAuthentication("Cookies")
    .AddCookie("Cookies", options =>
    {
        options.Cookie.Name = "ECommerceCookie"; // Çereze bir isim ver
        options.LoginPath = "/Login/Index";
        options.LogoutPath = "/Login/LogOut";
        options.AccessDeniedPath = "/Error/Index";
        options.ExpireTimeSpan = TimeSpan.FromHours(3);
        options.SlidingExpiration = true; // Kullanýcý iþlem yaptýkça süre uzasýn
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        options.Cookie.HttpOnly = true; // Güvenlik için (XSS korumasý)
        options.Events = new Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationEvents
        {
            OnSigningIn = context =>
            {
                // Tokenlarýn mevcut olduðundan emin olur
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
