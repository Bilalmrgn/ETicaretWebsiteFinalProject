using IdentityServer.Application.Interfaces;
using IdentityServer.Domain;
using IdentityServer.Persistence.Concrete;
using IdentityServer.Persistence.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServer.Persistence.ServiceRegistration
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<IdentityDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DbConnection")));

            services.AddIdentity<AppUser, AppUserRole>(options =>
            {
                options.Password.RequireDigit = true;           // sayı
                options.Password.RequireLowercase = true;       // küçük harf
                options.Password.RequireUppercase = true;       // büyük harf
                options.Password.RequireNonAlphanumeric = true; // özel karakter
                options.Password.RequiredLength = 8;            // minimum uzunluk
                options.Password.RequiredUniqueChars = 1;       // farklı karakter sayısı

            }).AddEntityFrameworkStores<IdentityDbContext>().AddDefaultTokenProviders(); ;


            //IoC
            services.AddScoped<IUserService, UserService>();
            return services;
        }
    }
}
