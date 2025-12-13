
using Microsoft.EntityFrameworkCore;
using WeatherAccuracyMonitor.Domain.Entities;

namespace WeatherAccuracyMonitor.Data
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
