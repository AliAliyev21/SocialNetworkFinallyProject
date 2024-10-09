using Microsoft.AspNetCore.Mvc;
using SocialNetworkProject.Entities;

namespace SocialNetworkProject.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Login model)
        {
            if (ModelState.IsValid)
            {
                if (IsValidUser(model.UsernameOrEmail, model.Password))
                {
                    return RedirectToAction("Index", "Profile");
                }
                ModelState.AddModelError(string.Empty, "Invalid login attempt."); 
            }
            return View(model);
        }

        private bool IsValidUser(string usernameOrEmail, string password)
        {
            return usernameOrEmail == "test@example.com" && password == "12345"; 
        }

    }
}
