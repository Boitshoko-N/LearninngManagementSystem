using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LearninngManagementSystem.Models
{
    public class SlotDate
    {
        public int SlotDateId { get; set; }
        public DateTime Date { get; set; }

        public bool IsAvailable { get; set; }

        public int SlotId { get; set; }

        public List<Slot> Slots { get; set; }

    }
}