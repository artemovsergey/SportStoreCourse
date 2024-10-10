using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SportStore.Application.Interfaces;
using SportStore.Application.Models;
using SportStore.Domain;
using SportStore.Infrastructure.Data;
using SportStore.Infrastructure.Repositories;
using SportStore.Infrastructure.Services;

namespace SportStore.Infrastructure;

    public static class InfrastructreServices
    {
        public static IServiceCollection AddInfrastructreServices(this IServiceCollection services,
                                                                  IConfiguration configuration){
            
            services.AddScoped<IUnitOfWork,UnitOfWork>();
            services.AddScoped<IUserRepository,UserRepository>();

            services.AddDbContext<SportStoreContext>(
                options =>
                options.UseNpgsql(configuration.GetConnectionString("Postgres")));

            services.Configure<EmailSettings>(c => configuration.GetSection("EmailSettings"));
            services.AddTransient<IEmailService,EmailService>();
            services.AddTransient<ICsvExporter,ExportToCsvService>();

            return services;
        }
        
    }