using BugTracker.Models;
using BugTracker.Models.ProjectClasses;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BugTracker.DAL
{
    public class TicketRepo
    {
        ApplicationDbContext db = new ApplicationDbContext();


        public void Add(Ticket entity)
        {
            db.Tickets.Add(entity);
            db.SaveChanges();
        }

        public void Delete(Ticket entity)
        {
            db.Tickets.Remove(entity);
            db.SaveChanges();
        }

        public Ticket GetEntity(Func<Ticket, bool> where)
        {
            return db.Tickets.FirstOrDefault(where);
        }

        public IList<Ticket> GetList(Func<Ticket, bool> where)
        {
            return db.Tickets.Where(where).ToList();
        }

        public void Update(Ticket entity)
        {

        }
    }
}