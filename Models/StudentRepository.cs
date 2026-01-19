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
                        StudentNo = reader["StudentNumber"].ToString(),
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
                        StudentNo = reader["StudentNumber"].ToString(),
                        Password = reader["StudentPassword"].ToString()
                    };
                }

                return null;
            }
        }

        public static Student GetById(int studentId)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                string sql = @"
                SELECT s.*, p.*
                FROM Student s
                INNER JOIN Parent p ON s.ParentId = p.ParentId
                WHERE s.StudentId = @StudentId";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@StudentId", studentId);

                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (!dr.Read())
                    return null;

                return new Student
                {
                    StudentId = (int)dr["StudentId"],
                    Name = dr["StudentName"].ToString(),
                    Surname = dr["StudentSurname"].ToString(),
                    StudentNo = dr["StudentNumber"].ToString(),
                    IsRegistered = (bool)dr["IsRegistered"],
                    Parent = new Parent
                    {
                        ParentId = (int)dr["ParentId"],
                        Phone = dr["Phone"].ToString(),
                        ParentNumber = dr["ParentNumber"].ToString()
                    }
                };
            }
        }

        // UPDATE STUDENT
        public static void Update(Student student)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                string sql = @"
                UPDATE Student
                SET StudentNumber = @StudentNumber,
                    IsRegistered = 1,
                    FirstLogin = 1
                WHERE StudentId = @StudentId;

                UPDATE Parent
                SET ParentNumber = @ParentNumber
                WHERE ParentId = @ParentId;";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@StudentNumber", student.StudentNo);
                cmd.Parameters.AddWithValue("@StudentId", student.StudentId);
                cmd.Parameters.AddWithValue("@ParentNumber", student.Parent.ParentNumber);
                cmd.Parameters.AddWithValue("@ParentId", student.Parent.ParentId);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }



    }
}