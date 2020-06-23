using BugTracker.BL;
using BugTracker.Models;
using BugTracker.Models.ProjectClasses;
using BugTracker.Repo;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Web.Mvc;

namespace BugTracker.Controllers
{
    [Authorize(Roles = "Manager,Admin")]
    public class ProjectController : Controller
    {
        ProjectLogic projectLogic = new ProjectLogic();
        ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var projects = projectLogic.getAllProjectsOfAUser(userId);
            ViewBag.UserId = userId;
            return View(projects);

           
        }
        public ActionResult UserIndex()
        {
            var projects = projectLogic.getAllProjects();
            var ProjectUsers = projectLogic.CreateProjectViewModel(projects);

            return View(ProjectUsers);
        }

        [HttpGet]
        public ActionResult AddProject()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddProject(Project model)
        {
            if (ModelState.IsValid)
            {
                projectLogic.createProject(model.Name, model.Priority);
            }
            return RedirectToAction("Index", "Project");
        }

        [HttpGet]
        public ActionResult AssignUsersToProject(int projectId)
        {
            ViewBag.projectId = projectId;
            var userId = User.Identity.GetUserId();
            var AssignedUsersToProject = projectLogic.getAllAssignedusersToProject(projectId);

            List<ApplicationUser> users = new List<ApplicationUser>();

            if (AdminLogic.CheckIfUserIsInRole(userId, "Admin"))
            {
                users = AdminLogic.GetAllDevelopersAndManagers();
                return View(users);
            }
            else

            if (AdminLogic.CheckIfUserIsInRole(userId, "Manager"))
            {
                users = AdminLogic.GetAllDevelopers();
            }

            return View(users);
        }

        [HttpPost]
        public JsonResult AssignUsersToProject(string userList, int projectId)
        {
            string[] arr = userList.Split(',');
            foreach (var id in arr)
            {
                projectLogic.AssignProjectToUser(projectId, id);
            }
            ViewBag.projectId = projectId;
            return Json("", JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult UnAssignUserfromProject(int projectId)
        {
            ViewBag.projectId = projectId;
            var userId = User.Identity.GetUserId();
            List<ApplicationUser> users = new List<ApplicationUser>();
            var usersList = projectLogic.getAllAssignedusersToProject(projectId);
            foreach (var user in usersList)
            {
                if (user.Id != userId)
                    users.Add(user);
            }
            return View(users);
        }

        [HttpPost]
        public JsonResult UnAssignUserfromProject(string userList, int projectId)
        {
            string[] arr = userList.Split(',');
            foreach (var id in arr)
            {
                projectLogic.UnAssignUserfromProject(projectId, id);
            }
            ViewBag.projectId = projectId;
            return Json("", JsonRequestBehavior.AllowGet);
        }

        public ActionResult UserAssignedToProject()
        {
            var projuser = projectLogic.getAllProjectUsers();
            return View(projuser);
        }

        [HttpGet]
        public ActionResult EditProject(int projectId)
        {
            var project = projectLogic.getProjectById(projectId);
            ViewBag.projectId = projectId;
            return View(project);
        }
        [HttpPost]
        public ActionResult EditProject(Project model)
        {
            if (ModelState.IsValid)
            {
                projectLogic.EditProject(model.Id, model.Name, model.Priority);
            }
            return RedirectToAction("Index", "Project");
        }

        public ActionResult DeleteProject(int projectId)
        {
            projectLogic .deleteProject(projectId);
            return RedirectToAction("Index", "Project");
        }

        public ActionResult CreateTicket(int projectid)
        {
            return View();
        }
    }
}