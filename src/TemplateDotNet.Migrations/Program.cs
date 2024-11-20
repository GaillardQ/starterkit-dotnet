using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Microsoft.Extensions.Logging;
using TemplateDotNet.Migrations;

using var host = Host.CreateDefaultBuilder(args)
    .ConfigureHostConfiguration(configHost =>
    {
        configHost.AddEnvironmentVariables();
    })
    .ConfigureServices((context, services) =>
    {
        // Add logger
        services.AddLogging(
            (loggingBuilder) => loggingBuilder
                .SetMinimumLevel(LogLevel.Debug)
                .AddConsole()
        )
        .BuildServiceProvider();

        //  adding Read db context enable DbContextOptions<TDbContext> options
        //  at design time, dotnet ef is only able to find connection string from appSetting.json
        //  at runtime, to perform migration, it is able to fing the connection string from launchSettings.json

		var credentials = context.Configuration.GetConnectionString("template_db");
        services
            .AddDbContext<ApiDbContext>(opt => opt
                .UseNpgsql(credentials));
    })
    .Build();

PerformDbMigrate();

void PerformDbMigrate()
{
    using var scope = host.Services.CreateScope();
    var logger = scope.ServiceProvider.GetRequiredService<ILoggerFactory>().CreateLogger<Program>();
    logger.LogInformation("EF Core Migration starting");
    //  inject, then use Data/DbContext will perform migrations
    var db = scope.ServiceProvider.GetRequiredService<ApiDbContext>();
    db.Database.Migrate();
    logger.LogInformation("EF Core Migration completed");
}