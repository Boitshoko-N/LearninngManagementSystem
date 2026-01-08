using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LearninngManagementSystem.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult StudentPortalView()
        {
            if (Session["IsLoggedIn"] == null)
            {
                return RedirectToAction("StudentLogInView", "User");
            }

            return View();
        }


        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("StudentLogInView", "User");
        }


    }
}