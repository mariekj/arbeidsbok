using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Arbeidsbok.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var message = new []{"gaute", "marie"};

            return View(message);
        }
    }
}
