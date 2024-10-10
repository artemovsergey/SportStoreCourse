using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace SportStore.API
{
    public static class PresentationServices
    {
        public static IServiceCollection AddPresentationServices(this IServiceCollection services, IConfiguration configuration){
            
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                            options.TokenValidationParameters = new TokenValidationParameters{
                                ValidateIssuerSigningKey = true,
                                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["TokenKey"]!)),
                                ValidateIssuer = false,
                                ValidateAudience = false,   
                }
                );

            services.AddControllers();
            services.AddEndpointsApiExplorer();
            
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                     Version="v1",
                     Title="SportStore API",
                });

                //c.OperationFilter<FileResultContentTypeOperationFilter>();
            }
            );
            
            return services;
        }
        
    }
}