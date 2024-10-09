using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SocialNetworkProject.Data;
using SocialNetworkProject.Entities;
using SocialNetworkProject.Hubs;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SocialNetworkProject.Controllers
{
    public class FriendsController : Controller
    {
        private readonly SocialNetworkDbContext _context;
        private readonly IHubContext<NotificationHub> _hubContext;

        public FriendsController(SocialNetworkDbContext context, IHubContext<NotificationHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        public async Task<IActionResult> Index(string searchQuery = null)
        {
            ViewBag.CurrentSearchQuery = searchQuery;

            var friendsQuery = _context.Friends.AsQueryable();

            if (!string.IsNullOrEmpty(searchQuery))
            {
                friendsQuery = friendsQuery.Where(f => f.FullName.Contains(searchQuery));
            }

            var friends = await friendsQuery
                .Where(f => f.Following)
                .Include(f => f.User)
                .ToListAsync();

            var peopleYouMayKnow = await _context.Friends
                .Where(f => !f.Following)
                .Include(f => f.User)
                .ToListAsync();

            ViewBag.Friends = friends;
            ViewBag.PeopleYouMayKnow = peopleYouMayKnow;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ToggleFollow(int id)
        {
            var friend = await _context.Friends.FindAsync(id);
            if (friend == null)
            {
                return NotFound();
            }

            friend.Following = !friend.Following;

            _context.Friends.Update(friend);
            await _context.SaveChangesAsync();

            if (friend.Following)
            {
                string message = $"{User.Identity.Name} started following you.";
                await _hubContext.Clients.User(friend.UserId.ToString()).SendAsync("ReceiveNotification", message);

                var notification = new Notification
                {
                    UserId = friend.UserId,
                    Content = message,
                    IsRead = false,
                    NotificationDate = DateTime.Now
                };

                _context.Notifications.Add(notification);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }
    }
}
