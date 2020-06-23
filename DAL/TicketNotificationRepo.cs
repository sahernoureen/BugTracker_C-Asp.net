using BugTracker.Models;
using BugTracker.Models.ProjectClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTracker.DAL
{
    public class TicketNotificationRepo
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public void Add(TicketNotification entity)
        {
            db.TicketNotifications.Add(entity);
            db.SaveChanges();

        }

        public void Delete(TicketNotification entity)
        {
            db.TicketNotifications.Remove(entity);
            db.SaveChanges();

        }

        public void Update(int notificationId)
        {
            var Notification = db.TicketNotifications.FirstOrDefault(x => x.Id == notificationId);
            Notification.IsNew = false;
            db.SaveChanges();

        }


        public TicketNotification GetEntity(Func<TicketNotification, bool> where)
        {
            return db.TicketNotifications.FirstOrDefault(where);
        }


        public IList<TicketNotification> GetList(Func<TicketNotification, bool> where)
        {
            return db.TicketNotifications.Where(where).ToList();
        }

    }
}