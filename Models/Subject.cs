using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LearninngManagementSystem.Models
{
    public class Subject
    {
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public string SubjectCode { get; set; }
        public int CreditHours { get; set; }
        public float MarksObtained { get; set; }
        public int Grade { get; set; }
        public string Description { get; set; }


    }
}