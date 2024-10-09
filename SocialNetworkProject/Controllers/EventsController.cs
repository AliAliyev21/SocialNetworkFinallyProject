using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using SocialNetworkProject.Data;
using SocialNetworkProject.Entities;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetworkProject.Controllers
{
    public class EventsController : Controller
    {
        private readonly SocialNetworkDbContext _context;

        public EventsController(SocialNetworkDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string searchString)
        {
            var events = from e in _context.Events
                         select e;

            if (!string.IsNullOrEmpty(searchString))
            {
                events = events.Where(e => e.EventName.StartsWith(searchString) || e.EventLocation.StartsWith(searchString));
            }

            ViewData["SearchString"] = searchString;

            return View(await events.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Create(Event newEvent, IFormFile eventImage)
        {
            if (ModelState.IsValid && eventImage != null && eventImage.Length > 0)
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", eventImage.FileName);

                var directory = Path.GetDirectoryName(filePath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await eventImage.CopyToAsync(stream);
                }

                newEvent.ImageUrl = "/images/" + eventImage.FileName;

                _context.Events.Add(newEvent);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(newEvent);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var eventToDelete = await _context.Events.FindAsync(id);
            if (eventToDelete != null)
            {
                _context.Events.Remove(eventToDelete);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
