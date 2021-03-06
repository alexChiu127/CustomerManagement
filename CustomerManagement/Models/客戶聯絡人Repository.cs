using System;
using System.Linq;
using System.Collections.Generic;
	
namespace CustomerManagement.Models
{   
	public  class 客戶聯絡人Repository : EFRepository<客戶聯絡人>, I客戶聯絡人Repository
	{
        public override IQueryable<客戶聯絡人> All()
        {
            return base.All().Where(c => c.是否已刪除 == false).OrderBy(x => x.姓名);

        }

        public 客戶聯絡人 Find(int id)
        {
            return this.All().Where(c => c.Id == id).FirstOrDefault();
        }

        public IQueryable<string> FindContactEmailsByCustomerId(int customerId)
        {
            return this.All().Where(c => c.客戶Id == customerId).Select(c => c.Email);
        }

        public override void Delete(客戶聯絡人 entity)
        {
            entity.是否已刪除 = true;
            //base.Delete(entity);
        }
    }

	public  interface I客戶聯絡人Repository : IRepository<客戶聯絡人>
	{

	}
}