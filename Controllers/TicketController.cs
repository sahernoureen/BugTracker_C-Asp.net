using BugTracker.BL;
using BugTracker.Models;
using Microsoft.AspNet.Identity;
using PagedList;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace BugTracker.Controllers
{
    public class TicketController : Controller
    {
        TicketLogic ticketLogic = new TicketLogic();
        ProjectLogic projectLogic = new ProjectLogic();

        public ActionResult Index(int? page)
        {

            if (User.IsInRole("Admin"))
            {
                ViewBag.classView = "centerTextAdmin";
            }
            else if (User.IsInRole("Manager"))
            {
                ViewBag.classView = "centerTextManager";
            }
            else if (User.IsInRole("Developer"))
            {
                ViewBag.classView = "centerTextDev";
            }
            else if (User.IsInRole("Submitter"))
            {
                ViewBag.classView = "centerTextSubmitter";
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            var a = ticketLogic.getAllTicket().OrderByDescending(s => s.Id).ToPagedList(pageNumber, pageSize);

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
                return Redirect("Shared/Error");
            }
            var userId = User.Identity.GetUserId();
            var users = new List<ApplicationUser>();
            if (AdminLogic.CheckIfUserIsInRole(userId, "Admin"))
            {
                users = ticketLogic.getAllManagerAndDeveloperUser(userId);
            }
            else
            {
                users = ticketLogic.getAllDeveloperUser(userId);
            }

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
            var users = new List<ApplicationUser>();
            if (AdminLogic.CheckIfUserIsInRole(userId, "Admin"))
            {
                users = ticketLogic.getAllManagerAndDeveloperUser(userId);
            }
            else
            {
                users = ticketLogic.getAllDeveloperUser(userId);
            }
            ViewBag.DeveloperId = new SelectList(users, "Id", "UserName");
            return View();
        }

        [Authorize(Roles = "Submitter")]
        [HttpGet]
        public ActionResult CreateTicket()
        {
            var projects = projectLogic.getAllProjects();
            ViewBag.ProjectId = new SelectList(projects, "Id", "Name");
            return View();
        }

        // POST: Create Ticket
        [HttpPost]
        public ActionResult CreateTicket(CreateTicketViewModel model)
        {
            var projects = projectLogic.getAllProjects();
            ViewBag.ProjectId = new SelectList(projects, "Id", "Name");

            if (ModelState.IsValid)
            {
                var userId = User.Identity.GetUserId();
                ticketLogic.createTicket(model, userId);
                return RedirectToAction("Index");
            }
            return Redirect("Shared/Error");
        }

        public ActionResult DeleteTicket(int ticketId)
        {
            var ticket = ticketLogic.getTicketById(ticketId);
            if (ticket == null)
            {
                return Redirect("Shared/Error");
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
                return Redirect("Shared/Error");
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
                ticketLogic.updateTicket(model, userId);
                return RedirectToAction("Index");
            }
            return Redirect("Shared/Error");
        }


        [HttpGet]
        public ActionResult History(int ticketId)
        {
            var ticketHistory = ticketLogic.GetHistoryOfTicket(ticketId);
            if (ticketHistory == null)
            {
                return Redirect("Shared/Error");
            }

            return View(ticketHistory);
        }
        public ActionResult Error()
        {
            return View();
        }
    }
}