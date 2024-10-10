using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using ErrorOr;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SportStore.Application.Behaviors;
using SportStore.Application.Interfaces;
using SportStore.Application.Services;
using SportStore.Application.Users.Commands;
using SportStore.Domain;


namespace SportStore.Application;

    public static class ApplicationServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services){
            
            services.AddScoped<ITokenService, TokenService>();
            services.AddMediatR(option =>
            {
                option.AddBehavior<IPipelineBehavior<DeleteUserCommand,ErrorOr<User>>,ExampleBehavior>();
                option.RegisterServicesFromAssembly(typeof(ApplicationServices).Assembly);
            });

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }
        
    }