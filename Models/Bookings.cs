using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;

namespace LearninngManagementSystem.Models
{
    public class Bookings
    {
        public static string ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        public static List<SlotDate> AvailableSlotDates()
        {
            List<SlotDate> slotDates = new List<SlotDate>();

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                string query = "SELECT * FROM SlotDate WHERE IsAvailable = 1";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    SlotDate slotDate = new SlotDate
                    {
                        SlotDateId = (int)reader["SlotDateId"],
                        Date = (DateTime)reader["Date"],
                    };
                    slotDates.Add(slotDate);
                }
                reader.Close();
            }
            return slotDates;
        }

        public static List<Slot> GetSlots()
        {
            List<Slot> slots = new List<Slot>();
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Slot WHERE IsAvailable = 1";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Slot slot = new Slot
                    {
                        SlotId = Convert.ToInt32(reader["SlotId"]),
                        StartTime = (TimeSpan)reader["StartTime"],
                        EndTime = (TimeSpan)reader["EndTime"],
                        SlotDateId = Convert.ToInt32(reader["SlotDateId"]),
                        maxBookings = Convert.ToInt32(reader["maxBookings"])
                    };
                    slots.Add(slot);
                }
                reader.Close();
            }
            return slots;

        }

        private static readonly Random _random = new Random();

        public static string GenerateBookingNumber()
        {
            string datePart = DateTime.Now.ToString("yyyyMMdd");
            int randomPart = _random.Next(1000, 9999);

            return $"BK-{datePart}-{randomPart}";
        }




        public static void SaveBooking(Booking booking)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                
                string query = @"
                   INSERT INTO Booking
                   ( BookingNo,SlotId, SlotDateId, P_FullName, L_FullName,
                   P_PhoneNumber, L_PhoneNumber, BookingDate, Home_Language, Grade, DateMade)
                   VALUES
                  ( @BookingNo, @SlotId, @SlotDateId, @P_FullName, @L_FullName,
                   @P_PhoneNumber, @L_PhoneNumber, @BookingDate, @Home_Language, @Grade, @DateMade)";

                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@BookingNo", booking.BookingNo);
                cmd.Parameters.AddWithValue("@SlotId", booking.SlotId);
                cmd.Parameters.AddWithValue("@SlotDateId", booking.SlotDateId);
                cmd.Parameters.AddWithValue("@P_FullName", booking.P_FullName);
                cmd.Parameters.AddWithValue("@L_FullName", booking.L_FullName);
                cmd.Parameters.AddWithValue("@P_PhoneNumber", booking.P_PhoneNumber);
                cmd.Parameters.AddWithValue("@L_PhoneNumber", booking.L_PhoneNumber);
                cmd.Parameters.AddWithValue("@Home_Language", booking.Home_Language);
                cmd.Parameters.AddWithValue("@Grade", booking.Grade);
                cmd.Parameters.AddWithValue("@DateMade", booking.DateMade);
                cmd.Parameters.AddWithValue("@BookingDate", booking.BookingDate);
                cmd.ExecuteNonQuery();
            }
        }

        public static DateTime GetSlotDateFromId(int slotDateId)
        {
            using (SqlConnection conn = new SqlConnection(Bookings.ConnectionString))
            {
                conn.Open();
                string query = "SELECT Date FROM SlotDate WHERE SlotDateId = @SlotDateId";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@SlotDateId", slotDateId);

                var result = cmd.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                {
                    return (DateTime)result;
                }
                else
                {
                    // fallback to some default valid date
                    return DateTime.Now;
                }
            }
        }


        public static BookingConfirmationVM GetBookingByNo(string bookingNo)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                string query = @"
           SELECT 
           b.BookingNo,
           b.DateMade,
           sd.Date AS SlotDate,
           s.StartTime,
           s.EndTime,
           b.P_FullName,
           b.L_FullName,
           b.P_PhoneNumber,
           b.L_PhoneNumber,
           b.Home_Language,
           b.Grade
           FROM Booking b
          INNER JOIN Slot s ON b.SlotId = s.SlotId
          INNER JOIN SlotDate sd ON b.SlotDateId = sd.SlotDateId
          WHERE b.BookingNo = @BookingNo";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@BookingNo", bookingNo); 

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return new BookingConfirmationVM
                    {
                        BookingNo = reader["BookingNo"].ToString(),
                        DateMade = (DateTime)reader["DateMade"],
                        SlotDate = (DateTime)reader["SlotDate"],
                        StartTime = (TimeSpan)reader["StartTime"],
                        EndTime = (TimeSpan)reader["EndTime"],
                        P_FullName = reader["P_FullName"].ToString(),
                        L_FullName = reader["L_FullName"].ToString(),
                        P_PhoneNumber = reader["P_PhoneNumber"].ToString(),
                        L_PhoneNumber = reader["L_PhoneNumber"].ToString(),
                        Home_Language = reader["Home_Language"].ToString(),
                        Grade = (int)reader["Grade"]
                    };
                }
            }

            return null;
        }



    }
}