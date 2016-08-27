using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using EventTracker.Models;

namespace EventTracker.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _context;

        public HomeController()
        {
            _context = new ApplicationDbContext();
        }
        public ActionResult Index()
        {
            var upcomingEvents = _context.Events
                                    .Include(e => e.Host)
                                    .Where(e => e.DateTime > DateTime.Now);
            return View(upcomingEvents);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}