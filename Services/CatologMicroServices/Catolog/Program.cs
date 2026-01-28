using Catolog.Mapping;
using Catolog.Services.CategoryServices;
using Catolog.Services.ProductDetailDetailServices;
using Catolog.Services.ProductDetailServices;
using Catolog.Services.ProductImagesServices;
using Catolog.Services.ProductServices;
using Catolog.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Servislerin DI kaydı: Controller içinde kullanabilmek için
builder.Services.AddScoped<ICategoryServices, CategoryServices>();//uygulamada ICategoryService istendiğinde arka planda categoryservice servisim kullanılsın
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductDetailServices, ProductDetailServices>();
builder.Services.AddScoped<IProductImageService,ProductImagesService>();

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




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
