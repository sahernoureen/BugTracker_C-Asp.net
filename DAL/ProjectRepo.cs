using BugTracker.Models;
using BugTracker.Models.ProjectClasses;
using System.Collections.Generic;
using System.Linq;

namespace BugTracker.Repo
{
    public class ProjectRepo
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public ApplicationUser getUserById(string userId)
        {
            return db.Users.Where(x => x.Id == userId).FirstOrDefault();
        }


        //PROJECT
        public Project getProjectById(int id)
        {
            return db.Projects.FirstOrDefault(x => x.Id == id);
        }
        public List<Project> getAllProjects()
        {
            return db.Projects.ToList();
        }
        public List<ProjectUser> getAllProjectUsers()
        {
            return db.ProjectUsers.ToList();
        }

        public List<ApplicationUser> getAllUsers()
        {
            return db.Users.ToList();
        }
        //ADD PROJECT
        public void createProject(string name, Priority priority)
        {
            Project project = new Project(name, priority);
            db.Projects.Add(project);
            db.SaveChanges();
        }

        public void EditProject(int projectid, string name, Priority priority)
        {
            var project = getProjectById(projectid);
            project.Id = projectid;
            project.Priority = priority;
            project.Name = name;
            db.SaveChanges();

        }

        public void deleteProject(int projectid)
        {
            var project = getProjectById(projectid);
            db.Projects.Remove(project);
            db.SaveChanges();
        }

        public void AssignProjectToUser(int projectid, string userId)
        {
            var result = db.ProjectUsers.FirstOrDefault(pu => pu.ProjectId == projectid && pu.UserId == userId);
            if (result == null)
            {
                ProjectUser projectUser = new ProjectUser(projectid, userId);
                Project project = getProjectById(projectid);
                ApplicationUser user = getUserById(userId);
                db.ProjectUsers.Add(projectUser);
                project.ProjectUsers.Add(projectUser);
                user.ProjectUsers.Add(projectUser);
                db.SaveChanges();

            }
        }
        public List<Project> getAllProjectsOfAUser(string userId)
        {
            List<Project> AssignedProjects = new List<Project>();
            var result = db.ProjectUsers.Where(pu => pu.UserId == userId).Select(pu => pu.ProjectId);
            foreach (var p in result)
            {
                var project = getProjectById(p);
                AssignedProjects.Add(project);
            }
            return AssignedProjects;
        }

        public List<ApplicationUser> getAllAssignedusersToProject(int projectid)
        {
            List<ApplicationUser> Assignedusers = new List<ApplicationUser>();
            var result = db.ProjectUsers.Where(pu => pu.ProjectId == projectid).Select(pu => pu.UserId);
            foreach (var u in result)
            {
                var user = getUserById(u);
                Assignedusers.Add(user);
            }
            return Assignedusers;
        }

        public void UnAssignUserfromProject(int projectid, string userId)
        {
            var result = db.ProjectUsers.FirstOrDefault(pu => pu.ProjectId == projectid && pu.UserId == userId);
            if (result != null)
            {
                ApplicationUser user = getUserById(userId);
                user.ProjectUsers.Remove(result);
                db.ProjectUsers.Remove(result);
                db.SaveChanges();

            }
        }
    }
}