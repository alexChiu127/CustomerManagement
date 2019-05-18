using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CustomerManagement.Models;
using CustomerManagement.ViewModel;

namespace CustomerManagement.Controllers
{
    public class 客戶資料Controller : Controller
    {
        private 客戶資料Entities db = new 客戶資料Entities();

        // GET: 客戶資料
        public ActionResult Index()
        {
            var model = new CustomerListViewModel
            {
                SearchParameter = new SearchParameterViewModel(),
                Customers = db.客戶資料.Where(c => c.是否已刪除 == false).OrderBy(x => x.客戶名稱)
            };

            return View(model);
        }

        // POST: 客戶資料
        [HttpPost]
        public ActionResult Index(CustomerListViewModel model)
        {
            var query = db.客戶資料.Where(c => c.是否已刪除 == false).AsQueryable();

            if (!string.IsNullOrWhiteSpace(model.SearchParameter.客戶名稱))
            {
                query = query.Where(
                    x => x.客戶名稱.Contains(model.SearchParameter.客戶名稱));
            }
            if (!string.IsNullOrWhiteSpace(model.SearchParameter.客戶統一編號))
            {
                query = query.Where(
                    x => x.統一編號.Contains(model.SearchParameter.客戶統一編號));
            }
            if (!string.IsNullOrWhiteSpace(model.SearchParameter.客戶Email))
            {
                query = query.Where(
                    x => x.Email.Contains(model.SearchParameter.客戶Email));
            }
            if (!string.IsNullOrWhiteSpace(model.SearchParameter.客戶電話))
            {
                query = query.Where(
                    x => x.電話.Contains(model.SearchParameter.客戶電話));
            }

            query = query.OrderBy(x => x.客戶名稱);


            var result = new CustomerListViewModel
            {
                SearchParameter = model.SearchParameter,
                Customers = query
            };

            return View(result);
        }

        // GET: 客戶資料/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = db.客戶資料.Find(id);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // GET: 客戶資料/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: 客戶資料/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email")] 客戶資料 客戶資料)
        {
            if (ModelState.IsValid)
            {
                db.客戶資料.Add(客戶資料);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(客戶資料);
        }

        // GET: 客戶資料/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = db.客戶資料.Find(id);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // POST: 客戶資料/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶名稱,統一編號,電話,傳真,地址,Email")] 客戶資料 客戶資料)
        {
            if (ModelState.IsValid)
            {
                db.Entry(客戶資料).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(客戶資料);
        }

        // GET: 客戶資料/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = db.客戶資料.Find(id);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // POST: 客戶資料/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            客戶資料 客戶資料 = db.客戶資料.Find(id);
            //db.客戶資料.Remove(客戶資料);
            客戶資料.是否已刪除 = true;
            foreach(var contact in 客戶資料.客戶聯絡人)
            {
                contact.是否已刪除 = true;
            }
            foreach (var bankInfo in 客戶資料.客戶銀行資訊)
            {
                bankInfo.是否已刪除 = true;
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
