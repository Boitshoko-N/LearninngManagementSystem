using LearninngManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LearninngManagementSystem.Controllers
{
    public class StaffController : Controller
    {
        // GET: Staff
        public ActionResult PrincipalView()
        {
            var model = new ManageViewModel
            {
                Students = StudentRepository.GetStudents(),
                Staff = StaffRepository.GetAllStaff()
            };
            return View(model);
        }
        public ActionResult TresurerView()
        {
            return View();
        }
        
        public ActionResult TeacherView()
        {
            return View();
        }

    }
}