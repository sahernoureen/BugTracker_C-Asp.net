using BugTracker.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;

namespace BugTracker.Controllers
{
    [Authorize(Roles = "Admin, Manager, Developer")]
    public class DeveloperController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Developer
        public ActionResult Index()
        {
            var dev = this.User.Identity.GetUserId();
            var devTickets = db.Tickets.Where(u => u.AssignedToUserId == dev).ToList();
            return View(devTickets);
        }
    }
}