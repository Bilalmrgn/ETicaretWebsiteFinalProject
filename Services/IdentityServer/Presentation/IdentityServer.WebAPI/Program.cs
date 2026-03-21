using IdentityServer.Domain;
using IdentityServer.Persistence.Concrete;
using IdentityServer.Persistence.ServiceRegistration;
using IdentityServer.WebAPI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//persistence service registration
builder.Services.AddDatabase(builder.Configuration);

//IdentityServer,Controller
builder.Services
    .AddIdentityServer()//Token almamýzý saðlayan kýsým connect/token endpoint inden token üretir
    .AddInMemoryApiResources(Config.ApiResources)
    .AddInMemoryApiScopes(Config.ApiScopes)
    .AddInMemoryIdentityResources(Config.IdentityResources)
    .AddInMemoryClients(Config.Clients)
    .AddDeveloperSigningCredential()
    .AddProfileService<CustomProfileService>();

//mikroservis koruma altýna alýnmasý
builder.Services.AddLocalApiAuthentication();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();
app.UseIdentityServer();//token i almamý saðlayan kýsým

app.UseAuthentication();//mikroservisin koruma altýna alýnmasý
app.UseAuthorization();


app.MapControllers();

app.Run();
