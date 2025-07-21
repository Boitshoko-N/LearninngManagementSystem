using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LearninngManagementSystem.Models
{
    public class StaffRepository
    {
        public static List<Staff>staff = new List<Staff>() {

            new Staff {StaffNo = 98765432,Password = "Pass123",Type = "Teacher"},
            new Staff {StaffNo = 12345678,Password = "Pass234",Type = "Principal"}
        };

        public static Staff GetStaffByStaffNo(int staffNo)
        {
            return staff.FirstOrDefault(m => m.StaffNo == staffNo);
        }
    }
}