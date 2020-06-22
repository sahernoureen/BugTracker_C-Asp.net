using BugTracker.BL;
using BugTracker.Models;
using BugTracker.Models.ProjectClasses;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BugTracker.Controllers
{
    public class TicketCommentController : Controller
    {
        // GET: TicketComment
        TicketCommentLogic ticketCommentLogic = new TicketCommentLogic();
        ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index(int ticketId)
        {
           var TicketComments =  ticketCommentLogic.getTicketCommentsById(ticketId);
            ViewBag.ticketId = ticketId;
            return View(TicketComments);
        }

        [HttpGet]
        public ActionResult CreateTicketComment(int ticketId)
        {
            var userId = User.Identity.GetUserId();
            ViewBag.userId = userId;
            ViewBag.ticketId = ticketId;
            return View();
        }

        // POST: Create Ticket
        [HttpPost]
        public ActionResult CreateTicketComment(TicketComment model, int ticketId)
        {
            ViewBag.TicketId = ticketId;
            if (ModelState.IsValid)
            {
                var UserId = User.Identity.GetUserId();
                model.Created = DateTime.Now;
                model.TicketId = ticketId;                  
                ticketCommentLogic.createTicketComment(model, UserId);
                return RedirectToAction("Index",new { ticketId = ticketId });
            }
           
            return RedirectToAction("Error");
        }



        [HttpGet]
        public ActionResult UpdateTicketComment(int ticketCommentId)
        {
            var ticketComment = ticketCommentLogic.GetTicketComment(ticketCommentId);
            if (ticketComment == null)
            {
                return RedirectToAction("Error");
            }
            ViewBag.CommentId = ticketComment.Id;
            return View(ticketComment);
        }

        // POST: Update Ticket By Submitter
        [HttpPost]
        public ActionResult UpdateTicketComment(TicketComment model)
        {
            if (ModelState.IsValid)
            {
               
                ticketCommentLogic.updateTicketComment(model);
                return RedirectToAction("Index", new { ticketId = model.TicketId });
            }
            return RedirectToAction("Error");
        }


        public ActionResult DeleteTicketComment(int ticketCommentId)
        {
            var ticketComment = ticketCommentLogic.GetTicketComment(ticketCommentId);
            if (ticketComment == null)
            {
                return RedirectToAction("Error");
            }

            ticketCommentLogic.deleteTicketComment(ticketComment);
            return RedirectToAction("Index", new { ticketId = ticketComment.TicketId });
        }




        public ActionResult Error()
        {
            return View();
        }

    }
}