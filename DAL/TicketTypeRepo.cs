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
        }

        public void Delete(TicketType entity) {
            db.TicketTypes.Remove(entity);
            db.SaveChanges();
        }

        public TicketType GetEntity(Func<TicketType, bool> where) {
            return db.TicketTypes.FirstOrDefault(where);
        }

        public void Update(int ticketTypeId, string name) {
            var ticketType = db.TicketTypes.FirstOrDefault(x => x.Id == ticketTypeId);
            ticketType.Name = name;
            db.SaveChanges();
        }

    }
}