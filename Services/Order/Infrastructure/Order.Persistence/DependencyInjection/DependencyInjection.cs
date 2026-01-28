using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Order.Application.Interfaces;
using Order.Persistence.Concrete;
using Order.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Persistence.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<OrderDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DbConnection")));

            //Repository IoC container
            services.AddScoped<IOrderingRepository, OrderingRepository>();
            services.AddScoped<IAdressRepository, AddressRepository>();

            return services;
        }

        
    }
}
