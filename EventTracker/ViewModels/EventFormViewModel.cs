using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using EventTracker.Models;

namespace EventTracker.ViewModels
{
    public class EventFormViewModel
    {
        [Required]
        public string Venue { get; set; }
        
        [Required]
        [FutureDate]
        public string Date { get; set; }

        [Required]
        [ValidTime]
        public string Time { get; set; }

        [Required]
        public byte Category { get; set; }

        public IEnumerable<Category> Categories{ get; set; }

        public DateTime GetDateTime()
        { 
             return DateTime.Parse($"{Date} {Time}");
        }
    }
}