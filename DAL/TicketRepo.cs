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
        }

        public void Assign(AssignTicketViewModel model) {
            var ticket = db.Tickets.FirstOrDefault(x => x.Id == model.TicketId);
            ticket.AssignedToUserId = model.DeveloperId;
            db.SaveChanges();
        }

        public void Delete(Ticket entity) {
            db.Tickets.Remove(entity);
            db.SaveChanges();
        }

        public Ticket GetEntity(Func<Ticket, bool> where) {
            return db.Tickets.Include("Project")
                .Include("TicketComments")
                .Include("TicketHistories")
                .Include("TicketNotifications")
                .Include("TicketPriority")
                .Include("TicketStatus")
                .Include("TicketType")
                .Include("TicketAttachments")
                .FirstOrDefault(where);
        }

        public IList<Ticket> GetTicketList(Func<Ticket, bool> where) {
            return db.Tickets.Include("Project")
                .Include("TicketPriority")
                .Include("OwnerUser")
                .Include("AssignedToUser")
                .Where(where)
                .ToList();
        }

        public IList<ApplicationUser> GetAllUserList(Func<ApplicationUser, bool> where) {
            return db.Users.ToList();
        }

        public List<Ticket> GetAllTicketList() {
            return db.Tickets.Include("Project")
                .Include("TicketPriority")
                .Include("OwnerUser")
                .Include("AssignedToUser")
                .ToList();
            
        }

        public void Update(CreateTicketViewModel model) {
            var ticket = db.Tickets.FirstOrDefault(x => x.Id == model.Id);
            ticket.Title = model.Title;
            ticket.Description = model.Description;
            db.SaveChanges();
        }
    }
}