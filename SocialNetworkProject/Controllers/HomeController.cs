using Microsoft.AspNetCore.Mvc;
using SocialNetworkProject.Models;
using System.Diagnostics;

namespace SocialNetworkProject.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
