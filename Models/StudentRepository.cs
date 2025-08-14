using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LearninngManagementSystem.Models
{
    public class StudentRepository
    {
        public static List<Student> Students = new List<Student>()
        {
           new Student {Name = "Jack",Surname = "Perrow",StudentNo = 12345678,Password = "Jack",grade = 11},
           new Student {Name = "Jeke",Surname = "Maarn",StudentNo = 23456789, Password = "Perro",grade = 12}
        };
        public static Student GetStudentByStudentNo(int studentNo)
        {
            return Students.FirstOrDefault(m => m.StudentNo == studentNo);
        }
        public static List<Student> GetAllStudents() 
        {
            return Students;
        }
    }
}