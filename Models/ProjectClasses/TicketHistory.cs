using System.ComponentModel.DataAnnotations.Schema;

namespace BugTracker.Models.ProjectClasses
{
    public class TicketHistory
    {
        public TicketHistory()
        {

        }
        public TicketHistory(int ticketId, string Property, string OldValue, string NewValue, bool Changed, string userId)
        {
            this.TicketId = ticketId;
            this.Property = Property;
            this.OldValue = OldValue;
            this.NewValue = NewValue;
            this.Changed = Changed;
            this.UserId = userId;
        }
        public int Id { get; set; }
        public string Property { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public bool? Changed { get; set; }



        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }



        [ForeignKey("Ticket")]
        public int TicketId { get; set; }
        public virtual Ticket Ticket { get; set; }
    }
}