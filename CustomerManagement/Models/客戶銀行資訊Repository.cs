using System;
using System.Linq;
using System.Collections.Generic;
	
namespace CustomerManagement.Models
{   
	public  class 客戶銀行資訊Repository : EFRepository<客戶銀行資訊>, I客戶銀行資訊Repository
	{
        public override IQueryable<客戶銀行資訊> All()
        {
            return base.All().Where(c => c.是否已刪除 == false).OrderBy(x => x.銀行名稱);

        }

        public 客戶銀行資訊 Find(int id)
        {
            return this.All().Where(c => c.Id == id).FirstOrDefault();
        }

        public override void Delete(客戶銀行資訊 entity)
        {
            entity.是否已刪除 = true;
            //base.Delete(entity);
        }

    }

	public  interface I客戶銀行資訊Repository : IRepository<客戶銀行資訊>
	{

	}
}