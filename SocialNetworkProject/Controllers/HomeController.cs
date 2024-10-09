using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.IO; // Fayl işləmə üçün
using SocialNetworkProject.Entities;
using SocialNetworkProject.Models;
using SocialNetworkProject.Data;
using Microsoft.EntityFrameworkCore;

namespace SocialNetworkProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly SocialNetworkDbContext _context;

        public HomeController(HttpClient httpClient, SocialNetworkDbContext context)
        {
            _httpClient = httpClient;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _context.Users.FirstOrDefaultAsync();

            if (user == null)
            {
                return NotFound("Kullanıcı bulunamadı");
            }

            var weatherApiKey = "04fafbc28865d436a2690c86be70f0bc";
            var requestUri = $"https://api.openweathermap.org/data/2.5/weather?q=Baku&units=metric&appid={weatherApiKey}";

            var response = await _httpClient.GetAsync(requestUri);
            WeatherResponse weatherData = null;

            if (response.IsSuccessStatusCode)
            {
                weatherData = await response.Content.ReadFromJsonAsync<WeatherResponse>();
            }

            var posts = await _context.Posts.ToListAsync();

            var viewModel = new HomeViewModel
            {
                User = user,
                Weather = weatherData,
                Posts = posts
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost(string message, IFormFile image, IFormFile video)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                ModelState.AddModelError("", "Message is required.");
                return RedirectToAction("Index");
            }

            var user = await _context.Users.FirstOrDefaultAsync();

            if (user == null)
            {
                ModelState.AddModelError("", "User not found.");
                return RedirectToAction("Index");
            }

            var post = new Post
            {
                Message = message,
                ImagePath = image != null ? await SaveFileAsync(image) : null,
                VideoPath = video != null ? await SaveFileAsync(video) : null,
                CreatedAt = DateTime.Now,
                UserId = user.Id
            };

            _context.Posts.Add(post);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        private async Task<string> SaveFileAsync(IFormFile file)
        {
            var filePath = Path.Combine("wwwroot/uploads", file.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return "/uploads/" + file.FileName;
        }

        public async Task<IActionResult> HidePost(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post != null)
            {
                // Logic to hide the post, e.g., set a hidden flag
                post.IsHidden = true; // Assuming IsHidden is a property in your Post model
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeletePost(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post != null)
            {
                _context.Posts.Remove(post);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> EditPost(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            // Return a view to edit the post
            return View(post); // Assuming you have a view for editing
        }

        [HttpPost]
        public async Task<IActionResult> EditPost(int id, string message, IFormFile image, IFormFile video)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            post.Message = message;
            post.ImagePath = image != null ? await SaveFileAsync(image) : post.ImagePath;
            post.VideoPath = video != null ? await SaveFileAsync(video) : post.VideoPath;

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
