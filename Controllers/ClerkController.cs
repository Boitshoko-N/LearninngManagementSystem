using LearninngManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LearninngManagementSystem.Services;
using static LearninngManagementSystem.Models.StudentRepository;

namespace LearninngManagementSystem.Controllers
{
    
    public class ClerkController : Controller
    {
        // ================= DASHBOARD =================
        public ActionResult ClerkView()
        {
            ViewBag.StudentCount = StudentRepository.GetStudentCount();
            return View();
        }

        public ActionResult RecordMarks()
        {
            var model = ClerkRepository.GetBookingsForIntake();
            return View(model);
        }

        [HttpPost]
        public ActionResult SaveResult(int bookingId, bool passed)
        {
            ClerkRepository.SaveIntakeResult(bookingId, passed);
            return RedirectToAction("RecordMarks");
        }

        public ActionResult RegisterStudent()
        {
            var model = ClerkRepository.GetPassedStudents();
            return View(model);
        }






        // ================= ACADEMIC RECORDS =================
        public ActionResult GenerateClassList()
        {
            return View();
        }

        // ================= COMMUNICATION =================
        public ActionResult SendAnnouncement()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SendAnnouncement(object model)
        {
            // TODO: Send announcement to parents & teachers
            TempData["SuccessMessage"] = "Announcement sent successfully.";
            return RedirectToAction("ClerkView");
        }

        // ================= STUDENT MANAGEMENT =================

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmRegister(int bookingId)
        {
            // Generate numbers (as strings)
            string studentNoStr = RegistrationService.GeneratePermanentNumber();
            string parentNoStr = RegistrationService.GeneratePermanentNumber();

            // Convert student and parent numbers to int
            if (!int.TryParse(studentNoStr, out int studentNo))
            {
                TempData["ErrorMessage"] = "Invalid student number generated.";
                return RedirectToAction("RegisterStudent");
            }
            if (!int.TryParse(parentNoStr, out int parentNo))
            {
                TempData["ErrorMessage"] = "Invalid parent number generated.";
                return RedirectToAction("RegisterStudent");
            }

            // Generate passwords
            string studentPassword = RegistrationService.GenerateTempPassword();
            string parentPassword = RegistrationService.GenerateTempPassword();

            // Create student & parent and get emails
            RegistrationEmails emails = StudentRepository.CreateStudentFromBooking(
                bookingId,
                studentNo,
                parentNo,
                studentPassword,
                parentPassword
            );

            // SEND EMAIL
            EmailService.SendRegistrationEmail(
                emails.ParentEmail,
                emails.StudentEmail,
                parentNoStr,
                parentPassword,
                studentNoStr,
                studentPassword
            );

            TempData["SuccessMessage"] =
                "Student registered successfully. Login details sent via email.";

            return RedirectToAction("RegisterStudent");
        }



    }
}