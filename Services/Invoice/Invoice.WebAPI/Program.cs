using Invoice.WebAPI.Consume;
using Invoice.WebAPI.Context;
using Invoice.WebAPI.Publisher;
using Invoice.WebAPI.Services.Concrete;
using Invoice.WebAPI.Services.Interface;
using Microsoft.EntityFrameworkCore;
using Shared.RabbitMQ.Settings;

var builder = WebApplication.CreateBuilder(args);

// QuestPDF License
QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;

// Add services to the container.

//veritabanı bağlantısı
var connectionString = builder.Configuration.GetConnectionString("DbConnection");

// DbContext ekle
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

//rabbitmq bağlantısı
builder.Services.Configure<RabbitMqSettings>(
    builder.Configuration.GetSection("RabbitMqSettings"));

builder.Services.AddScoped<IInvoiceCreatedPublisher,InvoiceCreatedPublisher>();
builder.Services.AddScoped<IInvoiceService, InvoiceService>();
builder.Services.AddScoped<IPdfService,PdfService>();


builder.Services.AddHostedService
    <PaymentCompletedConsumer>();



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
