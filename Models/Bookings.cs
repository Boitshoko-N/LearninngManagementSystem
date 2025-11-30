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
                        SlotId = (int)reader["SlotId"],
                        StartTime = (DateTime)reader["StartTime"],
                        EndTime = (DateTime)reader["EndTime"],
                        SlotDateId = (int)reader["SlotDateId"],
                        maxBookings = (int)reader["maxBookings"]
                    };
                    slots.Add(slot);
                }
                reader.Close();
            }
            return slots;

        }
    }
}