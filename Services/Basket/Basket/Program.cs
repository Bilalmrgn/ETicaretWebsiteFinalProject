using Basket.Service.Concrete;
using Basket.Service.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;

var builder = WebApplication.CreateBuilder(args);

var requireAuthorizePolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");

//proje seviyesinde authorization
builder.Services.AddControllers(options =>
{
    options.Filters.Add(new AuthorizeFilter(requireAuthorizePolicy));

});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//mikroservisin koruma altýna alýnmasý
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.Authority = builder.Configuration["IdentityServerUrl"];
    options.Audience = "basket_microservice";
    options.RequireHttpsMetadata = false;
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IBasketService, BasketService>();
builder.Services.AddScoped<IRedisService, RedisService>();
builder.Services.AddScoped<IDiscountService, DiscountService>();
builder.Services.AddHttpClient();

var redisHost = builder.Configuration["RedisHost"];


//distributed cache Redis configuration
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = redisHost;
});

builder.Services.AddAuthorization(opt =>
{
    opt.AddPolicy("CatalogRead", policy => policy.RequireClaim("scope", "catalog.read", "catalog.full"));
    opt.AddPolicy("CatalogWrite", policy =>
        policy.RequireClaim("scope", "catalog.full"));

    //Admin kontrolü
    opt.AddPolicy("AdminOnly", policy =>
        policy.RequireRole("Admin"));

});
//authorization policy configuration
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("BasketFullPermission", policy => policy.RequireClaim("scope", "basket.full"));
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
