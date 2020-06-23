using BugTracker.DAL;
using BugTracker.Models.ProjectClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTracker.BL
{
    public class TicketCommentLogic
    {
        TicketCommentRepo TicketCommentRepo = new TicketCommentRepo();
        TicketRepo TicketRepo = new TicketRepo();
        TicketNotificationRepo TicketNotificationRepo = new TicketNotificationRepo();


        //CREATE TICKET
        public void createTicketComment(TicketComment model, string userId)
        {
            var ticket = TicketRepo.GetEntity(x => x.Id == model.TicketId);         
           var ticketComment = TicketCommentRepo.GetEntity(x => x.Comment == model.Comment && x.TicketId == ticket.Id);
           if(ticketComment == null)
            {
                TicketComment newticketComment = new TicketComment(model.Comment, model.Created, userId, model.TicketId);
                TicketCommentRepo.Add(newticketComment);
            }


            if (ticket.AssignedToUserId != null)
            {
                TicketNotification notification = new TicketNotification(ticket.AssignedToUserId, ticket.Id, true);
                TicketNotificationRepo.Add(notification);
            }
        }
        public TicketComment GetTicketComment(int CommentId)
        {
            return TicketCommentRepo.GetEntity(x => x.Id == CommentId);
        }

        public List<TicketComment> getTicketCommentsById(int ticketId)
        {
            return TicketCommentRepo.GetList(x => x.TicketId== ticketId).ToList();
        }

        public void updateTicketComment(TicketComment model)
        {
       
            TicketCommentRepo.Update(model);
        }


        public void deleteTicketComment(TicketComment ticketcomment)
        {

            TicketCommentRepo.Delete(ticketcomment);
         
        }

    }
}