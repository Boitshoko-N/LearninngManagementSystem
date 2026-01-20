using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls.WebParts;

namespace LearninngManagementSystem.Models
{
    public abstract class Person
    {
       
        public string Name { get; set; }
        public string Surname { get; set; }

    }

    public class Parent : Person
    {
        public int ParentId { get; set; }

        public string ParentNumber { get; set; } // YYXXXXXX
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string ParentEmail { get; set; }
    }

    public class Student : Person
    {
        
        public string Password { get; set; }
        public int grade {  get; set; }

        public Student() { }

        public int StudentId { get; set; }

        public string StudentNo { get; set; } // YYXXXXXX
        public string FullName { get; set; }
        public string StudentEmail { get; set; }
        

        public bool IsRegistered { get; set; }
        public bool FirstLogin { get; set; } = true;

        public int ParentId { get; set; }
        public Parent Parent { get; set; }

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