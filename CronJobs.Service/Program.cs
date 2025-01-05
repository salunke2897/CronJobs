using CronJobs.Service;
using CronJobs.Service.Common.Interface;
using CronJobs.Service.Common.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;

var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((context, config) =>
    {
        Console.WriteLine("Current environment: " + environment);
        config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        config.AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true); // For environment-specific configs
        config.AddEnvironmentVariables();
    })
    .ConfigureServices((hostContext, services) =>
    {
        var configuration = hostContext.Configuration;

        // Register the configuration instance for DI
        services.AddSingleton(configuration);

        // Register custom services
        services.AddSingleton<IConfigurationUtilityManager, ConfigurationUtilityManager>();
        services.AddHttpClient<IEndpointService, EndpointService>();
        services.AddQuartz(q =>
        {
            q.UseMicrosoftDependencyInjectionJobFactory();
        });
        services.AddQuartzHostedService(options =>
        {
            options.WaitForJobsToComplete = true;
        });
        services.AddSingleton<IJobScheduler,JobSchedulerService>();
    })
    .Build();
IJobScheduler jobScheduler = host.Services.GetRequiredService<IJobScheduler>();
await jobScheduler.ExecuteAsync();

await host.RunAsync();
