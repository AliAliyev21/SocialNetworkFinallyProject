using SocialNetworkProject.Entities;
using SocialNetworkProject.Models;

namespace SocialNetworkProject.Controllers
{
    internal class HomeViewModel
    {
        public User? User { get; set; }
        public WeatherResponse? Weather { get; set; }
        public List<Post>? Posts { get; set; }
    }
}