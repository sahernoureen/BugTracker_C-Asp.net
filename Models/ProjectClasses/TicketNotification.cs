using System.ComponentModel.DataAnnotations.Schema;

namespace BugTracker.Models.ProjectClasses
{
    public class TicketNotification
    {
        public TicketNotification()
        {

        }
        public TicketNotification(string userId, int ticketId, bool IsNew)
        {
            this.UserId = userId;
            this.TicketId = ticketId;
            this.IsNew = IsNew;

        }

        public int Id { get; set; }
        public bool IsNew { get; set; }


        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }



        [ForeignKey("Ticket")]
        public int TicketId { get; set; }
        public virtual Ticket Ticket { get; set; }
    }


    public class TicketNotificationViewModel
    {
        public int NotificationId { get; set; }
        public int TicketId { get; set; }
        public string UserId { get; set; }
        public bool IsNew { get; set; }
        public string TicketTitle { get; set; }
    }

    }