using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SportStore.API.Data;

namespace SportStore.API.Extensions
{
    public static class ApiExtension
    {
        
            public static async Task ResetDatabaseAsync(this WebApplication app){
                using var scope = app.Services.CreateScope();
                try
                {
                    var context = scope.ServiceProvider.GetService<SportStoreContext>();
                    if(context != null){
                        await context.Database.EnsureDeletedAsync();
                        await context.Database.EnsureCreatedAsync();
                    }
                }
                catch(Exception ex)
                {

                }
            }

    }
}