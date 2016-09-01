using System;
using System.Collections.Generic;
using System.Data.Entity;
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
        public ActionResult Mine()
        {
            var userId = User.Identity.GetUserId();
            var events = _context.Events
                .Where(e => e.HostId == userId && 
                e.DateTime > DateTime.Now && 
                 !e.IsCanceled)
                .Include(e => e.Category)
                .ToList();

            return View(events);
        }

        [Authorize]
        public ActionResult Attending()
        {
            var userId = User.Identity.GetUserId();
            var events = _context.Attendances
                .Where(a => a.AttendeeId == userId)
                .Select(a => a.Event)
                .Include(e => e.Host)
                .Include(e => e.Category)
                .ToList();

            var attendances = _context.Attendances
                .Where(a => a.AttendeeId == userId && a.Event.DateTime > DateTime.Now)
                .ToList()
                .ToLookup(a => a.EventId);

            var viewModel = new EventsViewModel()
            {
                UpcomingEvents = events,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Events I am attending",
                Attendances = attendances
            };

            return View("Events", viewModel);
        }

        [Authorize]
        public ActionResult Following()
        {
            var userId = User.Identity.GetUserId();
            var folowees = _context.Followings
                .Where(a => a.FollowerId == userId)
                .Select(a => a.Followee)
                .ToList();

            var viewModel = new HostsFollowingViewModel()
            {
                HostsFollowing = folowees,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Hosts I am following"
            };

            return View("HostsFollowing", viewModel);
        }

        public ActionResult Details(int id)
        {
            var anEvent = _context.Events
                            .Include(e => e.Host)
                            .Include(e => e.Category)
                            .SingleOrDefault(e => e.Id == id);

            if (anEvent == null)
                return HttpNotFound();

            var viewModel = new EventDetailsViewModel()
            {
                Event = anEvent
            };

            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();

                viewModel.IsAttending = _context.Attendances
                    .Any(a => a.EventId == anEvent.Id && a.AttendeeId == userId);

                viewModel.IsFollowing =
                    _context.Followings
                    .Any(f => f.FolloweeId == anEvent.HostId && f.FollowerId == userId);
            }

            return View("Details", viewModel);
        }

        [HttpPost]
        public ActionResult Search(EventsViewModel viewModel)
        {
            return RedirectToAction("Index", "Home", new {query = viewModel.SearchTerm});
        }

        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new EventFormViewModel
            {
                Categories = _context.Categories.ToList(),
                Heading = "Add an Event"
            };
            return View("EventForm", viewModel);
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            var userId = User.Identity.GetUserId();
            var anEvent = _context.Events.Single(e => e.Id == id && e.HostId == userId);

            var viewModel = new EventFormViewModel
            {
                
                Heading = "Edit an Event",
                Id = anEvent.Id,
                Categories = _context.Categories.ToList(),
                Date = anEvent.DateTime.ToString("d MMM yyyy"),
                Time = anEvent.DateTime.ToString("HH:mm"),
                Category = anEvent.CategoryId,
                Venue = anEvent.Venue
                
            };
            return View("EventForm", viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EventFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Categories = _context.Categories.ToList();
                return View("EventForm", viewModel);
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
            return RedirectToAction("Mine", "Events");
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(EventFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Categories = _context.Categories.ToList();
                return View("EventForm", viewModel);
            }

            var userId = User.Identity.GetUserId();
            var anEvent = _context.Events.Single(e => e.Id == viewModel.Id && e.HostId == userId );
            anEvent.Venue = viewModel.Venue;
            anEvent.DateTime = viewModel.GetDateTime();
            anEvent.CategoryId = viewModel.Category;

            _context.SaveChanges();
            return RedirectToAction("Mine", "Events");
        }
    }
}