using BugTracker.BL;
using BugTracker.Models;
using BugTracker.Models.ProjectClasses;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;

namespace BugTracker.Controllers {
    public class TicketController : Controller {
        ApplicationDbContext db = new ApplicationDbContext();
        TicketLogic ticketLogic = new TicketLogic();
        public ActionResult Index() {
            var a = db.Tickets.ToList();
            return View(a);
        }

        // GET: Create Ticket
        [HttpGet]
        public ActionResult CreateTicket() {
            return View();
        }

        // POST: Create Ticket
        [HttpPost]
        public ActionResult CreateTicket(CreateTicketViewModel model) {
            if (ModelState.IsValid) {
                var userId = User.Identity.GetUserId();
                ticketLogic.createTicket(model, userId);
                return RedirectToAction("Index");
            } 
            return RedirectToAction("Error");
        }
        public ActionResult DeleteTicket(int ticketId) {
            var ticket = ticketLogic.getTicketById(ticketId);
            if (ticket == null) {
                return RedirectToAction("Error");
            }

            ticketLogic.deleteTicket(ticket);
            return RedirectToAction("Index");
        }
        // GET: Update Ticket By Submitter
        [HttpGet]
        public ActionResult UpdateTicketBySubmitter(int ticketId) {
            var ticket = ticketLogic.updateTicketById(ticketId);
            if(ticket == null) {
                return RedirectToAction("Error");
            }

            return View(ticket);
        }

        // POST: Update Ticket By Submitter
        [HttpPost]
        public ActionResult UpdateTicketBySubmitter(CreateTicketViewModel model) {
            if (ModelState.IsValid) {
                var userId = User.Identity.GetUserId();
                ticketLogic.updateTicket(model);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Error");
        }


    }
}