using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using SocialNetworkProject.Models; // Add this line

namespace SocialNetworkProject.Controllers
{
    public class WeatherController : Controller
    {
        private readonly HttpClient _httpClient;

        public WeatherController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetWeather(string country, int forecastDays, string temperatureUnit)
        {
            var apiKey = "04fafbc28865d436a2690c86be70f0bc"; // Replace with your actual API key
            var requestUri = $"https://api.openweathermap.org/data/2.5/weather?q={country}&units={temperatureUnit}&appid={apiKey}";

            var response = await _httpClient.GetAsync(requestUri);

            if (response.IsSuccessStatusCode)
            {
                var weatherData = await response.Content.ReadFromJsonAsync<WeatherResponse>();
                return View("Index", weatherData);
            }

            ModelState.AddModelError("", "Unable to retrieve weather data.");
            return View("Index");
        }
    }
}
