using System.Linq;
using System.Web.Http;
using EventTracker.Models;
using Microsoft.AspNet.Identity;

namespace EventTracker.Controllers.Api
{
    [Authorize]
    public class AttendancesController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public AttendancesController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Attend([FromBody] int eventId)
        {
            var userId = User.Identity.GetUserId();

            if (_context.Attendances.Any(a => a.AttendeeId == userId && a.EventId == eventId))
                return BadRequest("Attendance already exists!");

            var attendance = new Attendance
            {
                EventId = eventId,
                AttendeeId = userId
            };
            _context.Attendances.Add(attendance);
            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult DeleteAttendance(int id)
        {
            var userId = User.Identity.GetUserId();

            var attendance = _context.Attendances
                .SingleOrDefault(a => a.AttendeeId == userId && a.EventId == id);

            if (attendance == null)
                return NotFound();

            _context.Attendances.Remove(attendance);
            _context.SaveChanges();

            return Ok(id);
        }
    }
}
