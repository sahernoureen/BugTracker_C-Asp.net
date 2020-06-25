using BugTracker.BL;
using BugTracker.Models.ProjectClasses;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;

namespace BugTracker.Controllers
{
    public class TicketAttachmentController : Controller
    {
        TicketAttachmentLogic ticketAttachmentLogic = new TicketAttachmentLogic();
        //get
        public ActionResult UploadFile(int ticketId)
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

        [HttpGet]
        public ActionResult CreateTicketAttachment(int ticketId)
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

            var userId = User.Identity.GetUserId();
            ViewBag.userId = userId;
            ViewBag.ticketId = ticketId;
            return View();
        }

        //post
        [HttpPost]
        public ActionResult CreateTicketAttachment(TicketAttachmentViewModel ticketAttachmentViewModel)
        {

            ticketAttachmentLogic.createTicketAttachment(ticketAttachmentViewModel);
            return RedirectToAction("Index", new { ticketId = ticketAttachmentViewModel.TicketId });
        }

        [HttpGet]
        public ActionResult UpdateTicketAttachment(int ticketAttachmentId)
        {

            var ticketAttachment = ticketAttachmentLogic.GetTicketAttachment(ticketAttachmentId);
            if (ticketAttachment == null)
            {
                return RedirectToAction("Error");
            }
            //ViewBag.AttachmentId = ticketAttachment.Id;
            TicketAttachmentViewModel ticketAttachmentViewModel = new TicketAttachmentViewModel();
            ticketAttachmentViewModel.Description = ticketAttachment.Description;
            ticketAttachmentViewModel.Id = ticketAttachment.Id;
            ticketAttachmentViewModel.FilePath = ticketAttachment.FilePath;
            ticketAttachmentViewModel.TicketId = ticketAttachment.TicketId;


            return View(ticketAttachmentViewModel);
        }

        // POST: Update Ticket By Submitter
        [HttpPost]
        public ActionResult UpdateTicketAttachment(TicketAttachmentViewModel model)
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
            var ticketAttachment = ticketAttachmentLogic.GetTicketAttachment(ticketAttachmentId);
            if (ticketAttachment == null)
            {
                return RedirectToAction("Error");
            }

            ticketAttachmentLogic.deleteTicketAttachment(ticketAttachment);
            return RedirectToAction("Index", new { ticketId = ticketAttachment.TicketId });
        }

        public ActionResult Error()
        {
            return View();
        }
    }
}