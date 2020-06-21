using System.Web.Mvc;

namespace BugTracker.Controllers
{
    [Authorize(Roles = "Admin, Manager, Developer, Submitter")]
    public class SubmitterController : Controller
    {
        // GET: Submitter
        public ActionResult Index()
        {
            return View();
        }
    }
}