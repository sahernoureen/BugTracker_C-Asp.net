using BugTracker.BL;
using System.Web.Mvc;

namespace BugTracker.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        //get
        public ActionResult AddUserToRole()
        {
            var users = AdminLogic.GetAllUserExceptAdmin();
            var roles = AdminLogic.GetAllRoles();
            ViewBag.userId = new SelectList(users, "Id", "Name");
            ViewBag.role = new SelectList(roles, "Name", "Name");
            return View();
        }
        //post
        [HttpPost]
        public ActionResult AddUserToRole(string userId, string role)
        {
            var users = AdminLogic.GetAllUserExceptAdmin();
            var roles = AdminLogic.GetAllRoles();
            ViewBag.userId = new SelectList(users, "Id", "Name");
            ViewBag.role = new SelectList(roles, "Name", "Name");
            AdminLogic.AddUserToRole(userId, role);
            return View();
        }
        public ActionResult RemoveUserFromRole(string userId, string role)
        {
            AdminLogic.RemoveUserFromRole(userId, role);
            return View();
        }

        public ActionResult DeleteUser(string userId)
        {
            AdminLogic.DeleteUser(userId);
            return View();
        }

        public ActionResult GetAllUsersInfo()
        {
            return View();
        }

        public ActionResult AddUser()
        {
            return View();
        }
    }
}