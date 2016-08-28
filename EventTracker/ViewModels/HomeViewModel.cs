using System.Collections.Generic;
using System.Linq;
using EventTracker.Models;

namespace EventTracker.ViewModels
{
    public class HomeViewModel
    {
        public IQueryable<Event> UpcomingEvents { get; set; }
        public bool ShowActions { get; set; }
    }
}