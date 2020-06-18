using BugTracker.Models;
using BugTracker.Models.ProjectClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTracker.DAL {
    public class TicketPriorityRepo {
        ApplicationDbContext db = new ApplicationDbContext();

        public void Add(TicketPriority entity) {
            db.TicketPriorities.Add(entity);
            db.SaveChanges();
            db.Dispose();
        }

        public void Delete(TicketPriority entity) {
            db.TicketPriorities.Remove(entity);
            db.SaveChanges();
            db.Dispose();
        }
    }
}