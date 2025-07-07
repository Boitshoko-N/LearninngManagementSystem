using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LearninngManagementSystem.Models
{
    public class Booking
    {
        public int BookingId { get; set; }
        public string P_FullName { get; set; }
        public string L_FullName {  get; set; }
        public int Grade {  get; set; }
        public string P_PhoneNumber { get; set; }
        public string L_PhoneNumber { get; set; }
        public DateTime BookingDate { get; set; }
        public DateTime DateMade { get; set; }
        public string Home_Language { get; set; }

        public Booking()
        {

        }

    }
}