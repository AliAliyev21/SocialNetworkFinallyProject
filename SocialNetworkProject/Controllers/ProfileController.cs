using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialNetworkProject.Data; 
using SocialNetworkProject.Entities;
using System.Linq;

namespace SocialNetworkProject.Controllers
{
    public class ProfileController : Controller
    {
        private readonly SocialNetworkDbContext _context;

        public ProfileController(SocialNetworkDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(int id)
        {
            var user = _context.Users.FirstOrDefault();

            if (user == null)
            {
                return NotFound("Kullanıcı bulunamadı");
            }

            return View(user);
        }
    }
}
