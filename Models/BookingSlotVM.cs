using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LearninngManagementSystem.Models
{
    public class BookingSlotVM
    {
        
        public List<SlotDate> SlotDateList { get; set; }
        public List<Slot> Slots { get; set; }

        public DateTime Date { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public bool IsAvailable { get; set; }

        public string BookingId { get; set; }
        public string P_FullName { get; set; }
        public string L_FullName { get; set; }
        public int Grade { get; set; }
        public string P_PhoneNumber { get; set; }
        public string L_PhoneNumber { get; set; }
        public DateTime DateMade { get; set; }
        public string Home_Language { get; set; }
    }
}