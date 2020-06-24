using BugTracker.BL;
using BugTracker.Models;
using BugTracker.Models.ProjectClasses;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;

namespace BugTracker.Controllers
{
    [Authorize(Roles = "Admin, Manager, Developer")]
    public class DeveloperController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        TicketLogic ticketLogic = new TicketLogic();
       

        public ActionResult Index()
        {
            var dev = this.User.Identity.GetUserId();
            var devTickets = db.Tickets.Where(u => u.AssignedToUserId == dev).ToList();
            ViewBag.NotificationCount = GetNotificationsCount();
            ViewBag.UserId = dev;
            return View(devTickets);
        }

        public int GetNotificationsCount()
        {
            var dev = this.User.Identity.GetUserId();
            var NotificationsOFUser = ticketLogic.GetAllNotificationsForUser(dev);
            return NotificationsOFUser.Count;
        }

      
        public ActionResult DeveloperNotifications()
        {
            var dev = this.User.Identity.GetUserId();
            var NotificationsOFUser = ticketLogic.GetAllNotificationsForUser(dev);
            return View(NotificationsOFUser);
        }

        public ActionResult UpdateNotifications(int NotifiId)
        {
            var dev = this.User.Identity.GetUserId();
            var NotificationsOFUser = ticketLogic.GetAllNotificationsForUser(dev);
            ticketLogic.UpdateTicketNotification(NotifiId);         
            return View(NotificationsOFUser);
        }
   
    }
}