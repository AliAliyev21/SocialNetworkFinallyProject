using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialNetworkProject.Data;
using SocialNetworkProject.Entities;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SocialNetworkProject.Controllers
{
    public class NotificationsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
