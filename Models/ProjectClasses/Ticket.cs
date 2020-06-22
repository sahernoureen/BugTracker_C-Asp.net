using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BugTracker.Models.ProjectClasses
{
    public class Ticket
    {
        public Ticket() 
        { 
        }
        public Ticket(string title, string description, DateTime created, int projectId,
            int ticketTypeId, int ticketPriorityId, string ownerUserId)
        {
            Title = title;
            Description = description;
            Created = created;
            ProjectId = projectId;
            TicketTypeId = ticketTypeId;
            TicketPriorityId = ticketPriorityId;
            OwnerUserId = ownerUserId;

        }
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        [Display(Name = "Title")]
        public string Title { get; set; }


        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }


        [Display(Name = "Date Created")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Created { get; set; }


        [Display(Name = "Date Updated")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? Updated { get; set; }


        [ForeignKey("Project")]
        public int ProjectId { get; set; }
        public virtual Project Project { get; set; }


        [ForeignKey("TicketType")]
        public int TicketTypeId { get; set; }
        public virtual TicketType TicketType { get; set; }



        [ForeignKey("TicketPriority")]
        public int TicketPriorityId { get; set; }
        public virtual TicketPriority TicketPriority { get; set; }


        [ForeignKey("TicketStatus")]
        public int? TicketStatusId { get; set; }
        public virtual TicketStatus TicketStatus { get; set; }


        public string OwnerUserId { get; set; }
        public virtual ApplicationUser OwnerUser { get; set; }

        public string AssignedToUserId { get; set; }
        public virtual ApplicationUser AssignedToUser { get; set; }


    }
}