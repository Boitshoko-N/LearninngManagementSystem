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

        public class RegistrationEmails
        {
            public string ParentEmail { get; set; }
            public string StudentEmail { get; set; }
        }

        public static RegistrationEmails CreateStudentFromBooking(
            int bookingId,
            int studentNo,
            int parentNo,
            string studentPassword,
            string parentPassword)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                SqlTransaction tx = conn.BeginTransaction();

                try
                {
                    // 1️⃣ Get booking info
                    string bookingSql = @"
                SELECT 
                    L_FullName,
                    L_Email,
                    Grade,
                    P_FullName,
                    P_PhoneNumber,
                    P_Email
                FROM Booking
                WHERE BookingId = @BookingId";

                    SqlCommand bookingCmd = new SqlCommand(bookingSql, conn, tx);
                    bookingCmd.Parameters.AddWithValue("@BookingId", bookingId);

                    SqlDataReader dr = bookingCmd.ExecuteReader();

                    if (!dr.Read())
                        throw new Exception("Booking not found");

                    string learnerName = dr["L_FullName"].ToString();
                    string studentEmail = dr["L_Email"].ToString();
                    int grade = (int)dr["Grade"];

                    string parentName = dr["P_FullName"].ToString();
                    string parentPhone = dr["P_PhoneNumber"].ToString();
                    string parentEmail = dr["P_Email"].ToString();

                    dr.Close();

                    // 2️⃣ Split names safely
                    string[] learnerParts = learnerName.Split(' ');
                    string studentName = learnerParts[0];
                    string studentSurname = learnerParts.Length > 1 ? learnerParts[1] : "";

                    string[] parentParts = parentName.Split(' ');
                    string pName = parentParts[0];
                    string pSurname = parentParts.Length > 1 ? parentParts[1] : "";

                    // 3️⃣ Insert Parent
                    string parentSql = @"
                INSERT INTO Parent
                (ParentNumber, ParentName, ParentSurname, ParentPassword, ParentPhone, ParentEmail)
                OUTPUT INSERTED.ParentId
                VALUES
                (@ParentNumber, @ParentName, @ParentSurname, @ParentPassword, @ParentPhone, @ParentEmail)";

                    SqlCommand parentCmd = new SqlCommand(parentSql, conn, tx);
                    parentCmd.Parameters.AddWithValue("@ParentNumber", parentNo);
                    parentCmd.Parameters.AddWithValue("@ParentName", pName);
                    parentCmd.Parameters.AddWithValue("@ParentSurname", pSurname);
                    parentCmd.Parameters.AddWithValue("@ParentPassword", parentPassword);
                    parentCmd.Parameters.AddWithValue("@ParentPhone", parentPhone);
                    parentCmd.Parameters.AddWithValue("@ParentEmail", parentEmail);

                    int parentId = (int)parentCmd.ExecuteScalar();

                    // 4️⃣ Insert Student
                    string studentSql = @"
                INSERT INTO Student
                (StudentNumber, StudentName, StudentSurname, StudentPassword, StudentEmail, Grade, ParentId)
                VALUES
                (@StudentNumber, @StudentName, @StudentSurname, @StudentPassword, @StudentEmail, @Grade, @ParentId)";

                    SqlCommand studentCmd = new SqlCommand(studentSql, conn, tx);
                    studentCmd.Parameters.AddWithValue("@StudentNumber", studentNo);
                    studentCmd.Parameters.AddWithValue("@StudentName", studentName);
                    studentCmd.Parameters.AddWithValue("@StudentSurname", studentSurname);
                    studentCmd.Parameters.AddWithValue("@StudentPassword", studentPassword);
                    studentCmd.Parameters.AddWithValue("@StudentEmail", studentEmail);
                    studentCmd.Parameters.AddWithValue("@Grade", grade);
                    studentCmd.Parameters.AddWithValue("@ParentId", parentId);

                    studentCmd.ExecuteNonQuery();

                    // 5️⃣ Update Booking IsRegistered flag
                    string updateBookingSql = @"
                UPDATE Booking
                SET IsRegistered = 1
                WHERE BookingId = @BookingId";

                    SqlCommand updateCmd = new SqlCommand(updateBookingSql, conn, tx);
                    updateCmd.Parameters.AddWithValue("@BookingId", bookingId);
                    updateCmd.ExecuteNonQuery();

                    // 6️⃣ Commit transaction
                    tx.Commit();

                    // Return emails for sending notifications
                    return new RegistrationEmails
                    {
                        ParentEmail = parentEmail,
                        StudentEmail = studentEmail
                    };
                }
                catch
                {
                    tx.Rollback();
                    throw;
                }
            }
        }



        public static int GetStudentCount()
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Student", conn);
                conn.Open();
                return (int)cmd.ExecuteScalar();
            }
        }


    }
}