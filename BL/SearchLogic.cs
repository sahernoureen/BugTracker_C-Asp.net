using BugTracker.Models;
using BugTracker.Models.ProjectClasses;
using System.Collections.Generic;
using System.Linq;

namespace BugTracker.BL
{
    public class SearchLogic
    {
        static ApplicationDbContext db = new ApplicationDbContext();

        public static List<Ticket> GetRelatedTickets(string input)
        {
            var result = db.Tickets.Where(t => t.Title.Contains(input)).ToList();
            return result;
        }

        public static Ticket GetTicketByTitle(string title)
        {
            var result = db.Tickets.FirstOrDefault(t => t.Title == title);
            return result;
        }
    }
}