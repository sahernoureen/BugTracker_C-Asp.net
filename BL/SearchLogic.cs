using BugTracker.Models;
using BugTracker.Models.ProjectClasses;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web.Security;

namespace BugTracker.BL
{
    public class SearchLogic
    {
        public static ApplicationDbContext db = new ApplicationDbContext();
        public string userId = System.Web.HttpContext.Current.User.Identity.GetUserId();
        public static List<Ticket> GetRelatedTickets(string input)
        {
           
            return db.Tickets.Where(t => t.Title.Contains(input)).ToList();

        }

        public static List<Ticket> GetTicketById(int titleId)
        {
            
            var result = db.Tickets.Where(t => t.Id == titleId).ToList();
            return result;
        }

        public static List<Ticket> GetTicketByTitle(string title)
        {

            var result = db.Tickets.Where(t => t.Title == title).ToList();
            return result;
        }
    }
}