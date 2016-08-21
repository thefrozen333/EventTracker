using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EventTracker.Models
{
    public class Event
    {
        public int Id { get; set; }

        [Required]
        public ApplicationUser Host { get; set; }

        public DateTime DateTime { get; set; }

        [Required]
        [StringLength(255)]
        public string Venue { get; set; }

        [Required]
        public Category Category { get; set; }
    }
}