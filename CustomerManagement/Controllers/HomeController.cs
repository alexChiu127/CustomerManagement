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
        //private 客戶資料Entities db = new 客戶資料Entities();
        private 客戶管理清單Repository customerManagementListRepo;
        private 客戶聯絡人Repository customerContactRepo;
        private 客戶資料Repository customerRepo;
        
        public HomeController()
        {
            var unitOfWork = new EFUnitOfWork();
            customerManagementListRepo = RepositoryHelper.Get客戶管理清單Repository(unitOfWork);
            customerContactRepo = RepositoryHelper.Get客戶聯絡人Repository(unitOfWork);
            customerRepo = RepositoryHelper.Get客戶資料Repository(unitOfWork);
        }
        public ActionResult Index()
        {
            //var result = db.客戶管理清單.AsQueryable();
            var result = customerManagementListRepo.All();
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

        [HttpGet]
        public ActionResult GetContactEmails(string customerName)
        {
            string errMsg = "";
            try
            {
                var customer = customerRepo.Find(customerName);
                if(customer !=null)
                {
                    var contactEmails = customerContactRepo.FindContactEmailsByCustomerId(customer.Id).ToList();
                    if (contactEmails.Count() > 0)
                    {
                        return Json(
                                    contactEmails, JsonRequestBehavior.AllowGet);
                    }
                    else
                        errMsg = "No contacts!";
                }
                else
                    errMsg = "This customer" + customerName  + "not exist!";


            }
            catch(Exception ex)
            {
                errMsg = ex.Message;
            }
            return Json(new{err = errMsg}, JsonRequestBehavior.AllowGet);
        }


    }
}