using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BugTracker.Models.ProjectClasses
{
    public class Project
    {
        public Project()
        {

        }
        public Project(string name, Priority priority)
        {
            Name = name;
            Priority = priority;
            this.ProjectUsers = new HashSet<ProjectUser>();
            this.Tickets = new HashSet<Ticket>();

        }


        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        [Required]
        public Priority Priority { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
        public virtual ICollection<ProjectUser> ProjectUsers { get; set; }
       
    }


    public class ProjectUserViewModel
    {
        public int ProjectId { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        [Required]
        public Priority Priority { get; set; }
        public List<string> UserName = new List<string>();
    }
}