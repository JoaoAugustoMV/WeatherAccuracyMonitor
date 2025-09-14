using System.Threading;
using System.Threading.RateLimiting;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WeatherAccuracyMonitorBackend.Domain.Repositories;
using WeatherAccuracyMonitorBackend.Infra.AppDbContext.Repositories;
using WeatherAccuracyMonitorBackend.Services;
using WeatherAccuracyMonitorLib.Domain.Services;
using WeatherAccuracyMonitorLib.Domain.Services.ForecastServices;
using WeatherAccuracyMonitorLib.Infra.AppDbContext;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddRateLimiter(options => 
    {
        options.OnRejected = async (context, cancellationToken) =>
        {
            context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
            context.HttpContext.Response.Headers.RetryAfter = "60";

            await context.HttpContext.Response.WriteAsync("Rate limit exceeded. Please try again later.", cancellationToken);
        };
        options.AddTokenBucketLimiter(policyName: "tokenPolicy", options =>
        {
            options.TokenLimit = 20;
            options.QueueLimit = 0;
            options.ReplenishmentPeriod = TimeSpan.FromSeconds(10);
            options.TokensPerPeriod = 10;
        });
    }
);

builder.Services.AddCors(options => options.AddPolicy("Front", c => 
                c.WithOrigins("http://localhost:4200")
                .AllowAnyMethod()
                .AllowAnyHeader()));

builder.Services.AddDbContextPool<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddScoped<IForecastInfoDayRepository, ForecastInfoDayRepository>();

builder.Services.AddScoped<IForecastInfoDayService, ForecastInfoDayService>();
builder.Services.AddScoped<IInfoDataService, InfoDataService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRateLimiter();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors("Front");
app.Run();
