using BugTracker.DAL;
using BugTracker.Models;
using BugTracker.Models.ProjectClasses;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace BugTracker.BL
{
    public class TicketAttachmentLogic 
    {
        TicketAttachmentRepo TicketAttachmentRepo = new TicketAttachmentRepo();
        TicketRepo TicketRepo = new TicketRepo();
        ApplicationDbContext db = new ApplicationDbContext();
        TicketNotificationRepo TicketNotificationRepo = new TicketNotificationRepo();
        public List<TicketAttachment> getTicketAttachmentsById(int ticketId)
        {
            return TicketAttachmentRepo.GetList(x => x.TicketId == ticketId).ToList();
        }

        public void createTicketAttachment(TicketAttachmentViewModel ticketAttachmentViewModel)
        {
            Ticket ticket = TicketRepo.GetEntity(x => x.Id == ticketAttachmentViewModel.TicketId);

            string userId = HttpContext.Current.User.Identity.GetUserId();
            string userName = db.Users.Find(userId).UserName;
            string projectName = ticket.Project.Name;
            string ticketTitle = ticket.Title;
           
            var stresm = ticketAttachmentViewModel.FileURL.InputStream;
            var fileName = ticketAttachmentViewModel.FileURL.FileName;

            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string source = baseDir + "Content\\UserImage\\" + userName + "\\" + projectName + "\\" + ticketTitle + "\\" + fileName;
            FileInfo fi = new FileInfo(source);
            var di = fi.Directory;
            if (!di.Exists)
            {
                di.Create();
            }

            TicketAttachment ticketAttachment = new TicketAttachment();
            ticketAttachment.FilePath = source;
            ticketAttachment.Description = ticketAttachmentViewModel.Description;
            ticketAttachment.TicketId = ticketAttachmentViewModel.TicketId;
            ticketAttachment.UserId = userId;
            ticketAttachment.Created = DateTime.Now;
            TicketAttachmentRepo.Add(ticketAttachment);

            using (ticketAttachmentViewModel.FileURL.InputStream)
            {

                using (FileStream fsWrite = new FileStream(source, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    byte[] buffer = new byte[1024 * 1024 * 5];

                    while (true) 
                    {
                        int r = stresm.Read(buffer, 0, buffer.Length);
                        if (r == 0) 
                        {
                            break;
                        }
                        fsWrite.Write(buffer, 0, r);
                    }
                }
            }

            if (ticket.AssignedToUserId != null)
            {
                TicketNotification notification = new TicketNotification(ticket.AssignedToUserId, ticket.Id, true);
                TicketNotificationRepo.Add(notification);
            }
        }
        public TicketAttachment GetTicketAttachment(int AttachmentId)
        {
            return TicketAttachmentRepo.GetEntity(x => x.Id == AttachmentId);
        }

        public void updateTicketAttachment(TicketAttachment model)
        {
            TicketAttachmentRepo.Update(model);
        }

        public void deleteTicketAttachment(TicketAttachment ticketAttachment)
        {

            TicketAttachmentRepo.Delete(ticketAttachment);

        }
    }
}