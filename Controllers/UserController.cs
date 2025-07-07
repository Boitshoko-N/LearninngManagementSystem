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

        [HttpPost]
        public ActionResult BookingView(Booking booking)
        {

        }
        public ActionResult BookingView() {
            return View();
        }
    }
}