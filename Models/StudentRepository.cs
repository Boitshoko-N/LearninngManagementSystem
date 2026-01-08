using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;

namespace LearninngManagementSystem.Models
{
    public class StudentRepository
    {
        public static string ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        public static List<Student> GetStudents() 
        {
            List<Student> Students = new List<Student>();

            using(SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string query = "SELECT * FROM Student";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Student student = new Student()
                    {
                        StudentNo = Convert.ToInt32(reader["StudentNumber"]),
                        Name = reader["StudentName"].ToString(),
                        Surname = reader["StudentSurname"].ToString(),
                        Password = reader["StudentPassword"].ToString(),
                        grade = Convert.ToInt32(reader["Grade"])
                    };
                    Students.Add(student);
                }
                reader.Close();
            }
            return Students;

        }

        public static Student GetStudentByStudentNo(int studentNo)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string query = @"SELECT StudentNumber, StudentPassword
                         FROM Student
                         WHERE StudentNumber = @StudentNumber";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@StudentNumber", studentNo);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return new Student
                    {
                        StudentNo = Convert.ToInt32(reader["StudentNumber"]),
                        Password = reader["StudentPassword"].ToString()
                    };
                }

                return null;
            }
        }



    }
}