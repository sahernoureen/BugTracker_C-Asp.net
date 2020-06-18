﻿using System.ComponentModel.DataAnnotations.Schema;

namespace BugTracker.Models.ProjectClasses
{
    public class ProjectUser
    {
        public int Id { get; set; }



        [ForeignKey("Project")]
        public int ProjectId { get; set; }
        public virtual Project Project { get; set; }



        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

    }
}