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
    public class 客戶聯絡人Controller : Controller
    {
        //private 客戶資料Entities db = new 客戶資料Entities();
        
        private 客戶聯絡人Repository customerContactRepo;
        private 客戶資料Repository customerRepo;

        public 客戶聯絡人Controller()
        {
            var unitOfWork = new EFUnitOfWork();
            customerContactRepo = RepositoryHelper.Get客戶聯絡人Repository(unitOfWork);
            customerRepo = RepositoryHelper.Get客戶資料Repository(unitOfWork);
        }

        // GET: 客戶聯絡人
        public ActionResult Index()
        {
            var model = new CustomerContactListViewModel
            {
                SearchParameter = new SearchParameterViewModel(),
                //CustomerContacts = db.客戶聯絡人.Include(客 => 客.客戶資料).Where(c => c.是否已刪除 == false).OrderBy(x => x.姓名)
                CustomerContacts = customerContactRepo.All()
            };

            return View(model);
        }

        // POST: 客戶資料
        [HttpPost]
        public ActionResult Index(CustomerContactListViewModel model)
        {
            //var query = db.客戶聯絡人.Include(客 => 客.客戶資料).Where(c => c.是否已刪除 == false).AsQueryable();

            var query = customerContactRepo.All();

            if (!string.IsNullOrWhiteSpace(model.SearchParameter.聯絡人姓名))
            {
                query = query.Where(
                    x => x.姓名.Contains(model.SearchParameter.聯絡人姓名));
            }
            if (!string.IsNullOrWhiteSpace(model.SearchParameter.聯絡人職稱))
            {
                query = query.Where(
                    x => x.職稱.Contains(model.SearchParameter.聯絡人職稱));
            }
            if (!string.IsNullOrWhiteSpace(model.SearchParameter.聯絡人Email))
            {
                query = query.Where(
                    x => x.Email.Contains(model.SearchParameter.聯絡人Email));
            }
            if (!string.IsNullOrWhiteSpace(model.SearchParameter.聯絡人手機))
            {
                query = query.Where(
                    x => x.手機.Contains(model.SearchParameter.聯絡人手機));
            }

            query = query.OrderBy(x => x.姓名);


            var result = new CustomerContactListViewModel
            {
                SearchParameter = model.SearchParameter,
                CustomerContacts = query
            };

            return View(result);
        }

        // GET: 客戶聯絡人
        [ChildActionOnly]
        public ActionResult List(int 客戶Id)
        {

            var contacts = customerContactRepo.All().Where(p => p.客戶Id == 客戶Id);
            return PartialView(contacts.ToList());
        }

        // GET: 客戶聯絡人/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //客戶聯絡人 客戶聯絡人 = db.客戶聯絡人.Find(id);
            客戶聯絡人 客戶聯絡人 = customerContactRepo.Find(id.Value);

            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            return View(客戶聯絡人);
        }

        // GET: 客戶聯絡人/Create
        public ActionResult Create()
        {
            //ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱");
            ViewBag.客戶Id = new SelectList(customerRepo.All(), "Id", "客戶名稱");
            return View();
        }

        // POST: 客戶聯絡人/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶Id,職稱,姓名,Email,手機,電話")] 客戶聯絡人 客戶聯絡人)
        {
            if (ModelState.IsValid)
            {
                //db.客戶聯絡人.Add(客戶聯絡人);
                //db.SaveChanges();

                customerContactRepo.Add(客戶聯絡人);
                customerContactRepo.UnitOfWork.Commit();


                return RedirectToAction("Index");
            }

            //ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            ViewBag.客戶Id = new SelectList(customerRepo.All(), "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // GET: 客戶聯絡人/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = customerContactRepo.Find(id.Value);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            //ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            ViewBag.客戶Id = new SelectList(customerRepo.All(), "Id", "客戶名稱", 客戶聯絡人.客戶Id);

            return View(客戶聯絡人);
        }

        // POST: 客戶聯絡人/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶Id,職稱,姓名,Email,手機,電話")] 客戶聯絡人 客戶聯絡人)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(客戶聯絡人).State = EntityState.Modified;
                //db.SaveChanges();


                customerRepo.UnitOfWork.Context.Entry(客戶聯絡人).State = EntityState.Modified;
                customerRepo.UnitOfWork.Commit();

                return RedirectToAction("Index");
            }
            //ViewBag.客戶Id = new SelectList(db.客戶資料, "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            ViewBag.客戶Id = new SelectList(customerRepo.All(), "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // GET: 客戶聯絡人/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //客戶聯絡人 客戶聯絡人 = db.客戶聯絡人.Find(id);
            客戶聯絡人 客戶聯絡人 = customerContactRepo.Find(id.Value);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            return View(客戶聯絡人);
        }

        // POST: 客戶聯絡人/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //客戶聯絡人 客戶聯絡人 = db.客戶聯絡人.Find(id);
            客戶聯絡人 客戶聯絡人 = customerContactRepo.Find(id);
            //db.客戶聯絡人.Remove(客戶聯絡人);
            //客戶聯絡人.是否已刪除 = true;
            //db.SaveChanges();

            customerContactRepo.Delete(客戶聯絡人);
            customerContactRepo.UnitOfWork.Commit();

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
