using CustomerManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomerManagement.ViewModel
{
    public class CustomerBankInfoListViewModel
    {
        public SearchParameterViewModel SearchParameter { get; set; }

        public IEnumerable<客戶銀行資訊> CustomerBankInfo { get; set; }
    }
}