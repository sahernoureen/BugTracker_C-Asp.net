using BugTracker.DAL;
using BugTracker.Models;
using BugTracker.Models.ProjectClasses;
using BugTracker.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BugTracker.BL
{
    public class ProjectLogic
    {
        ProjectRepo projectRepo = new ProjectRepo();
        ProjectUserRepo projectUserRepo = new ProjectUserRepo();



        public Project getProjectById(int id)
        {
            return projectRepo.GetEntity(x => x.Id == id);
        }

        public List<Project> getAllProjects()
        {
            return projectRepo.GetList();
        }
        //ADD PROJECT
        public void createProject(string name, Priority priority)
        {
            Project project = new Project(name, priority);
            projectRepo.Add(project);

        }

        public List<ProjectUser> getAllProjectUsers()
        {
            return projectUserRepo.getAllProjectUsers().ToList();
        }

        public void EditProject(int projectid, string name, Priority priority)
        {

            projectRepo.Update(projectid, name, priority);
        }

        public void deleteProject(int projectid)
        {
            var project = getProjectById(projectid);
            projectRepo.Delete(project);

        }

        public void AssignProjectToUser(int projectid, string userId)
        {
            projectRepo.Assign(projectid, userId);
        }
        public List<Project> getAllProjectsOfAUser(string userId)
        {
            List<Project> AssignedProjects = new List<Project>();
            var porjectsofuser = projectUserRepo.getAllProjectsofAUser(userId);
            var result = porjectsofuser.Select(pu => pu.ProjectId);
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
            var result = projectUserRepo.getAllUsersOfAProject(projectid).Select(pu => pu.UserId);
            foreach (var u in result)
            {
                var user = projectRepo.getUserById(u);
                Assignedusers.Add(user);
            }
            return Assignedusers;
        }

        public void UnAssignUserfromProject(int projectid, string userId)
        {
            var result = projectUserRepo.getProjectUser(projectid, userId);
            //      .FirstOrDefault(pu => pu.ProjectId == projectid && pu.UserId == userId);
            if (result != null)
            {
                ApplicationUser user = projectRepo.getUserById(userId);
                user.ProjectUsers.Remove(result);
                projectUserRepo.Delete(result);

            }
        }

        public List<ProjectUserViewModel> CreateProjectViewModel(List<Project> projects )
        {
            var ProjectInfo = new List<ProjectUserViewModel>();

            foreach (var project in projects)
            {
                var ProjUser = new ProjectUserViewModel();
                ProjUser.ProjectId = project.Id;
                ProjUser.Name = project.Name;
                ProjUser.Priority = project.Priority;

                foreach (var user in project.ProjectUsers)
                {
                    var userToAdd = AdminLogic.GetUserById(user.UserId);
                    ProjUser.UserName.Add(userToAdd.UserName);
                }
                ProjectInfo.Add(ProjUser);

            }
            return ProjectInfo;

        }
     
    }
}