using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LearninngManagementSystem.Models
{
    public class StaffRepository
    {
        public static List<Staff>staff = new List<Staff>() {

            new Staff {Name = "Peter",Surname = "Smith",StaffNo = 98765432,Password = "Pass123",Type = "Clerk"},
            new Staff {Name = "Adam",Surname = "Smith",StaffNo = 2345678,Password = "Pass@1",Type = "Teacher"},
            new Staff {Name = "Asanda",Surname = "Sigigaba",StaffNo = 12345678,Password = "Pass234",Type = "Principal"}
        };

        public static Staff GetStaffByStaffNo(int staffNo)
        {
            return staff.FirstOrDefault(m => m.StaffNo == staffNo);
        }
        public static List<Staff> GetAllStaff()
        {
            return staff;
        }
    }
}