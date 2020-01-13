using ASPMedAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace ASPMedAPI.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            //index är namnet på vår framsidan därför lägger vi koden här
            // vi skapar en lokal variabel Profiler
            //Gör en ny DBcontext och hämtar ut profilerna som ligger i listan
            //vi hämtar ut 3 profiler 


            var Profiler = new ApplicationDbContext().Profil.ToList().Take(7);
            return View(Profiler);
            
            //return View();
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