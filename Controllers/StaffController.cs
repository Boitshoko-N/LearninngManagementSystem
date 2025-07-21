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
            return View();
        }
        public ActionResult TresurerView()
        {
            return View();
        }
        public ActionResult ClerkView()
        {
            return View();
        }
        public ActionResult TeacherView()
        {
            return View();
        }

    }
}