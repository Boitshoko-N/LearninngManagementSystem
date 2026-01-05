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

        public ActionResult GetSlotsByDate(int dateId)
        {
            var slots = Bookings.GetSlots()
                .Where(s => s.SlotDateId == dateId)
                .Select(s => new
                {
                    s.SlotId,
                    StartTime = s.StartTime.ToString(@"hh\:mm"),
                    EndTime = s.EndTime.ToString(@"hh\:mm")
                })
                .ToList();

            return Json(slots, JsonRequestBehavior.AllowGet);
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
        public ActionResult BookingView(BookingSlotVM model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.SlotDates = Bookings.AvailableSlotDates();
                return View(model);
            }

            var booking = new Booking
            {
                BookingId = Guid.NewGuid().ToString(),
                SlotId = model.SlotId,
                SlotDateId = model.SlotDateId,
                P_FullName = model.P_FullName,
                L_FullName = model.L_FullName,
                P_PhoneNumber = model.P_PhoneNumber,
                L_PhoneNumber = model.L_PhoneNumber,
                Home_Language = model.Home_Language,
                Grade = model.Grade,
                DateMade = DateTime.Now,
                BookingDate = Bookings.GetSlotDateFromId(model.SlotDateId)
            };

            Bookings.SaveBooking(booking);

            return RedirectToAction("BookingConfirmedView", new { id = booking.BookingId });
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
       
        
        public ActionResult BookingConfirmedView(int id)
        {
            var model = Bookings.GetBookingById(id);
            return View(model);
        }



        public ActionResult ParentLogInView()
        {
            return View();
        }
    }
}