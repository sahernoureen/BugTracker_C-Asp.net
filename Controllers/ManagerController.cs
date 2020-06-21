using System.Web.Mvc;

namespace BugTracker.Controllers
{
    [Authorize(Roles = "Admin, Manager")]
    public class ManagerController : Controller
    {
        // GET: Manager
        public ActionResult Index()
        {
            return View();
        }
    }
}