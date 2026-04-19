using Duende.IdentityServer.Services;
using IdentityServer.Domain;
using IdentityServer.Persistence.Concrete;
using IdentityServer.Persistence.ServiceRegistration;
using IdentityServer.WebAPI;
using System.IdentityModel.Tokens.Jwt;

var builder = WebApplication.CreateBuilder(args);
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

// Add services to the container.

builder.Services.AddControllersWithViews(); // MVC desteği eklendi
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<CustomProfileService>();
/*builder.Services.AddScoped<IProfileService, ProfileService>();*/

//persistence service registration
builder.Services.AddDatabase(builder.Configuration);

//IdentityServer,Controller
builder.Services
    .AddIdentityServer(opt =>
    {
        opt.EmitStaticAudienceClaim = true;
    })
    .AddInMemoryApiResources(Config.ApiResources)
    .AddInMemoryApiScopes(Config.ApiScopes)
    .AddInMemoryIdentityResources(Config.IdentityResources)
    .AddInMemoryClients(Config.Clients)
    .AddDeveloperSigningCredential()
    .AddAspNetIdentity<AppUser>()
    // Sonra senin özel servisin (Sıralama önemli)
    .AddProfileService<CustomProfileService>();

//mikroservis koruma altına alınması
builder.Services.AddLocalApiAuthentication();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // Statik dosyalar (CSS/JS) için eklendi

app.UseRouting();
app.UseIdentityServer(); // IdentityServer middleware

app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"); // Varsayılan route eklendi

app.MapControllers();

app.Run();
