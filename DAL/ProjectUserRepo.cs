using BugTracker.Models;
using BugTracker.Models.ProjectClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTracker.DAL
{
    public class ProjectUserRepo
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public IList<ApplicationUser> GetList(Func<ApplicationUser, bool> where)
        {
            return db.Users.ToList();
        }
        public ApplicationUser getUserById(string userId)
        {
            return db.Users.Where(x => x.Id == userId).FirstOrDefault();
        }

        public List<ProjectUser> getAllProjectUsers()
        {
            return db.ProjectUsers.ToList();
        }

        public ProjectUser getProjectUser(int projectid, string userId)
        {
            return db.ProjectUsers.FirstOrDefault(pu => pu.ProjectId == projectid && pu.UserId == userId);
        }

        public List<ProjectUser> getAllProjectsofAUser(string userId)
        {
            return db.ProjectUsers.Where(pu => pu.UserId == userId).ToList();
        }


        public List<ProjectUser> getAllUsersOfAProject(int projectId)
        {
            return db.ProjectUsers.Where(pu => pu.ProjectId == projectId).ToList();
        }

        public void Add(ProjectUser entity)
        {
            db.ProjectUsers.Add(entity);

            db.SaveChanges();
            
        }


        public void Delete(ProjectUser entity)
        {
            db.ProjectUsers.Remove(entity);
            db.SaveChanges();
            //      db.Dispose();

        }

    }
}