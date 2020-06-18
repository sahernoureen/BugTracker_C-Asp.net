using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;

namespace BugTracker.Models.ProjectClasses
{
    public class UserInfoHolder
    {
        public string Id { set; get; }
        public string Name { set; get; }

        public List<IdentityRole> RolesInfo = new List<IdentityRole>();
    }
}