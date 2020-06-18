using BugTracker.BL;
using BugTracker.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;

namespace BugTracker.Controllers
{
    public class TicketController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index() {
            var a = db.Tickets.ToList();
            return View(a);
        }

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
            TicketLogic ticket = new TicketLogic();
            ticket.createTicket(model, userId);
            
            return RedirectToAction("Index", "Home");
        }
    }
}