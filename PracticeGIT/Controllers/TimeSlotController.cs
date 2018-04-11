using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PracticeGIT.Models;
using PracticeGIT.Services;

namespace PracticeGIT.Controllers
{
    [RoutePrefix("api")]
    public class TimeSlotController : Controller
    {
        private TimeSlotService slotService;

        public TimeSlotController()
        {
           slotService = new TimeSlotService();
        }
        
        // GET: Lists all master time slots
        [Route("slots")]
        public ActionResult GetMasterSlots()
        {
            return Json(slotService.GetAllSlots(), JsonRequestBehavior.AllowGet);
        }

        [Route("users/{userId}/slots")]
        public ActionResult GetSlots(int userId)
        {
            return Json(slotService.GetUserSlots(userId), JsonRequestBehavior.AllowGet);
        }

        [Route("users/{userId}/slots")]
        [HttpPost]
        public ActionResult AddSlot(int userId, TimeSlotPreference preference)
        {
            slotService.AddSlot(userId, preference);
            return new HttpStatusCodeResult(System.Net.HttpStatusCode.NoContent);
        }

        [Route("users/{userId}/slots/available")]
        public ActionResult GetAvailableSlots(int userId)
        {
            return Json(slotService.GetAvailableSlots(userId), JsonRequestBehavior.AllowGet);
        }

    }
}