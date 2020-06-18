using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BugTracker.Models.ProjectClasses
{
    public class TicketStatus
    {

        public TicketStatus(Status status)
        {
            Status = status;
            Tickets = new HashSet<Ticket>();
        }

        public int Id { get; set; }

        [StringLength(30)]
        public string Name { get; set; }

        [Required]
        public Status Status { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}