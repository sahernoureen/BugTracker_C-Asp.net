using BugTracker.DAL;
using BugTracker.Models;
using BugTracker.Models.ProjectClasses;
using System;

namespace BugTracker.BL {
    public class TicketLogic {
        TicketRepo TicketRepo = new TicketRepo();
        TicketTypeRepo TicketTypeRepo = new TicketTypeRepo();
        TicketPriorityRepo TicketPriorityRepo = new TicketPriorityRepo();
        TicketStatusRepo TicketStatusRepo = new TicketStatusRepo();


        //CREATE TICKET
        public void createTicket(CreateTicketViewModel model, string userId) {
            TicketType ticketType = new TicketType(model.TicketTypeName);
            TicketPriority ticketPriority = new TicketPriority(model.Priority);
            TicketTypeRepo.Add(ticketType);
            TicketPriorityRepo.Add(ticketPriority);
            Ticket ticket = new Ticket(model.Title, model.Description, DateTime.Now, 1, ticketType.Id,
                ticketPriority.Id, userId);
            TicketRepo.Add(ticket);
        }

        //DELETE TICKET 
        public void deleteTicket(Ticket ticket) {
            TicketRepo.Delete(ticket);
        }


        //GET TICKET
        public Ticket getTicketById(int ticketId) {
            return TicketRepo.GetEntity(x => x.Id == ticketId);
        }

        //UPDATE TICKET
        public CreateTicketViewModel updateTicketById(int ticketId) {
            var ticketCopy = TicketRepo.GetEntity(x => x.Id == ticketId);
            CreateTicketViewModel ticketViewModel = new CreateTicketViewModel();
            ticketViewModel.Id = ticketCopy.Id;
            ticketViewModel.Title = ticketCopy.Title;
            ticketViewModel.Description = ticketCopy.Description;
            ticketViewModel.TicketTypeName = ticketCopy.TicketType.Name;
            ticketViewModel.Priority = ticketCopy.TicketPriority.Priority;
            ticketViewModel.Created = ticketCopy.Created;
            return ticketViewModel;
        }

        public void updateTicket(CreateTicketViewModel model) {
            var ticketCopy = TicketRepo.GetEntity(x => x.Id == model.Id);
            TicketTypeRepo.Update(ticketCopy.TicketTypeId, model.TicketTypeName);
            TicketPriorityRepo.Update(ticketCopy.ProjectId, model.Priority);
            TicketRepo.Update(model);
        }

    }
}