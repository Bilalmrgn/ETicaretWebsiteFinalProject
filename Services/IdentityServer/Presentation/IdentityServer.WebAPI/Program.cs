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
    .AddIdentityServer()
    .AddInMemoryApiResources(Config.ApiSResources)
    .AddInMemoryApiScopes(Config.ApiScopes)
    .AddInMemoryIdentityResources(Config.IdentityResources)
    .AddInMemoryClients(Config.Clients)
    .AddDeveloperSigningCredential();

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

app.UseAuthorization();

app.MapControllers();

app.Run();
