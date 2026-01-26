using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;

namespace LearninngManagementSystem.Models
{
    public class SubjectRepo
    {
        public static string ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        public static void AddSubject(Subject subject)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                string query = "INSERT INTO Subjects (SubjectName, SubjectCode, CreditHours, Grade, Description) " +
                               "VALUES (@SubjectName, @SubjectCode, @CreditHours, @Grade, @Description)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@SubjectName", subject.SubjectName);
                cmd.Parameters.AddWithValue("@SubjectCode", subject.SubjectCode);
                cmd.Parameters.AddWithValue("@CreditHours", subject.CreditHours);
                cmd.Parameters.AddWithValue("@Grade", subject.Grade);
                cmd.Parameters.AddWithValue("@Description", subject.Description);
                cmd.ExecuteNonQuery();
            }
        }
    }
}