using LearninngManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LearninngManagementSystem.Controllers
{
    [Authorize(Roles = "Clerk")]
    public class ClerkController : Controller
    {
        // ================= DASHBOARD =================
        public ActionResult ClerkView()
        {
            return View();
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
        public ActionResult ConfirmRegister(int studentId)
        {
            var student = StudentRepository.GetById(studentId);

            if (student == null)
                return HttpNotFound();

            // Generate numbers
            student.StudentNumber = RegistrationService.GeneratePermanentNumber();
            student.Parent.ParentNumber = RegistrationService.GeneratePermanentNumber();

            // Temp password
            string tempPassword = RegistrationService.GenerateTempPassword();

            student.IsRegistered = true;
            student.FirstLogin = true;

            StudentRepository.Update(student);

            SmsService.Send(
                student.Parent.Phone,
                $"Student No: {student.StudentNumber}, Parent No: {student.Parent.ParentNumber}, Temp Password: {tempPassword}"
            );

            TempData["SuccessMessage"] = "Student registered successfully.";
            return RedirectToAction("RegisterStudent");
        }

    }
}