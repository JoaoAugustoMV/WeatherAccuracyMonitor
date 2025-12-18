using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;

using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace HelloWorld;

public class Function
{

    private static readonly HttpClient client = new HttpClient();
    
    public async Task FunctionHandler(ILambdaContext context)
    {
        context.Logger.LogLine("Hello World from AWS Lambda!");
        Console.WriteLine("Só quero testar");
    }
}