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
        public ActionResult BookingView() {
            return View();
        }
        public ActionResult BookingConfirmedView()
        {
            return View();
        }
    }
}