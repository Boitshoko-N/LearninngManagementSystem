using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LearninngManagementSystem.Models
{
    public class Slot
    {
        public int SlotId { get; set; }

        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public bool IsAvailable { get; set; }

        public int SlotDateId { get; set; }

        public int maxBookings { get; set; }

        
    }
}