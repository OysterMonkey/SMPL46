using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SMPL46.ViewModels;

namespace SMPL46.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var model = new HomeViewModel();

            model.SMPLVersion = String.IsNullOrEmpty(System.Configuration.ConfigurationManager.AppSettings["SMPLVersion"]) ? "STAGE" : System.Configuration.ConfigurationManager.AppSettings["SMPLVersion"];

            return View(model);
        }

        public ActionResult About()
        {
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