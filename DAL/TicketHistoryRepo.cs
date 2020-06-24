using BugTracker.Models;
using BugTracker.Models.ProjectClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTracker.DAL
{
    public class TicketHistoryRepo
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public void Add(TicketHistory entity)
        {
            db.TicketHistories.Add(entity);
            db.SaveChanges();
        }

        public void Delete(TicketHistory entity)
        {
            db.TicketHistories.Remove(entity);
            db.SaveChanges();
        }

        public TicketHistory GetEntity(Func<TicketHistory, bool> where)
        {
            return db.TicketHistories.FirstOrDefault(where);
        }


        public IList<TicketHistory> GetList(Func<TicketHistory, bool> where)
        {
            return db.TicketHistories.Where(where).ToList();
        }
    }
}