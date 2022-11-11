
namespace BrezyWeather.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherController : ControllerBase
{
    private readonly ILogger<WeatherController> _logger;
    private readonly WeatherDbContext weatherContext;

    public WeatherController(ILogger<WeatherController> logger, WeatherDbContext weatherContext)
    {
        _logger = logger;
        weatherContext.Database.EnsureCreated();
        this.weatherContext = weatherContext;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Weather>> GetWeather()
    {
        var weather = weatherContext.Weather;

        if (weather == null)
        {
            return new List<Weather>();
        }

        return weather.ToList();
    }

    [HttpPost]
    public ActionResult<Weather> AddWeather(Weather weather)
    {
        if (weather == null)
        {
            return BadRequest();
        }

        if(weatherContext.Weather == null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Weather database not initialized");
        }

        weatherContext.Weather.Add(weather);
        weatherContext.SaveChanges();

        return Ok(weather);
    }

    [HttpGet("{id}")]
    public ActionResult<Weather> GetWeather(int id)
    {
        if (weatherContext.Weather == null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Weather database not initialized");
        }

        var weather = weatherContext.Weather.FirstOrDefault(x => x.ID == id);
        if (weather == null)
        {
            return NotFound();
        }

        return weather;
    }

    [HttpPut("{id}")]
    public ActionResult<Weather> UpdateWeather(int id, Weather weather)
    {
        if (weatherContext.Weather == null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Weather database not initialized");
        }

        var currentWeather = weatherContext.Weather.FirstOrDefault(x => x.ID == id);
        if (currentWeather == null)
        {
            return NotFound();
        }

        currentWeather.Temperature = weather.Temperature;
        currentWeather.Humidity = weather.Humidity;
        currentWeather.AirQuality = weather.AirQuality;
        weatherContext.SaveChanges();

        return weather;
    }

    [HttpDelete("{id}")]
    public ActionResult<Weather> DeleteWeather(int id)
    {
        if (weatherContext.Weather == null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Weather database not initialized");
        }

        var currentWeather = weatherContext.Weather.FirstOrDefault(x => x.ID == id);
        if (currentWeather == null)
        {
            return NotFound();
        }

        weatherContext.Weather.Remove(currentWeather);
        weatherContext.SaveChanges();

        return currentWeather;
    }

}

