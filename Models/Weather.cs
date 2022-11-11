using System.ComponentModel.DataAnnotations;

namespace BrezyWeather.Models;

public class Weather
{
    public int  ID { get; set; }

    public DateTime Time { get; set; }

    public float Temperature { get; set; }

    public float Humidity { get; set; }

    public string? AirQuality { get; set; }
}

