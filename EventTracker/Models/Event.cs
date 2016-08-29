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

        public bool IsCanceled { get; set; }

        public ApplicationUser Host { get; set; }

        [Required]
        public string HostId { get; set; }

        public DateTime DateTime { get; set; }

        [Required]
        [StringLength(255)]
        public string Venue { get; set; }

        public Category Category { get; set; }

        [Required]
        public byte CategoryId { get; set; }
    }
}