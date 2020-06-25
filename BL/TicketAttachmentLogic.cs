using BugTracker.DAL;
using BugTracker.Models;
using BugTracker.Models.ProjectClasses;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

            var stream = ticketAttachmentViewModel.FileData.InputStream;
            var fileName = ticketAttachmentViewModel.FileData.FileName;

            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string source = baseDir + "Content\\Attachements\\" + userName + "\\" + projectName + "\\" + ticketTitle + "\\" + fileName;
            string fileURL = "/Content/Attachements/" + userName + "/" + projectName + "/" + ticketTitle + "/" + fileName;
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
            ticketAttachment.FileURL = fileURL;
            TicketAttachmentRepo.Add(ticketAttachment);

            using (stream)
            {

                using (FileStream fsWrite = new FileStream(source, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    byte[] buffer = new byte[1024 * 1024 * 5];

                    while (true)
                    {
                        int r = stream.Read(buffer, 0, buffer.Length);
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

        public void updateTicketAttachment(TicketAttachmentViewModel ticketAttachmentViewModel)
        {
            TicketAttachment ticketAttachment = TicketAttachmentRepo.GetEntity(x => x.Id == ticketAttachmentViewModel.Id);

            Ticket ticket = TicketRepo.GetEntity(x => x.Id == ticketAttachment.TicketId);
            var stream = ticketAttachmentViewModel.FileData.InputStream;
            var fileName = ticketAttachmentViewModel.FileData.FileName;
            File.Delete(ticketAttachment.FilePath);

            string source = Path.GetDirectoryName(ticketAttachment.FilePath) + "\\" + fileName;
            int n = ticketAttachment.FileURL.LastIndexOf("/");
            string fileURL = ticketAttachment.FileURL.Substring(0, n) + "/" + fileName;

            FileInfo fi = new FileInfo(source);
            var di = fi.Directory;
            di.Create();

            ticketAttachment.FilePath = source;
            ticketAttachment.Description = ticketAttachmentViewModel.Description;
            ticketAttachment.Created = DateTime.Now;
            ticketAttachment.FileURL = fileURL;
            TicketAttachmentRepo.Update(ticketAttachment);

            using (stream)
            {

                using (FileStream fsWrite = new FileStream(source, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    byte[] buffer = new byte[1024 * 1024 * 5];

                    while (true)
                    {
                        int r = stream.Read(buffer, 0, buffer.Length);
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

        public void deleteTicketAttachment(TicketAttachment ticketAttachment)
        {
            FileInfo fi = new FileInfo(ticketAttachment.FilePath);
            var di = fi.Directory;
            if (!di.Exists)
            {
                di.Create();
            }
            else
            {
                File.Delete(ticketAttachment.FilePath);
            }

            TicketAttachmentRepo.Delete(ticketAttachment);

        }
    }
}