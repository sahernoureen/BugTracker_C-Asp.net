using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BugTracker.Models.ProjectClasses
{
    public class TicketPriority
    {
        public TicketPriority(string name, Priority priority)
        {
            Name = name;
            Priority = priority;
            Tickets = new HashSet<Ticket>();
        }

        public int Id { get; set; }

        [StringLength(30)]
        public string Name { get; set; }

        [Required]
        public Priority Priority { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}