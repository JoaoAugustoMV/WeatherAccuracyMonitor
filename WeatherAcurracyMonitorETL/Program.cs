using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WeatherAccuracyMonitorBackend.Domain.Repositories;
using WeatherAccuracyMonitorLib.Domain.Services.ForecastServices;
using WeatherAccuracyMonitorLib.Infra.AppDbContext.Repositories;
using WeatherAcurracyMonitorETL.Services;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();

        services.AddHttpClient();


        services.AddTransient<IForecastInfoDayRepository, ForecastInfoDayRepository>();

        services.AddTransient<ForecastServiceExecutor>();


        var assembly = typeof(WeatherForecastServiceBase).Assembly;

        foreach (var type in assembly.GetTypes()
            .Where(t => typeof(WeatherForecastServiceBase).IsAssignableFrom(t) && !t.IsAbstract))
        {
            services.AddTransient(typeof(WeatherForecastServiceBase), type);
        }

    }).ConfigureLogging(logging =>
    {
        //logging.AddConsole();
        logging.ClearProviders();       // remove Application Insights e outros providers
        logging.AddConsole();           // mantém apenas console
        logging.SetMinimumLevel(LogLevel.Information); // define nível mínimo
    })
    .Build();

host.Run();