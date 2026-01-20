using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LearninngManagementSystem.Models
{
    public class BookingIntakeVM
    {
        public int BookingId { get; set; }
        public string LearnerName { get; set; }
        public int Grade { get; set; }
        public string ParentName { get; set; }
        public string ParentPhone { get; set; }
        public string ParentEmail { get; set; }
        public bool? Passed { get; set; }

    }
}