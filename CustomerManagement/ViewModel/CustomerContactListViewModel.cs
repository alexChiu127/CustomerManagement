using CustomerManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomerManagement.ViewModel
{
    public class CustomerContactListViewModel
    {
        public SearchParameterViewModel SearchParameter { get; set; }

        public IEnumerable<客戶聯絡人> CustomerContacts { get; set; }
    }
}