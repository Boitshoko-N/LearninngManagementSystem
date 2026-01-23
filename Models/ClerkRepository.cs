using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace LearninngManagementSystem.Models
{
    public class ClerkRepository
    {
        static string cs =
            ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        // BOOKINGS THAT NEED MARKS
        public static List<BookingIntakeVM> GetBookingsForIntake()
        {
            var list = new List<BookingIntakeVM>();

            using (SqlConnection conn = new SqlConnection(cs))
            {
                string sql = @"
                SELECT b.BookingId,
                       b.L_FullName,
                       b.Grade,
                       b.P_FullName,
                       b.P_PhoneNumber,
                       b.P_Email
                FROM Booking b
                WHERE NOT EXISTS
                (
                    SELECT 1 FROM IntakeResult i WHERE i.BookingId = b.BookingId
                )";

                SqlCommand cmd = new SqlCommand(sql, conn);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    list.Add(new BookingIntakeVM
                    {
                        BookingId = (int)dr["BookingId"],
                        LearnerName = dr["L_FullName"].ToString(),
                        Grade = (int)dr["Grade"],
                        ParentName = dr["P_FullName"].ToString(),
                        ParentPhone = dr["P_PhoneNumber"].ToString(),
                        ParentEmail = dr["P_Email"].ToString()
                    });
                }
            }
            return list;
        }

        // SAVE RESULT
        public static void SaveIntakeResult(int bookingId, bool passed)
        {
            using (SqlConnection conn = new SqlConnection(cs))
            {
                string sql = @"
                INSERT INTO IntakeResult (BookingId, Passed)
                VALUES (@BookingId, @Passed)";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@BookingId", bookingId);
                cmd.Parameters.AddWithValue("@Passed", passed);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // PASSED STUDENTS
        public static List<BookingIntakeVM> GetPassedStudents()
        {
            var list = new List<BookingIntakeVM>();

            using (SqlConnection conn = new SqlConnection(cs))
            {
                string sql = @"
                SELECT b.BookingId,
                       b.L_FullName,
                       b.Grade,
                       b.P_FullName,
                       b.P_PhoneNumber,
                       b.P_Email
                FROM Booking b
                INNER JOIN IntakeResult i ON b.BookingId = i.BookingId
                WHERE i.Passed = 1 AND b.IsRegistered = 0";

                SqlCommand cmd = new SqlCommand(sql, conn);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    list.Add(new BookingIntakeVM
                    {
                        BookingId = (int)dr["BookingId"],
                        LearnerName = dr["L_FullName"].ToString(),
                        Grade = (int)dr["Grade"],
                        ParentName = dr["P_FullName"].ToString(),
                        ParentPhone = dr["P_PhoneNumber"].ToString(),
                        ParentEmail = dr["P_Email"].ToString()
                    });
                }
            }
            return list;
        }

        

    }
}