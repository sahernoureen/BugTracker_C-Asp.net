using BugTracker.DAL;
using BugTracker.Models;
using BugTracker.Models.ProjectClasses;
using System;

namespace BugTracker.BL
{
    public class TicketLogic
    {
        ApplicationDbContext db = new ApplicationDbContext();
        TicketRepo repo;
        public void createTicket(CreateTicketViewModel model, string userId)
        {
            TicketType ticketType = new TicketType(model.TicketTypeName);
            TicketPriority ticketPriority = new TicketPriority(model.TicketPriorityName, model.Priority);
            TicketStatus ticketStatus = new TicketStatus(model.TicketStatusName, model.Status);
            db.TicketTypes.Add(ticketType);
            db.TicketPriorities.Add(ticketPriority);
            db.TicketStatuses.Add(ticketStatus);
            db.SaveChanges();


            Ticket ticket = new Ticket(model.Title, model.Description, DateTime.Now, 1, ticketType.Id,
                ticketPriority.Id, ticketStatus.Id, userId);
            repo.Add(ticket);

        }

    }
}