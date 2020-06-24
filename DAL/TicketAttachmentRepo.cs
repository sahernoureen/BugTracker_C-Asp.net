using BugTracker.Models;
using BugTracker.Models.ProjectClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTracker.DAL
{
    public class TicketAttachmentRepo
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public void Add(TicketAttachment entity)
        {
            db.TicketAttachments.Add(entity);
            db.SaveChanges();
            db.Dispose();
        }

        public void Delete(TicketAttachment entity)
        {
            db.TicketAttachments.Remove(entity);
            db.SaveChanges();

        }

        public TicketAttachment GetEntity(Func<TicketAttachment, bool> where)
        {
            return db.TicketAttachments.FirstOrDefault(where);
        }

        public IList<TicketAttachment> GetList(Func<TicketAttachment, bool> where)
        {
            return db.TicketAttachments.Where(where).ToList();
        }

        public void Update(TicketAttachment model)
        {
            var ticketAttachment = db.TicketAttachments.FirstOrDefault(x => x.Id == model.Id);
            ticketAttachment.Description = model.Description;
            ticketAttachment.FilePath = model.FilePath;

            db.SaveChanges();
            db.Dispose();
        }

    



    }
}