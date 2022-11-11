
namespace BrezyWeather.Data
{
    public class WeatherDbContext: DbContext
    {
        public WeatherDbContext(DbContextOptions<WeatherDbContext> options) : base(options)
        {
        }

        public DbSet<Weather>? Weather { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Weather>().HasData(
                new Weather { ID = 1, Time = DateTime.Now.AddHours(-1), Temperature = 28, Humidity = 78, AirQuality = "Fair" },
                new Weather { ID = 2, Time = DateTime.Now.AddHours(-2), Temperature = 27, Humidity = 84, AirQuality = "Healthy" },
                new Weather { ID = 3, Time = DateTime.Now.AddHours(-3), Temperature = 33, Humidity = 40, AirQuality = "Healthy" }
                );
        }
    }
}

