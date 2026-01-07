using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LearninngManagementSystem.Models
{
    public class BookingConfirmationVM
    {
        public string BookingNo { get; set; }
        public string BookingId { get; set; }
        public DateTime DateMade { get; set; }

        public DateTime SlotDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public string P_FullName { get; set; }
        public string L_FullName { get; set; }
        public string P_PhoneNumber { get; set; }
        public string L_PhoneNumber { get; set; }

        public string Home_Language { get; set; }
        public int Grade { get; set; }
    }
}