using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WeatherAccuracyMonitor.Providers.ForecastSources;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace HelloWorld;

public class Function
{

    private readonly IHost _host;
    private readonly List<Task> tasks;
    public Function()
    {
        _host = Host.CreateDefaultBuilder().ConfigureServices(b =>
        {
            b.AddTransient<HGBrasilForecastService>();
            b.AddTransient<AdvisorForecastService>();
            

            var builder = new ConfigurationBuilder().AddEnvironmentVariables();
            IConfiguration configuration = builder.Build();
            b.AddSingleton<IConfiguration>(configuration);
            
            b.AddHttpClient();
            b.AddLogging();
        }).Build();

        

        tasks = new List<Task>
            {
                _host.Services.GetService<AdvisorForecastService>()!.ExecuteTemperaturesPredictions(),
                _host.Services.GetService<HGBrasilForecastService>()!.ExecuteTemperaturesPredictions()
            };
    }

    public async Task FunctionHandler(ILambdaContext context)
    {
        context.Logger.LogLine("Hello World from AWS Lambda!");
        Console.WriteLine("Só quero testar");

        try
        {
            await Task.WhenAll(tasks);
        }
        catch (Exception ex)
        {
            context.Logger.LogError("Error executing forecasts ETL: " + ex.Message);
        }
    }
}