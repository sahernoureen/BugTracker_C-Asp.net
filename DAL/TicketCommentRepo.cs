using BugTracker.Models;
using BugTracker.Models.ProjectClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTracker.DAL
{
    public class TicketCommentRepo
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public void Add(TicketComment entity)
        {
            db.TicketComments.Add(entity);
            db.SaveChanges();
        }

        public void Delete(TicketComment entity)
        {
            db.TicketComments.Remove(entity);
            db.SaveChanges();
       
        }

        public TicketComment GetEntity(Func<TicketComment, bool> where)
        {
            return db.TicketComments.FirstOrDefault(where);
        }

        public IList<TicketComment> GetList(Func<TicketComment, bool> where)
        {
            return db.TicketComments.Include("User")
                .Include("Ticket")
                .Where(where).ToList();
        }

        public void Update(TicketComment model)
        {
            var ticketComment = db.TicketComments.FirstOrDefault(x => x.Id == model.Id);
            ticketComment.Comment = model.Comment;
            db.SaveChanges();
        }


      
    }
}