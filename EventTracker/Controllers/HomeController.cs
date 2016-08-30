using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using EventTracker.Models;
using EventTracker.ViewModels;

namespace EventTracker.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController()
        {
            _context = new ApplicationDbContext();
        }
        public ActionResult Index(string query = null)
        {
            var upcomingEvents = _context.Events
                                    .Include(e => e.Host)
                                    .Include(e => e.Category)
                                    .Where(e => e.DateTime > DateTime.Now && !e.IsCanceled);

            if (!String.IsNullOrWhiteSpace(query))
            {
                upcomingEvents = upcomingEvents
                    .Where(e =>
                        e.Host.Name.Contains(query) ||
                        e.Category.Name.Contains(query) ||
                        e.Venue.Contains(query));
            }

            var viewModel = new EventsViewModel
            {
                UpcomingEvents = upcomingEvents,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Upcoming Events",
                SearchTerm = query
            };

            return View("Events", viewModel);
        }
    }
}