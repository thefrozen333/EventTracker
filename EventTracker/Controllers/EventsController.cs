using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventTracker.Models;
using EventTracker.ViewModels;
using Microsoft.AspNet.Identity;

namespace EventTracker.Controllers
{
    public class EventsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public EventsController()
        {
            _context = new ApplicationDbContext();
        }

        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new EventFormViewModel
            {
                Categories = _context.Categories.ToList()
            };
            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EventFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Categories = _context.Categories.ToList();
                return View("Create", viewModel);
            }
            var anEvent = new Event
            {
                HostId = User.Identity.GetUserId(),
                DateTime = viewModel.GetDateTime(),
                CategoryId = viewModel.Category,
                Venue = viewModel.Venue
            };

            _context.Events.Add(anEvent);
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}