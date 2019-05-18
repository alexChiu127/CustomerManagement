using CustomerManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CustomerManagement.Controllers
{

    public class HomeController : Controller
    {
        private 客戶資料Entities db = new 客戶資料Entities();

        public ActionResult Index()
        {
            var result = db.客戶管理清單.AsQueryable();
            return View(result);
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