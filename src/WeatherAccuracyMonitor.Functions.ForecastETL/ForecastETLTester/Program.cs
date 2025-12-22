// See https://aka.ms/new-console-template for more information
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WeatherAccuracyMonitor.Providers.ForecastSources;

Console.WriteLine("Hello, World!");
var serviceCollection = new ServiceCollection();

serviceCollection.AddLogging();
serviceCollection.AddHttpClient();


var configurationBuilder = new ConfigurationBuilder()    
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

serviceCollection.AddSingleton<IConfiguration>(configurationBuilder.Build());

configurationBuilder.Build();

serviceCollection.AddTransient<HGBrasilForecastService>();
serviceCollection.AddTransient<AdvisorForecastService>();

IServiceProvider ret = serviceCollection.BuildServiceProvider();

List<Task> tasks = new List<Task>
            {
                ret.GetService<AdvisorForecastService>()!.ExecuteTemperaturesPredictions(),
                ret.GetService<HGBrasilForecastService>()!.ExecuteTemperaturesPredictions()
            };
    
    Console.WriteLine("Só quero testar");

try
{
    await Task.WhenAll(tasks);
}
catch (Exception ex)
{
    Console.WriteLine("Error executing forecasts ETL: " + ex.Message);
}
