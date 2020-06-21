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
        TicketLogic ticketLogic = new TicketLogic();
        public ActionResult Index()
        {
            var a = db.Tickets.ToList();
            return View(a);
        }


        // GET: Assign Ticket
        [Authorize(Roles = "Admin, Manager")]
        [HttpGet]
        public ActionResult AssignTicket(int ticketId)
        {
            var ticket = ticketLogic.getTicketById(ticketId);
            if (ticket == null)
            {
                return RedirectToAction("Error");
            }
            var userId = User.Identity.GetUserId();
            var users = ticketLogic.getAllDeveloperUser(userId);
            ViewBag.Ticket = ticket;
            ViewBag.DeveloperId = new SelectList(users, "Id", "UserName");
            return View();
        }

        // POST: Assign Ticket
        [Authorize(Roles = "Admin, Manager")]
        [HttpPost]
        public ActionResult AssignTicket(AssignTicketViewModel model)
        {
            if (ModelState.IsValid)
            {
                ticketLogic.assignTicket(model);
                return RedirectToAction("Index");
            }

            var userId = User.Identity.GetUserId();
            var users = ticketLogic.getAllDeveloperUser(userId);
            ViewBag.DeveloperId = new SelectList(users, "Id", "UserName");
            return View();
        }

        [HttpGet]
        public ActionResult CreateTicket()
        {
            return View();
        }

        // POST: Create Ticket
        [HttpPost]
        public ActionResult CreateTicket(CreateTicketViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();
                ticketLogic.createTicket(model, userId);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Error");
        }

        public ActionResult DeleteTicket(int ticketId)
        {
            var ticket = ticketLogic.getTicketById(ticketId);
            if (ticket == null)
            {
                return RedirectToAction("Error");
            }

            ticketLogic.deleteTicket(ticket);
            return RedirectToAction("Index");
        }

        // GET: Update Ticket By Submitter
        [HttpGet]
        public ActionResult UpdateTicketBySubmitter(int ticketId)
        {
            var ticket = ticketLogic.updateTicketById(ticketId);
            if (ticket == null)
            {
                return RedirectToAction("Error");
            }

            return View(ticket);
        }

        // POST: Update Ticket By Submitter
        [HttpPost]
        public ActionResult UpdateTicketBySubmitter(CreateTicketViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();
                ticketLogic.updateTicket(model);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Error");
        }


        public ActionResult Error()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }
    }
}