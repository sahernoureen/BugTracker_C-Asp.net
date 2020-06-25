using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BugTracker.Models.ProjectClasses
{
    public class TicketAttachmentViewModel
    {
        public int Id { get; set; }
        [Required]
        public int TicketId { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public HttpPostedFileBase FileData { get; set; }
        public string FilePath { get; set; }
        
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Created { get; set; }
        public string UserId { get; set; }
       
    }
}