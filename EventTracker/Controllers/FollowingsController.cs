using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using EventTracker.Models;
using EventTracker.ViewModels;
using Microsoft.AspNet.Identity;

namespace EventTracker.Controllers
{
    [System.Web.Mvc.Authorize]
    public class FollowingsController : ApiController
    {
        private ApplicationDbContext _context;

        public FollowingsController()
        {
            _context = new ApplicationDbContext();
        }

        //[System.Web.Mvc.Authorize]
        //public ActionResult Following()
        //{
        //    var userId = User.Identity.GetUserId();
        //    var folowees = _context.Followings
        //        .Where(a => a.FolloweeId == userId)
        //        .Select(a => a.Followee)
        //        .ToList();

        //    var hostsFollowing = new List<Event>();

        //    foreach (var host in folowees)
        //    {
        //        hostsFollowing.AddRange(_context.Events
        //            .Where(e => e.HostId == host.Id)
        //            .Include(e => e.Host)
        //            .Include(e => e.Category));
        //    }

        //    var viewModel = new EventsViewModel()
        //    {
        //        UpcomingEvents = hostsFollowing,
        //        ShowActions = User.Identity.IsAuthenticated,
        //        Heading = "Hosts I am following"
        //    };

        //    return View("Events", viewModel);
        //}

        [System.Web.Mvc.HttpPost] public IHttpActionResult Follow ([FromBody]string followeeId)

        {
            var userId = User.Identity.GetUserId();

            if (_context.Followings.Any(f => f.FollowerId == userId && f.FolloweeId == followeeId))
                return BadRequest("Attendance already exists!");

            var following = new Following
            {
                FollowerId = userId,
                FolloweeId = followeeId
            };
            _context.Followings.Add(following);
            _context.SaveChanges();

            return Ok();
        }
    }
}
