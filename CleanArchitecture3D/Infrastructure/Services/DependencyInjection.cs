using Application;
using Application.Data;
using Domain.Abstractions;
using Domain.Customers;
using Domain.Primitives;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddPersistence(configuration);
            return services;
        }

        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("cadenaSQL")));

            services.AddScoped<IApplicationDbContext>(sp => 
                sp.GetRequiredService<ApplicationDbContext>());

            services.AddScoped<IUnitOfWork>(sp => 
                sp.GetRequiredService<ApplicationDbContext>());

            services.AddScoped<ICustomerRepository,CustomerRepository>();
            return services;
        }
    }
}
