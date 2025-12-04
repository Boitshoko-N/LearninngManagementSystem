using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LearninngManagementSystem.Models
{
    public class BookingSlotVM
    {
        
        public List<SlotDate> SlotDateList { get; set; }
        public List<Slot> Slots { get; set; }

        [Required]
        public DateTime Date { get; set; }
        [Required]
        public DateTime StartTime { get; set; }
        [Required]
        public DateTime EndTime { get; set; }

        public void MakeSlot(int slotId)
        {
            var slot = StartTime + ":" + EndTime;
        }
        
        public bool IsAvailable { get; set; }

        public string BookingId { get; set; }
        [Required]
        public string P_FullName { get; set; }
        [Required]
        public string L_FullName { get; set; }
        [Required]
        public int Grade { get; set; }
        [Required]
        public string P_PhoneNumber { get; set; }
        [Required]
        public string L_PhoneNumber { get; set; }
        [Required]
        public DateTime DateMade { get; set; }
        [Required]
        public string Home_Language { get; set; }
    }
}