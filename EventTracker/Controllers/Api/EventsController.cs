using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EventTracker.Models;
using Microsoft.AspNet.Identity;

namespace EventTracker.Controllers.Api
{
    [Authorize]
    public class EventsController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public EventsController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpDelete]
        public IHttpActionResult Cancel(int id)
        {
            var userId = User.Identity.GetUserId();
            var anEvent = _context.Events.Single(e => e.Id == id && e.HostId == userId);

            if (anEvent.IsCanceled)
                return NotFound();

            anEvent.IsCanceled = true;
            _context.SaveChanges();

            return Ok();
        }
    }
}
