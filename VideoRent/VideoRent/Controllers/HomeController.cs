using System;
using System.Web.Mvc;

namespace VideoRent.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            
            return View();
        }

        public ActionResult About()
        {
            throw new Exception();
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}