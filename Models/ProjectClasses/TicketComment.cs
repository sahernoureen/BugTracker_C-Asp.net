﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BugTracker.Models.ProjectClasses
{
    public class TicketComment
    {
        public TicketComment()
        {

        }
        public TicketComment(string Comment, DateTime Created, string UserId, int TicketId)
        {
            this.Comment = Comment;
            this.UserId = UserId;
            this.TicketId = TicketId;
            this.Created = Created;
        }
        public int Id { get; set; }

        [Required]
        public string Comment { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Created { get; set; }



        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }



        [ForeignKey("Ticket")]
        public int TicketId { get; set; }
        public virtual Ticket Ticket { get; set; }
    }
}