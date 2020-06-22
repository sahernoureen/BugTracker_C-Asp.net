using BugTracker.DAL;
using BugTracker.Models;
using BugTracker.Models.ProjectClasses;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BugTracker.Repo
{
    public class ProjectRepo
    {
        ApplicationDbContext db = new ApplicationDbContext();
        ProjectUserRepo projectUserRepo = new ProjectUserRepo();

        public IList<ApplicationUser> GetList(Func<ApplicationUser, bool> where)
        {
            return db.Users.ToList();
        }
        public ApplicationUser getUserById(string userId)
        {
            return db.Users.Where(x => x.Id == userId).FirstOrDefault();
        }

        //PROJECT
        public Project getProjectById(int id)
        {
            return db.Projects.FirstOrDefault(x => x.Id == id);
        }

        public List<Project> GetList()
        {
            return db.Projects.ToList();
        }

        //ADD PROJECT
        public void Add(Project entity)
        {
            db.Projects.Add(entity);
            db.SaveChanges();

        }

        //GetProject
        public Project GetEntity(Func<Project, bool> where)
        {
            return db.Projects.FirstOrDefault(where);
        }



        public void Update(int projectid, string name, Priority priority)
        {
            var project = db.Projects.FirstOrDefault(x => x.Id == projectid);
            project.Id = projectid;
            project.Priority = priority;
            project.Name = name;
            db.SaveChanges();
   
        }

        public void Delete(Project entity)
        {
            db.Projects.Remove(entity);
            db.SaveChanges();
          
        }

        public void Assign(int projectid, string userId)
        {
            var result = projectUserRepo.getProjectUser(projectid, userId);
            if (result == null)
            {
                ProjectUser projectUser = new ProjectUser(projectid, userId);
                projectUserRepo.Add(projectUser);
                Project project = getProjectById(projectid);
              
                db.SaveChanges();


            }
        }

    }
}