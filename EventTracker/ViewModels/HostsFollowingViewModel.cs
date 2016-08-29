using System.Collections.Generic;
using System.Linq;
using EventTracker.Models;

namespace EventTracker.ViewModels
{
    public class HostsFollowingViewModel
    {
        public IEnumerable<ApplicationUser> HostsFollowing { get; set; }
        public bool ShowActions { get; set; }
        public string Heading { get; set; }
    }
}