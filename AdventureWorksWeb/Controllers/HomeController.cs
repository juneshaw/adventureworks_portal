using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;

namespace AdventureWorksWeb.Controllers
{
    public class HomeController : Controller
    {
        // Initial Controler for loading the default View
        public ActionResult Index()
        {
            return View();
        }
    }
}
