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



        public ActionResult DeleteUser(string userId)
        {
            var result = AdminLogic.DeleteUser(userId);
            if (result)
            {
                return RedirectToAction("GetAllUsersInfo");
            }
            return RedirectToAction("Index");
        }

        //get
        public ActionResult AddUserToRole()
        {
            var users = AdminLogic.GetAllUserExceptAdmin();
            var roles = AdminLogic.GetAllRoles();
            ViewBag.userId = new SelectList(users, "Id", "UserName");
            ViewBag.role = new SelectList(roles, "Name", "Name");
            return View();
        }
        //post
        [HttpPost]
        public ActionResult AddUserToRole(string userId, string role)
        {
            var users = AdminLogic.GetAllUserExceptAdmin();
            var roles = AdminLogic.GetAllRoles();
            ViewBag.userId = new SelectList(users, "userId");
            ViewBag.role = new SelectList(roles, "role");

            var result = AdminLogic.AddUserToRole(userId, role);

            if (result)
            {
                return RedirectToAction("Index");
            }
            return View(false);
        }
        //get
        public ActionResult RemoveUserFromRole()
        {
            ViewBag.userId = AdminLogic.GetAllUserExceptAdmin();
            ViewBag.role = AdminLogic.GetAllRoles();

            return View();
        }

        //post

        [HttpPost]
        public ActionResult RemoveUserFromRole(string userId, string role)
        {
            ViewBag.userId = AdminLogic.GetAllUserExceptAdmin();
            ViewBag.role = AdminLogic.GetAllRoles();
            var result = AdminLogic.RemoveUserFromRole(userId, role);
            if (result)
            {
                return RedirectToAction("Index");
            }
            return View(false);
        }

        public ActionResult GetAllUsersInfo()
        {
            var result = AdminLogic.GetAllUsersInfo();
            return View(result);
        }

        public ActionResult GetRolesForUser(string userId)
        {
            var roles = AdminLogic.GetRolesForUser(userId);
            return Json(roles, JsonRequestBehavior.AllowGet);
        }


        //get
        public ActionResult GetAllRoles()
        {
            var result = AdminLogic.GetAllRoles();
            return View(result);
        }


        public ActionResult AddRole()
        {
            return View();
        }
        //post
        [HttpPost]
        public ActionResult AddRole(string roleName)
        {
            var result = AdminLogic.AddRole(roleName);
            return View(result);
        }
    }
}