using BugTracker.BL;
using BugTracker.Models.ProjectClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BugTracker.Controllers
{
    public class TicketAttachmentController : Controller
    {
        TicketAttachmentLogic ticketAttachmentLogic = new TicketAttachmentLogic();
        //get
        public  ActionResult UploadFile(int ticketId) 
        {
            ViewBag.TicketId = ticketId;
            return View();
        }

        public ActionResult Index(int ticketId)
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

            var TicketAttachments = ticketAttachmentLogic.getTicketAttachmentsById(ticketId);
            ViewBag.ticketId = ticketId;
            return View(TicketAttachments);
        }
        //post
        [HttpPost]
        public ActionResult createTicketAttachment(TicketAttachmentViewModel ticketAttachmentViewModel)
        {

            ticketAttachmentLogic.createTicketAttachment(ticketAttachmentViewModel);
            return View();
        }

        [HttpGet]
        public ActionResult UpdateTicketAttachment(int ticketAttachmentId)
        {
            var ticketAttachment = ticketAttachmentLogic.GetTicketAttachment(ticketAttachmentId);
            if (ticketAttachment == null)
            {
                return RedirectToAction("Error");
            }
            ViewBag.AttachmentId = ticketAttachment.Id;
            return View(ticketAttachment);
        }

        // POST: Update Ticket By Submitter
        [HttpPost]
        public ActionResult UpdateTicketAttachment(TicketAttachment model)
        {
            if (ModelState.IsValid)
            {

                ticketAttachmentLogic.updateTicketAttachment(model);
                return RedirectToAction("Index", new { ticketId = model.TicketId });
            }
            return RedirectToAction("Error");
        }

        public ActionResult DeleteTicketAttachment(int ticketAttachmentId)
        {
            var ticketComment = ticketAttachmentLogic.GetTicketAttachment(ticketAttachmentId);
            if (ticketComment == null)
            {
                return RedirectToAction("Error");
            }

            ticketAttachmentLogic.deleteTicketAttachment(ticketComment);
            return RedirectToAction("Index", new { ticketId = ticketComment.TicketId });
        }

        public ActionResult Error()
        {
            return View();
        }
    }
}