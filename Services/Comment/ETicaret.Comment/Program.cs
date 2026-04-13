using ETicaret.Comment.Context;
using ETicaret.Comment.Services.CommentService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var configuration = builder.Configuration;

//veritabaný connection kýsmý
builder.Services.AddDbContext<CommentDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DbConnection")));

//dependency injection
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddHttpContextAccessor();
//comment mikroservisini koruma altýna alma

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.Authority = builder.Configuration["IdentityServerUrl"];
    options.Audience = "comment_microservice";
    options.RequireHttpsMetadata = false;
});


//authorization policy konfigurasyonu
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("CommentFullPermission", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("scope", "comment.full");
    });
    options.AddPolicy("CommentReadPermission", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("scope", "comment.read");
    });
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
