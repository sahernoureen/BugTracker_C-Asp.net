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
            ViewBag.userId = new SelectList(users, "Id", "UserName");
            ViewBag.role = new SelectList(roles, "Name", "Name");
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
            return View();
        }

        public ActionResult GetRolesForUser(string userId)
        {
            var roles = AdminLogic.GetRolesForUser(userId);
            return Json(roles, JsonRequestBehavior.AllowGet);
        }
        //get
        public ActionResult Search()
        {
            return View();
        }

        //post
        [HttpPost]
        public ActionResult Search(string id) 
        {
            var result = AdminLogic.GetTicketByTitle(id);
            return View(result);
        }
        public ActionResult GetRelatedTickets(string input)
        {
            var titles = AdminLogic.GetRelatedTickets(input);
            return Json(titles, JsonRequestBehavior.AllowGet);
        }
    }
}