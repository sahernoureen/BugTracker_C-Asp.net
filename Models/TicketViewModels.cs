using BugTracker.Models.ProjectClasses;
using System;
using System.ComponentModel.DataAnnotations;

namespace BugTracker.Models {
    public class CreateTicketViewModel {
        //ticket
        public int Id { get; set; }
        [Required]
        [StringLength(30)]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }

        //project
        [Required]
        [Display(Name = "Project Name")]
        public int ProjectId { get; set; }


        //Ticket Type
        [Required]
        [StringLength(30)]
        [Display(Name = "Ticket Type Name")]
        public string TicketTypeName { get; set; }


        //Ticket Priority
        [Required]
        [Display(Name = "Ticket Priority")]
        public Priority Priority { get; set; }

        //Ticket Date
        [Display(Name = "Date Created")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Created { get; set; }

    }

    public class AssignTicketViewModel {
        public int TicketId { get; set; }
        [Display(Name = "Developer")]
        public string DeveloperId { get; set; }
    }
}