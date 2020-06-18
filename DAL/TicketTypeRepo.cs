using BugTracker.Models;
using BugTracker.Models.ProjectClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTracker.DAL {
    public class TicketTypeRepo {
        ApplicationDbContext db = new ApplicationDbContext();


        
        public void Add(TicketType entity) {
            db.TicketTypes.Add(entity);
            db.SaveChanges();
            db.Dispose();
        }

        public void Delete(TicketType entity) {
            db.TicketTypes.Remove(entity);
            db.SaveChanges();
            db.Dispose();
        }
    }
}