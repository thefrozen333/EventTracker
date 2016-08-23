using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventTracker.Models;
using EventTracker.ViewModels;

namespace EventTracker.Controllers
{
    public class EventsController : Controller
    {
        private ApplicationDbContext _context;
        public EventsController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: Events
        public ActionResult Create()
        {
            var viewModel = new EventFormViewModel
            {
                Categories = _context.Categories.ToList()
            };
            return View(viewModel);
        }
    }
}