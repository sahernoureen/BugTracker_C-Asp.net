using BugTracker.DAL;
using BugTracker.Models;
using BugTracker.Models.ProjectClasses;
using System;
using System.Collections.Generic;

namespace BugTracker.BL
{
    public class TicketLogic
    {
        TicketRepo TicketRepo = new TicketRepo();
        TicketTypeRepo TicketTypeRepo = new TicketTypeRepo();
        TicketPriorityRepo TicketPriorityRepo = new TicketPriorityRepo();
        TicketStatusRepo TicketStatusRepo = new TicketStatusRepo();
        TicketHistoryRepo TicketHistoryRepo = new TicketHistoryRepo();
        TicketCommentRepo TicketCommentRepo = new TicketCommentRepo();
        TicketNotificationRepo TicketNotificationRepo = new TicketNotificationRepo();

        //ASSIGNED TICKET
        //project manager assign ticket to developer
        public void assignTicket(AssignTicketViewModel model)
        {
            TicketRepo.Assign(model);
            TicketNotification notification = new TicketNotification(model.DeveloperId, model.TicketId, true);
            TicketNotificationRepo.Add(notification);
        }

        public void UpdateTicketNotification(int notificationId)
        {
            var notification = TicketNotificationRepo.GetEntity(x => x.Id == notificationId);
            TicketNotificationRepo.Update(notification.Id);
        }

        //CREATE TICKET
        public void createTicket(CreateTicketViewModel model, string userId)
        {
            TicketType ticketType = new TicketType(model.TicketTypeName);
            TicketPriority ticketPriority = new TicketPriority(model.Priority);
            TicketTypeRepo.Add(ticketType);
            TicketPriorityRepo.Add(ticketPriority);
            Ticket ticket = new Ticket(model.Title, model.Description, DateTime.Now, model.ProjectId, ticketType.Id,
                ticketPriority.Id, userId);
            TicketRepo.Add(ticket);
            var copyTicket = TicketRepo.GetEntity(x => x.Id == ticket.Id);

            TicketHistory history = new TicketHistory(copyTicket.Id, "property", model.Description, model.Description, true, userId);
            TicketHistoryRepo.Add(history);
        }

        //DELETE TICKET 
        public void deleteTicket(Ticket ticket)
        {
            var ticketType = TicketTypeRepo.GetEntity(x => x.Id == ticket.TicketTypeId);
            var ticketPriority = TicketPriorityRepo.GetEntity(x => x.Id == ticket.TicketPriorityId);
            var ticketStatus = TicketStatusRepo.GetEntity(x => x.Id == ticket.TicketStatusId);
            var ticketComments = TicketCommentRepo.GetList(x => x.TicketId == ticket.Id);
            var ticketHistory = TicketHistoryRepo.GetList(x => x.TicketId == ticket.Id);


            foreach (var comment in ticketComments)
            {
                TicketCommentRepo.Delete(comment);
            }

            foreach (var history in ticketHistory)
            {
                TicketHistoryRepo.Delete(history);
            }

            TicketRepo.Delete(ticket);
            TicketTypeRepo.Delete(ticketType);
            TicketPriorityRepo.Delete(ticketPriority);

            if (ticketStatus != null)
            {
                TicketStatusRepo.Delete(ticketStatus);
            }
        }


        //GET TICKET
        public Ticket getTicketById(int ticketId)
        {
            return TicketRepo.GetEntity(x => x.Id == ticketId);
        }

        public List<Ticket> getAllTicket()
        {
            return TicketRepo.GetAllTicketList();
        }

        //GET USER
        public List<ApplicationUser> getAllManagerAndDeveloperUser(string adminId)
        {
            var users = TicketRepo.GetAllUserList(x => x.Id != adminId);
            List<ApplicationUser> developers = new List<ApplicationUser>();
            // var list = users.Where(x => UserManager.IsInRole(x.Id, "NormalUser")).ToList();
            foreach (var user in users)
            {
                if (AdminLogic.CheckIfUserIsInRole(user.Id, "Developer") || AdminLogic.CheckIfUserIsInRole(user.Id, "Manager"))
                {
                    developers.Add(user);
                }
            }
            return developers;
        }

        public List<ApplicationUser> getAllDeveloperUser(string projectManagerId)
        {
            var users = TicketRepo.GetAllUserList(x => x.Id != projectManagerId);
            List<ApplicationUser> developers = new List<ApplicationUser>();
            foreach (var user in users)
            {
                if (AdminLogic.CheckIfUserIsInRole(user.Id, "Developer"))
                {
                    developers.Add(user);
                }
            }
            return developers;
        }

        //UPDATE TICKET
        //creating a clone of ticket to update
        public CreateTicketViewModel updateTicketById(int ticketId)
        {
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

        //UPDATE TICKET
        public void updateTicket(CreateTicketViewModel model, string userId)
        {
            var ticketCopy = TicketRepo.GetEntity(x => x.Id == model.Id);
            TicketTypeRepo.Update(ticketCopy.TicketTypeId, model.TicketTypeName);
            TicketPriorityRepo.Update(ticketCopy.TicketPriorityId, model.Priority);

            if(ticketCopy.AssignedToUserId != null)
            {
                TicketNotification notification = new TicketNotification(ticketCopy.AssignedToUserId, ticketCopy.Id, true);
                TicketNotificationRepo.Add(notification);
            }


            TicketHistory history = new TicketHistory(model.Id, "property", ticketCopy.Description, model.Description, true, userId);
            TicketHistoryRepo.Add(history);

            TicketRepo.Update(model);
        }


        public IList<TicketHistory> GetHistoryOfTicket(int ticketId)
        {
            return TicketHistoryRepo.GetList(x => x.TicketId == ticketId);
        }

        public List<TicketNotificationViewModel> GetAllNotificationsForUser(string userId)
        {
            List<TicketNotificationViewModel> notificationsList = new List<TicketNotificationViewModel>();
            var AllNotification = TicketNotificationRepo.GetList(x => x.UserId == userId && x.IsNew == true);
           
            foreach (var notification in AllNotification)
            {
                var ticket = TicketRepo.GetEntity(x => x.Id == notification.TicketId);
                TicketNotificationViewModel notificationViewModel = new TicketNotificationViewModel();
                notificationViewModel.NotificationId = notification.Id;
                notificationViewModel.TicketId = notification.TicketId;
                notificationViewModel.IsNew = notification.IsNew;
                notificationViewModel.UserId = notification.UserId;
                notificationViewModel.TicketTitle = ticket.Title;
                notificationsList.Add(notificationViewModel);
            }

            return notificationsList;
        }

      
    }
}