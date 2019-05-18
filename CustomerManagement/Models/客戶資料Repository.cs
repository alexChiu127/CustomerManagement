using System;
using System.Linq;
using System.Collections.Generic;
	
namespace CustomerManagement.Models
{   
	public  class 客戶資料Repository : EFRepository<客戶資料>, I客戶資料Repository
	{
        public override IQueryable<客戶資料> All()
        {
            return base.All().Where(c => c.是否已刪除 == false).OrderBy(x => x.客戶名稱); 
        }

        public IQueryable<客戶資料> Filter(string 客戶名稱, string 客戶統一編號, string 客戶Email, string 客戶電話, string 客戶分類)
        {
            var query = this.All();
            if (!string.IsNullOrWhiteSpace(客戶名稱))
            {
                query = query.Where(
                    x => x.客戶名稱.Contains(客戶名稱));
            }
            if (!string.IsNullOrWhiteSpace(客戶統一編號))
            {
                query = query.Where(
                    x => x.統一編號.Contains(客戶統一編號));
            }
            if (!string.IsNullOrWhiteSpace(客戶Email))
            {
                query = query.Where(
                    x => x.Email.Contains(客戶Email));
            }
            if (!string.IsNullOrWhiteSpace(客戶電話))
            {
                query = query.Where(
                    x => x.電話.Contains(客戶電話));
            }
            if (!string.IsNullOrWhiteSpace(客戶分類))
            {
                query = query.Where(
                    x => x.客戶分類.Contains(客戶分類));
            }
            return query.OrderBy(x => x.客戶名稱);
        }

        public 客戶資料 Find(int id)
        {
            return this.All().Where(c => c.Id == id).FirstOrDefault();
        }

        public override void Delete(客戶資料 entity)
        {
            entity.是否已刪除 = true;
            foreach (var contact in entity.客戶聯絡人)
            {
                contact.是否已刪除 = true;
            }
            foreach (var bankInfo in entity.客戶銀行資訊)
            {
                bankInfo.是否已刪除 = true;
            }

            //base.Delete(entity);
        }
    }

	public  interface I客戶資料Repository : IRepository<客戶資料>
	{

	}
}