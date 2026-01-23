using System.Net;
using System.Net.Mail;

namespace LearninngManagementSystem.Services
{
    public static class EmailService
    {
        public static void SendRegistrationEmail(
            string parentEmail,
            string studentEmail,
            string parentNumber,
            string parentPassword,
            string studentNumber,
            string studentPassword)
        {
            var message = new MailMessage();
            message.From = new MailAddress("boitshokomphahlele@gmail.com");
            message.To.Add(parentEmail);

            // Optional: also send to student
            if (!string.IsNullOrEmpty(studentEmail))
            {
                message.To.Add(studentEmail);
            }

            message.Subject = "Student Registration Confirmation";

            message.Body = $@"
               Dear Parent,

               Your child has been successfully registered.

               PARENT LOGIN DETAILS
               --------------------
               Parent Number: {parentNumber}
               Temporary Password: {parentPassword}

               STUDENT LOGIN DETAILS
               --------------------
               Student Number: {studentNumber}
               Temporary Password: {studentPassword}

               Please log in and change your passwords on first login.

               Regards,
               JEKE HIGH School Administration
               ";

            message.IsBodyHtml = false;

            var smtp = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential(
                    "boitshokomphahlele@gmail.com",
                    "APP_PASSWORD_HERE"
                ),
                EnableSsl = true
            };

            smtp.Send(message);
        }
    }
}
