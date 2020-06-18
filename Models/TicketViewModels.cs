using BugTracker.Models.ProjectClasses;
using System.ComponentModel.DataAnnotations;

namespace BugTracker.Models
{
    public class CreateTicketViewModel
    {
        //ticket
        [Required]
        [StringLength(30)]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }


        //Ticket Type
        [Required]
        [StringLength(30)]
        [Display(Name = "Ticket Type Name")]
        public string TicketTypeName { get; set; }


        //Ticket Priority
        [Required]
        [StringLength(30)]
        [Display(Name = "Ticket Priority Name")]
        public string TicketPriorityName { get; set; }

        [Required]
        [Display(Name = "Ticket Priority")]
        public Priority Priority { get; set; }


        //Ticket Status
        [Required]
        [StringLength(30)]
        [Display(Name = "Ticket Status Name")]
        public string TicketStatusName { get; set; }

        [Required]
        [Display(Name = "Ticket Name")]
        public Status Status { get; set; }
    }
}