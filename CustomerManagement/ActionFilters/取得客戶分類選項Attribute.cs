using CustomerManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CustomerManagement.ActionFilters
{
    public class 取得客戶分類選項Attribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var customerRepo = RepositoryHelper.Get客戶資料Repository();
            filterContext.Controller.ViewBag.客戶分類選項 = customerRepo.All客戶分類().Select(s => new SelectListItem { Text = s, Value = s }).ToList();
            base.OnActionExecuting(filterContext);
        }
        
    }
}