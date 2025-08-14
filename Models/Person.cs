using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LearninngManagementSystem.Models
{
    public abstract class Person
    {
       
        public string Name { get; set; }
        public string Surname { get; set; }

    }

    public class Student : Person
    {
        public int StudentNo { get; set; }
        public string Password { get; set; }
        public int grade {  get; set; }

        public Student() { }
    }
    public class Staff : Person
    {
        public int StaffNo { get; set; }
        public string Password { get; set; }
        public string Type { get; set; }
        public Staff() { }

        public string GetStaffType(string type) {
            type = Type;
            return type;
        }

    }
    

}