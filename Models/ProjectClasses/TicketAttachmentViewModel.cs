using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BugTracker.Models.ProjectClasses
{
    public class TicketAttachmentViewModel
    {
        
        [Required]
        public int TicketId { get; set; }
        [Required]
        public string Description { get; set; }

        [Required]
        public HttpPostedFileBase FileURL { get; set; }



    }
}