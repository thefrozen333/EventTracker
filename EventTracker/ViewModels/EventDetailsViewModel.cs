using EventTracker.Models;

namespace EventTracker.ViewModels
{
    public class EventDetailsViewModel
    {
        public Event Event { get; set; }
        public bool IsAttending { get; set; }
        public bool IsFollowing { get; set; }
    }
}