using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EventTracker.Models;

namespace EventTracker.ViewModels
{
    public class EventFormViewModel
    {
        public string Venue { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public int Category { get; set; }
        public IEnumerable<Category> Categories{ get; set; }
    }
}