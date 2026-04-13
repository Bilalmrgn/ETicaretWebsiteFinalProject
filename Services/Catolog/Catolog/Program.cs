using Catolog.Mapping;
using Catolog.Services.BrandService;
using Catolog.Services.CategoryServices;
using Catolog.Services.FeatureSliderService;
using Catolog.Services.ProductDetailDetailServices;
using Catolog.Services.ProductDetailServices;
using Catolog.Services.ProductImagesServices;
using Catolog.Services.ProductServices;
using Catolog.Services.SpecialOfferService;
using Catolog.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using System.Reflection;

using System.IdentityModel.Tokens.Jwt;

var builder = WebApplication.CreateBuilder(args);

// Claim mapping'i temizleyerek JWT içindeki role ve scope gibi alanların .NET tarafından değiştirilmesini engelliyoruz.
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();


//mikroservisin kormua altına alınması
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    //bu aralıkta 3 tane parametre geçilir.
    //1. parametre
    options.Authority = builder.Configuration["IdentityServerUrl"];//appsettings deki IdentityServerUrl kısmı
    options.RequireHttpsMetadata = false;
    //3.parametre
    options.Audience = "catalog_microservice";//burada katolog mikroservisini ayağa kaldırdığım için identity deki config dosyamın resource kısmındaki yeri okur. yani catalog_microservice
    
    //claim için bunu ekledik
    options.TokenValidationParameters = new TokenValidationParameters
    {
        NameClaimType = "name",
        RoleClaimType = "role"
    };

});



// Servislerin DI kaydı: Controller içinde kullanabilmek için
builder.Services.AddScoped<ICategoryServices, CategoryServices>();//uygulamada ICategoryService istendiğinde arka planda categoryservice servisim kullanılsın
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IFeatureSliderService, FeatureSliderService>();
builder.Services.AddScoped<IProductDetailServices, ProductDetailServices>();
builder.Services.AddScoped<IProductImageService, ProductImagesService>();
builder.Services.AddScoped<ISpecialOfferService, SpecialOfferService>();
builder.Services.AddScoped<IBrandService, BrandService>();

//automapper konfigürasyonu
//builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

//appsettings dosyamdaki verilere ulaşmak için
builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("DatabaseSettings"));

builder.Services.AddScoped<IDatabaseSettings>(sp =>
{
    return sp.GetRequiredService<IOptions<DatabaseSettings>>().Value;
});
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// -------- MongoDB ve Service Kayıtları -----------//

//appsettings içindeki databasesettings kısmını DatabaseSettings sınıfına bağlar
builder.Services.Configure<DatabaseSettings>(
    builder.Configuration.GetSection("DatabaseSettings"));

//her servisimde tekrar tekrar bağlantıyı yazmamak için tek yerden yönetim
builder.Services.AddSingleton<MongoContext>();

// AutoMapper: Entity ↔ DTO dönüşümlerinin olması için
builder.Services.AddAutoMapper(typeof(GeneralMapping));




//authorzation için policy tanımlama
builder.Services.AddAuthorization(opt =>
{
    opt.AddPolicy("CatalogRead", policy => policy.RequireClaim("scope", "catalog.read", "catalog.full"));
    opt.AddPolicy("CatalogWrite", policy =>
        policy.RequireClaim("scope", "catalog.full"));

    //Admin kontrolü
    opt.AddPolicy("AdminOnly", policy =>
        policy.RequireRole("Admin"));

});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();//mikroservisi koruma altına almak için
app.UseAuthorization();

app.MapControllers();

app.Run();
