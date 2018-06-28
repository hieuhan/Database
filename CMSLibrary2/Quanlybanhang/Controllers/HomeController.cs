using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Quanlybanhang.Models;

namespace Quanlybanhang.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            int rowCount = 0;
            short sysMessageId = 0;
            List<Actions> list = new Actions().GetPage("","","",20,0,ref rowCount);
            new Stores
            {
                StoreName = "RedBull",
                StoreDesc = "",
                Address = "Bac Giang",
                Email = "hantrunghieu@gmail.com",
                Mobile = "",
                Website = "hieuht.com"
            }.InsertOrUpdate(ref sysMessageId);

            return View();
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