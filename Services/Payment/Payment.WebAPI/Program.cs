using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Payment.WebAPI.Context;
using Payment.WebAPI.Services;
using Payment.WebAPI.Services.Concrete;
using Payment.WebAPI.Services.Interface;
using Shared.RabbitMQ.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// dependency injection
builder.Services.AddScoped<ICreditCardService,CreditCardService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();

//rabbitmq baðlantýsý
builder.Services.Configure<RabbitMqSettings>(
    builder.Configuration.GetSection("RabbitMqSettings"));

builder.Services.AddScoped<RabbitMqPublisher>();



builder.Services.AddSwaggerGen();
//veritabaný baðlantýsý
var connectionString = builder.Configuration.GetConnectionString("DbConnection");

// DbContext ekle
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.Authority = builder.Configuration["IdentityServerUrl"];
    options.Audience = "payment_microservice";//IdentityServer mikroservisimdeki config dosyasýndki discount api resource dosya
    options.RequireHttpsMetadata = false;

});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
