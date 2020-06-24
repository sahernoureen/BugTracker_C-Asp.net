using BugTracker.Models;
using BugTracker.Models.ProjectClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTracker.DAL {
    public class TicketStatusRepo {
        ApplicationDbContext db = new ApplicationDbContext();

        public void Add(TicketStatus entity) {
            db.TicketStatuses.Add(entity);
            db.SaveChanges();
        }

        public void Delete(TicketStatus entity) {
            db.TicketStatuses.Remove(entity);
            db.SaveChanges();
        }

        public TicketStatus GetEntity(Func<TicketStatus, bool> where) {
            return db.TicketStatuses.FirstOrDefault(where);
        }
    }
}