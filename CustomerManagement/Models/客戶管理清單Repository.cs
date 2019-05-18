using System;
using System.Linq;
using System.Collections.Generic;
	
namespace CustomerManagement.Models
{   
	public  class 客戶管理清單Repository : EFRepository<客戶管理清單>, I客戶管理清單Repository
	{
        public override IQueryable<客戶管理清單> All()
        {
            return base.All().Where(c => c.是否已刪除 == false).OrderBy(x => x.客戶名稱);
        }
    }

	public  interface I客戶管理清單Repository : IRepository<客戶管理清單>
	{

	}
}