namespace SocialNetworkProject.Models
{
    public class WeatherResponse
    {
        public Main? Main { get; set; }
        public string? Name { get; set; }
        public Weather[]? Weather { get; set; } // Array of weather conditions
    }

    public class Main
    {
        public float Temp { get; set; }
        public float Pressure { get; set; }
        public float Humidity { get; set; }
    }

    public class Weather
    {
        public string? Description { get; set; }
        public string? Icon { get; set; } // Icon code to fetch the weather icon
    }
}
