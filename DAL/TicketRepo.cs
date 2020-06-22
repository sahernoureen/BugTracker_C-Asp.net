using BugTracker.Models;
using BugTracker.Models.ProjectClasses;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BugTracker.DAL {
    public class TicketRepo {
        ApplicationDbContext db = new ApplicationDbContext();

        public void Add(Ticket entity) {
            db.Tickets.Add(entity);
            db.SaveChanges();
         //   db.Dispose();
        }

        public void Assign(AssignTicketViewModel model) {
            var ticket = db.Tickets.FirstOrDefault(x => x.Id == model.TicketId);
            ticket.AssignedToUserId = model.DeveloperId;
            db.SaveChanges();
        //    db.Dispose();
        }

        public void Delete(Ticket entity) {
            db.Tickets.Remove(entity);
            db.SaveChanges();
         //   db.Dispose();
        }

        public Ticket GetEntity(Func<Ticket, bool> where) {
            return db.Tickets.Include("TicketType")
                .Include("TicketPriority")
                .FirstOrDefault(where);
        }

        public IList<Ticket> GetList(Func<Ticket, bool> where) {
            return db.Tickets.Where(where).ToList();
        }

        public IList<ApplicationUser> GetList(Func<ApplicationUser, bool> where) {
            return db.Users.ToList();
        }

        public void Update(CreateTicketViewModel model) {
            var ticket = db.Tickets.FirstOrDefault(x => x.Id == model.Id);
            ticket.Title = model.Title;
            ticket.Description = model.Description;
            db.SaveChanges();
         //   db.Dispose();
        }
    }
}