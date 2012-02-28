using System.Web.Mvc;
using Arbeidsbok.Models;
using Raven.Client;
using Raven.Client.Linq;

namespace Arbeidsbok.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDocumentSession _session;

        public HomeController(IDocumentSession session)
        {
            _session = session;
        }

        public ActionResult Index()
        {
            var users = _session.Query<User>();
            return View(users);
        }
        
        [HttpPost]
        public ActionResult Index(User user)
        {
            _session.Store(user);
            return RedirectToAction("Index");
        }
    }
}
