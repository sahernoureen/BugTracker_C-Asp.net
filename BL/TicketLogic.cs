using BugTracker.DAL;
using BugTracker.Models;
using BugTracker.Models.ProjectClasses;
using System;

namespace BugTracker.BL
{
    public class TicketLogic
    {
        TicketRepo TicketRepo = new TicketRepo();
        TicketTypeRepo TicketTypeRepo = new TicketTypeRepo();
        TicketPriorityRepo TicketPriorityRepo = new TicketPriorityRepo();
        TicketStatusRepo TicketStatusRepo = new TicketStatusRepo();
   
        public void createTicket(CreateTicketViewModel model, string userId)
        {
            TicketType ticketType = new TicketType(model.TicketTypeName);
            TicketPriority ticketPriority = new TicketPriority(model.Priority);
            TicketStatus ticketStatus = new TicketStatus(model.Status);
            TicketTypeRepo.Add(ticketType);
            TicketPriorityRepo.Add(ticketPriority);
            TicketStatusRepo.Add(ticketStatus);
            
            Ticket ticket = new Ticket(model.Title, model.Description, DateTime.Now, 1, ticketType.Id,
                ticketPriority.Id, ticketStatus.Id, userId);
            TicketRepo.Add(ticket);


        }

    }
}