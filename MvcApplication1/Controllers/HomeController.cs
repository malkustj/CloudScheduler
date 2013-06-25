using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace CloudScheduler.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            return RedirectToAction("List", "Instance");
        }

        public ActionResult About()
        {
            return View();
        }
    }
} 
