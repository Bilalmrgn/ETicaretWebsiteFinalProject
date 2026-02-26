using Cargo.Application;
using Cargo.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cargo.Persistence
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CargoAppDbcontext>(options => options.UseSqlServer(configuration.GetConnectionString("DbConnection")));

            //IoC conteiner
            //CargoOpration
            services.AddScoped<ICargoOperationReadRepository, CargoOperationReadRepository>();
            services.AddScoped<ICargoOperationWriteRepository, CargoOperationWriteRepository>();

            //CargoCompany
            services.AddScoped<ICargoCompanyReadRepository, CargoCompanyReadRepository>();
            services.AddScoped<ICargoCompanyWriteRepository, CargoCompanyWriteRepository>();

            //CargoCustomer
            services.AddScoped<ICargoCustomerReadRepository, CargoCustomerReadRepository>();
            services.AddScoped<ICargoCustomerWriteRepository, CargoCustomerWriteRepository>();

            //CargoDetail
            services.AddScoped<ICargoDetailReadRepository, CargoDetailReadRepository>();
            services.AddScoped<ICargoDetailWriteRepository, CargoDetailWriteRepository>();

            return services;
        }
    }
}
