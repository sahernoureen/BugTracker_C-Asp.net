using BugTracker.BL;
using BugTracker.Models.ProjectClasses;
using BugTracker.Repo;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;

namespace BugTracker.Controllers
{
    [Authorize(Roles = "Manager,Admin")]
    public class ProjectController : Controller
    {
        ProjectRepo repo = new ProjectRepo();

        public ActionResult Index()
        {
            var projects = repo.getAllProjects();
            return View(projects);
        }
        public ActionResult UserIndex()
        {
            var userId = User.Identity.GetUserId();
            var projects = repo.getAllProjectsOfAUser(userId);
            ViewBag.UserId = userId;
            return View(projects);
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
                repo.createProject(model.Name, model.Priority);
            }
            return RedirectToAction("Index", "Project");
        }

        [HttpGet]
        public ActionResult AssignUsersToProject(int projectId)
        {
            ViewBag.projectId = projectId;
            var users = AdminLogic.GetAllUserExceptAdmin();
            return View(users);
        }

        [HttpPost]
        public JsonResult AssignUsersToProject(string userList, int projectId)
        {
            string[] arr = userList.Split(',');
            foreach (var id in arr)
            {
                repo.AssignProjectToUser(projectId, id);
            }
            ViewBag.projectId = projectId;
            return Json("", JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult UnAssignUserfromProject(int projectId)
        {
            ViewBag.projectId = projectId;
            var users = repo.getAllAssignedusersToProject(projectId);
            return View(users);
        }

        [HttpPost]
        public JsonResult UnAssignUserfromProject(string userList, int projectId)
        {
            string[] arr = userList.Split(',');
            foreach (var id in arr)
            {
                repo.UnAssignUserfromProject(projectId, id);
            }
            ViewBag.projectId = projectId;
            return Json("", JsonRequestBehavior.AllowGet);
        }

        public ActionResult UserAssignedToProject()
        {
            var projuser = repo.getAllProjectUsers();
            return View(projuser);
        }

        [HttpGet]
        public ActionResult EditProject(int projectId)
        {
            var project = repo.getProjectById(projectId);
            ViewBag.projectId = projectId;
            return View(project);
        }
        [HttpPost]
        public ActionResult EditProject(Project model)
        {
            if (ModelState.IsValid)
            {
                repo.EditProject(model.Id, model.Name, model.Priority);
            }
            return RedirectToAction("Index", "Project");
        }

        public ActionResult DeleteProject(int projectId)
        {
            repo.deleteProject(projectId);
            return RedirectToAction("Index", "Project");
        }

        public ActionResult CreateTicket(int projectid)
        {
            return View();
        }
    }
}