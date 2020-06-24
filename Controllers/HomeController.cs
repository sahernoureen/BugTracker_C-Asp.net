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
        public ActionResult GetTicketById(int id)
        {
            var result = SearchLogic.GetTicketById(id);
            return View(result);
        }

        //get
        public ActionResult GetTicketByTitle()
        {
            string title = Request.Form["inputStr"];
            var result = SearchLogic.GetTicketByTitle(title);
            return View(result);
        }
        //get
        public ActionResult GetRelatedTickets(string input)
        {

            var titles = SearchLogic.GetRelatedTickets(input);
            return Json(titles, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Demo()
        {
            return View("Demo");
        }


    }
}