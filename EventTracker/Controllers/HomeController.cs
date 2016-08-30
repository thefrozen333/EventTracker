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
        public ActionResult Index()
        {
            var upcomingEvents = _context.Events
                                    .Include(e => e.Host)
                                    .Include(e => e.Category)
                                    .Where(e => e.DateTime > DateTime.Now && !e.IsCanceled);

            var viewModel = new EventsViewModel
            {
                UpcomingEvents = upcomingEvents,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Upcoming Events"
            };

            return View("Events", viewModel);
        }
    }
}