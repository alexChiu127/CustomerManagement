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
    public class 客戶銀行資訊Controller : Controller
    {
        //private 客戶資料Entities db = new 客戶資料Entities();

        private 客戶銀行資訊Repository customerBankInfoRepo;
        private 客戶資料Repository customerRepo;

        public 客戶銀行資訊Controller()
        {
            var unitOfWork = new EFUnitOfWork();
            customerBankInfoRepo = RepositoryHelper.Get客戶銀行資訊Repository(unitOfWork);
            customerRepo = RepositoryHelper.Get客戶資料Repository(unitOfWork);
        }

        // GET: 客戶銀行資訊
        public ActionResult Index()
        {
            var model = new CustomerBankInfoListViewModel
            {
                SearchParameter = new SearchParameterViewModel(),
                //CustomerBankInfo = db.客戶銀行資訊.Include(客 => 客.客戶資料).Where(c => c.是否已刪除 == false).OrderBy(x => x.銀行名稱)
                CustomerBankInfo = customerBankInfoRepo.All()
        };

            return View(model);
        }

        // POST: 客戶銀行資訊
        [HttpPost]
        public ActionResult Index(CustomerBankInfoListViewModel model)
        {
            //var query = db.客戶銀行資訊.Include(客 => 客.客戶資料).Where(c => c.是否已刪除 == false).AsQueryable();
            var query = customerBankInfoRepo.All();
            if (!string.IsNullOrWhiteSpace(model.SearchParameter.帳戶名稱))
            {
                query = query.Where(
                    x => x.帳戶名稱.Contains(model.SearchParameter.帳戶名稱));
            }
            if (!string.IsNullOrWhiteSpace(model.SearchParameter.帳戶號碼))
            {
                query = query.Where(
                    x => x.帳戶號碼.Contains(model.SearchParameter.帳戶號碼));
            }
            if (!string.IsNullOrWhiteSpace(model.SearchParameter.銀行名稱))
            {
                query = query.Where(
                    x => x.銀行名稱.Contains(model.SearchParameter.銀行名稱));
            }
            if (model.SearchParameter.銀行代碼 !=0)
            {
                query = query.Where(
                    x => x.銀行代碼 == model.SearchParameter.銀行代碼);
            }

            query = query.OrderBy(x => x.銀行名稱);


            var result = new CustomerBankInfoListViewModel
            {
                SearchParameter = model.SearchParameter,
                CustomerBankInfo = query
            };

            return View(result);
        }

        // GET: 客戶銀行資訊/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //客戶銀行資訊 客戶銀行資訊 = db.客戶銀行資訊.Find(id);
            客戶銀行資訊 客戶銀行資訊 = customerBankInfoRepo.Find(id.Value);

            if (客戶銀行資訊 == null)
            {
                return HttpNotFound();
            }
            return View(客戶銀行資訊);
        }

        // GET: 客戶銀行資訊/Create
        public ActionResult Create()
        {
            //ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱");
            ViewBag.客戶Id = new SelectList(customerRepo.All(), "Id", "客戶名稱");
            return View();
        }

        // POST: 客戶銀行資訊/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶Id,銀行名稱,銀行代碼,分行代碼,帳戶名稱,帳戶號碼")] 客戶銀行資訊 客戶銀行資訊)
        {
            if (ModelState.IsValid)
            {
                //db.客戶銀行資訊.Add(客戶銀行資訊);
                //db.SaveChanges();
                customerBankInfoRepo.Add(客戶銀行資訊);
                customerBankInfoRepo.UnitOfWork.Commit();

                return RedirectToAction("Index");
            }

            //ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱", 客戶銀行資訊.客戶Id);
            ViewBag.客戶Id = new SelectList(customerRepo.All(), "Id", "客戶名稱", 客戶銀行資訊.客戶Id);
            return View(客戶銀行資訊);
        }

        // GET: 客戶銀行資訊/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //客戶銀行資訊 客戶銀行資訊 = db.客戶銀行資訊.Find(id);
            客戶銀行資訊 客戶銀行資訊 = customerBankInfoRepo.Find(id.Value);
            if (客戶銀行資訊 == null)
            {
                return HttpNotFound();
            }
            //ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱", 客戶銀行資訊.客戶Id);
            ViewBag.客戶Id = new SelectList(customerRepo.All(), "Id", "客戶名稱", 客戶銀行資訊.客戶Id);
            return View(客戶銀行資訊);
        }

        // POST: 客戶銀行資訊/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶Id,銀行名稱,銀行代碼,分行代碼,帳戶名稱,帳戶號碼")] 客戶銀行資訊 客戶銀行資訊)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(客戶銀行資訊).State = EntityState.Modified;
                //db.SaveChanges();

                customerBankInfoRepo.Add(客戶銀行資訊);
                customerBankInfoRepo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            //ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱", 客戶銀行資訊.客戶Id);
            ViewBag.客戶Id = new SelectList(customerRepo.All(), "Id", "客戶名稱", 客戶銀行資訊.客戶Id);
            return View(客戶銀行資訊);
        }

        // GET: 客戶銀行資訊/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //客戶銀行資訊 客戶銀行資訊 = db.客戶銀行資訊.Find(id);
            客戶銀行資訊 客戶銀行資訊 = customerBankInfoRepo.Find(id.Value);
            if (客戶銀行資訊 == null)
            {
                return HttpNotFound();
            }
            return View(客戶銀行資訊);
        }

        // POST: 客戶銀行資訊/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //客戶銀行資訊 客戶銀行資訊 = db.客戶銀行資訊.Find(id);
            客戶銀行資訊 客戶銀行資訊 = customerBankInfoRepo.Find(id);
            //db.客戶銀行資訊.Remove(客戶銀行資訊);
            //客戶銀行資訊.是否已刪除 = true;

            //db.SaveChanges();

            customerBankInfoRepo.Delete(客戶銀行資訊);
            customerRepo.UnitOfWork.Commit();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();
                customerRepo.UnitOfWork.Context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
