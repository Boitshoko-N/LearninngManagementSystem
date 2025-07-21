using LearninngManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LearninngManagementSystem.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult GuestServiceView()
        {
            return View();
        }
        public ActionResult StudentLogInView()
        {
            return View();
        }
        public ActionResult StaffLogInView()
        {
            return View();
        }

        [HttpPost]
        public ActionResult BookingView(Booking booking)
        {
            booking.BookingId = Guid.NewGuid().ToString();
            booking.DateMade = DateTime.Now;
            return View("BookingConfirmedView",booking);
        }

        [HttpPost]
        public ActionResult StudentLogInView(StudentLogInViewModel model)
        {
            var savedStudent = StudentRepository.GetStudentByStudentNo(model.StudentNo);

            if (savedStudent != null && savedStudent.Password == model.Password) {
                return RedirectToAction("StudentPortalView", "Student");

            }
            ModelState.AddModelError("", "Invalid login credentials.");
            return View(model);
        }

        [HttpPost]
        public ActionResult StaffLogInView(StaffLogInViewModel model)
        {
            var savedStaff = StaffRepository.GetStaffByStaffNo(model.StaffNo);
            
            if(savedStaff != null && savedStaff.Password == model.Password)
            {
                if (savedStaff.Type == "Principal")
                {
                    return RedirectToAction("PrincipalView", "Staff");
                }
                else if (savedStaff.Type == "Tresurer")
                {
                    return RedirectToAction("TresurerView", "Staff");
                }
                else if (savedStaff.Type == "Clerk")
                {
                    return RedirectToAction("ClerkView", "Staff");
                }
                else
                {
                    return RedirectToAction("TeacherView", "Staff");
                }
            }
            ModelState.AddModelError("", "Invalid log in details.");
            return View(model);
        }
        public ActionResult BookingView() {
            return View();
        }
        public ActionResult BookingConfirmedView()
        {
            return View();
        }
    }
}