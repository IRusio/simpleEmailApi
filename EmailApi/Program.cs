using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmailApi.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EmailApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).ConfigureAppConfiguration(
                (hostContext, builder) =>
                {
                    builder.AddUserSecrets<Program>();
                }
                ).Build();
            using (var scope = host.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetService<EmailContext>();
                dbContext?.Database.Migrate();

                var env = scope.ServiceProvider.GetRequiredService<IWebHostEnvironment>();
                if (env.IsDevelopment())
                {
                    // Seed the database in development mode
                    var dbInitializer = scope.ServiceProvider.GetRequiredService<Db.IDbContextInitializer>();
                    dbInitializer.Seed().GetAwaiter().GetResult();
                }
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
