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
           new Student {StudentNo = 12345678,Password = "Jack"},
           new Student {StudentNo = 23456789, Password = "Perro"}
        };
        public static Student GetStudentByStudentNo(int studentNo)
        {
            return Students.FirstOrDefault(m => m.StudentNo == studentNo);
        }
    }
}