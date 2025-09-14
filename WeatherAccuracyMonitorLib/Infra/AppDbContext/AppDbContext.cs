using Microsoft.EntityFrameworkCore;
using WeatherAccuracyMonitorBackend.Domain.Entities;

namespace WeatherAccuracyMonitorLib.Infra.AppDbContext
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<ForecastInfoDay> ForecastInfoDays { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ForecastInfoDay>()
                .ToTable("forecast_info_day")
                .Property(e => e.ForecastDate)
                .HasColumnType("date");

        }
    }
}
