using BugTracker.BL;
using BugTracker.Models;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;

namespace BugTracker.Controllers
{
    public class TicketController : Controller
    {
        TicketLogic ticketLogic = new TicketLogic();
        // GET: Ticket
        [HttpGet]
        public ActionResult CreateTicket()
        {
            return View();
        }
        // GET: Ticket
        [HttpPost]
        public ActionResult CreateTicket(CreateTicketViewModel model)
        {
            var userId = User.Identity.GetUserId();
            ticketLogic.createTicket(model, userId);

            return RedirectToAction("Index", "Home");
        }
    }
}