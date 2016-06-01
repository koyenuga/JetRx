using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JetRx.Api.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Help", new { area = "" });
        }

        public ActionResult Index2()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index3()
        {
            //new PrescriptionController().Post();
            return View( "Index2");
        }

    }
}