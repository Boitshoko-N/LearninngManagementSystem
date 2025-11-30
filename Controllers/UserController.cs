using LearninngManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

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

        public ActionResult BookingView()
        {
            var slotDates = Bookings.AvailableSlotDates();
            var slots = Bookings.GetSlots();
            ViewBag.SlotDates = slotDates;
            ViewBag.Slots = slots;
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
            else
            {
                ViewBag.ErrorMessage = "Invalid login details";
                return View("StudentLogInView", model);
            }
            
        }

        [HttpPost]
        public ActionResult StaffLogInView(StaffLogInViewModel model)
        {
            var savedStaff = StaffRepository.GetStaffByStaffNo(model.StaffNo);


            if (savedStaff != null && savedStaff.Password == model.Password)
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
            else
            {
                ViewBag.ErrorMessage = "Invalid login details";
                return View("StaffLogInView", model);
            }
           
            
             

            
            


            
        }
       
        public ActionResult BookingConfirmedView()
        {
            return View();
        }

        public ActionResult ParentLogInView()
        {
            return View();
        }
    }
}