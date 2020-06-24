using BugTracker.BL;
using BugTracker.Models;
using BugTracker.Models.ProjectClasses;
using Microsoft.AspNet.Identity;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace BugTracker.Controllers {
    public class TicketController : Controller {
        private readonly TicketLogic ticketLogic = new TicketLogic();
        private readonly ProjectLogic projectLogic = new ProjectLogic();

        public ActionResult Index(string sortOrder, int? page, string TitleHolder , string userId) {

            if (User.IsInRole("Admin")) {
                ViewBag.classView = "centerTextAdmin";
            } else if (User.IsInRole("Manager")) {
                ViewBag.classView = "centerTextManager";
            } else if (User.IsInRole("Developer")) {
                ViewBag.classView = "centerTextDev";
            } else if (User.IsInRole("Submitter")) {
                ViewBag.classView = "centerTextSubmitter";
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);
          

            string title = Request.Form["inputStr"];
            List<Ticket> allTickets = new List<Ticket>();
            if (title != null && title != "") {
                ViewBag.TitleHolder = title;             
                allTickets = ticketLogic.getTicketByTitle(title);
            }  else if (TitleHolder != "" && TitleHolder != null) {
                ViewBag.TitleHolder = TitleHolder;
                allTickets = ticketLogic.getTicketByTitle(TitleHolder);
            } else if (userId != null && userId != "") {
                ViewBag.userId = userId;
                allTickets = ticketLogic.getTicketByUserId(userId);
            } else {
                allTickets = ticketLogic.getAllTicket();
                ViewBag.TitleHolder = "";
                ViewBag.userId = "";
            }



            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.ProjectSortParm = sortOrder == "Project" ? "project_desc" : "Project";
            ViewBag.TitleSortParm = sortOrder == "Title" ? "title_desc" : "Title";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            ViewBag.PrioritySortParm = sortOrder == "Priority" ? "priority_desc" : "Priority";
            ViewBag.DevSortParm = sortOrder == "Developer" ? "dev_desc" : "Developer";

            switch (sortOrder) {
                case "name_desc":
                    allTickets = allTickets.OrderByDescending(n => n.OwnerUser.UserName).ToList();
                    break;
                case "Date":
                    allTickets = allTickets.OrderBy(s => s.Created).ToList();
                    break;
                case "date_desc":
                    allTickets = allTickets.OrderByDescending(s => s.Created).ToList();
                    break;
                case "Priority":
                    allTickets = allTickets.OrderBy(s => s.TicketPriority.Priority).ToList();
                    break;
                case "priority_desc":
                    allTickets = allTickets.OrderByDescending(s => s.TicketPriority.Priority).ToList();
                    break;
                case "Project":
                    allTickets = allTickets.OrderBy(s => s.Project.Name).ToList();
                    break;
                case "project_desc":
                    allTickets = allTickets.OrderByDescending(s => s.Project.Name).ToList();
                    break;
                case "Title":
                    allTickets = allTickets.OrderBy(s => s.Title).ToList();
                    break;
                case "title_desc":
                    allTickets = allTickets.OrderByDescending(s => s.Title).ToList();
                    break;
                case "Developer":
                    allTickets = allTickets.OrderBy(s => s.AssignedToUser.UserName).ToList();
                    break;
                case "dev_desc":
                    allTickets = allTickets.OrderByDescending(s => s.AssignedToUser.UserName).ToList();
                    break;
                default:
                    allTickets = allTickets.OrderBy(s => s.OwnerUser.UserName).ToList();
                    break;
            }

            return View(allTickets.ToPagedList(pageNumber, pageSize));
        }


        // GET: Assign Ticket
        [Authorize(Roles = "Admin, Manager")]
        [HttpGet]
        public ActionResult AssignTicket(int ticketId) {
            var ticket = ticketLogic.getTicketById(ticketId);
            if (ticket == null) {
                return RedirectToAction("Error");
            }
            var userId = User.Identity.GetUserId();
            var users = new List<ApplicationUser>();
            if (AdminLogic.CheckIfUserIsInRole(userId, "Admin")) {
                users = ticketLogic.getAllManagerAndDeveloperUser(userId);
            } else {
                users = ticketLogic.getAllDeveloperUser(userId);
            }

            ViewBag.Ticket = ticket;
            ViewBag.DeveloperId = new SelectList(users, "Id", "UserName");
            return View();
        }

        // POST: Assign Ticket
        [Authorize(Roles = "Admin, Manager")]
        [HttpPost]
        public ActionResult AssignTicket(AssignTicketViewModel model) {
            if (ModelState.IsValid) {
                ticketLogic.assignTicket(model);

                return RedirectToAction("Index");
            }

            var userId = User.Identity.GetUserId();
            var users = new List<ApplicationUser>();
            if (AdminLogic.CheckIfUserIsInRole(userId, "Admin")) {
                users = ticketLogic.getAllManagerAndDeveloperUser(userId);
            } else {
                users = ticketLogic.getAllDeveloperUser(userId);
            }
            ViewBag.DeveloperId = new SelectList(users, "Id", "UserName");
            return View();
        }

        [Authorize(Roles = "Submitter")]
        [HttpGet]
        public ActionResult CreateTicket() {
            var projects = projectLogic.getAllProjects();
            ViewBag.ProjectId = new SelectList(projects, "Id", "Name");
            return View();
        }

        // POST: Create Ticket
        [HttpPost]
        public ActionResult CreateTicket(CreateTicketViewModel model) {
            var projects = projectLogic.getAllProjects();
            ViewBag.ProjectId = new SelectList(projects, "Id", "Name");

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
            if (ticket == null) {
                return RedirectToAction("Error");
            }

            return View(ticket);
        }

        // POST: Update Ticket By Submitter
        [HttpPost]
        public ActionResult UpdateTicketBySubmitter(CreateTicketViewModel model) {
            if (ModelState.IsValid) {
                var userId = User.Identity.GetUserId();
                ticketLogic.updateTicket(model, userId);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Error");
        }


        [HttpGet]
        public ActionResult History(int ticketId) {
            var ticketHistory = ticketLogic.GetHistoryOfTicket(ticketId);
            if (ticketHistory == null) {
                return RedirectToAction("Error");
                
            }

            return View(ticketHistory);
        }
        public ActionResult Error() {
            return View();
        }

        public ActionResult GetRelatedTickets(string input) {

            var titles = SearchLogic.GetRelatedTickets(input);
            return Json(titles, JsonRequestBehavior.AllowGet);
        }
    }
}