using LearninngManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace LearninngManagementSystem.Controllers
{
    public class ManageController : Controller
    {
        // GET: Manage
        public ActionResult SubjectRegistration()
        {
            return View();
        }

        public ActionResult AddSubject()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddSubject(Subject subject)
        {
            if (!ModelState.IsValid)
            {
                return View(subject);
            }

            var newsubject = new Subject
            {
                SubjectName = subject.SubjectName,
                SubjectCode = subject.SubjectCode,
                CreditHours = subject.CreditHours,
                MarksObtained = subject.MarksObtained,
                Grade = subject.Grade,
                Description = subject.Description
            };
            SubjectRepo.AddSubject(newsubject);
            return RedirectToAction("AddSubject");
        }
    }
}