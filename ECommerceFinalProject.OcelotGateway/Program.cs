using Microsoft.AspNetCore.Authentication.JwtBearer;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

//1. ocelot.json dosyasýný konfigurasyona ekle
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

//2.authentication ekle. çünkü mikroservislerde authorize olduðu için gateway'in de token'ý tanýmasý gerekiyor
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer("CatalogAuthKey", options =>
    {
        options.Authority = builder.Configuration["IdentityServerUrl"]; 
        options.Audience = "catalog_microservice"; // Catalog için izin alacak
        options.RequireHttpsMetadata = false;
    });

//3. Ocelot servislerini ekleycez
builder.Services.AddOcelot();


var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

await app.UseOcelot();

app.MapGet("/", () => "Hello World!");

app.Run();


