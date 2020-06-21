using BugTracker.BL;
using System.Web.Mvc;

namespace BugTracker.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
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
            var result = SearchLogic.GetTicketByTitle(id);
            return View(result);
        }
        public ActionResult GetRelatedTickets(string input)
        {
            var titles = SearchLogic.GetRelatedTickets(input);
            return Json(titles, JsonRequestBehavior.AllowGet);
        }
    }
}