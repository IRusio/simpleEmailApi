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
using Serilog;
using Serilog.Formatting.Compact;

namespace EmailApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File(new RenderedCompactJsonFormatter(), "/logs/log.json")
                .CreateLogger();
        
            try
            {
                Log.Information("Starting up");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application start-up failed");
            }
            finally
            {
                Log.CloseAndFlush();
            }
            
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
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
