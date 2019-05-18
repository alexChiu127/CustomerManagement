using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomerManagement.ViewModel
{
    public class SearchParameterViewModel
    {
        public string 客戶名稱 { get; set; }

        public string 客戶統一編號 { get; set; }

        public string 客戶Email { get; set; }

        public string 客戶電話 { get; set; }

        public string 聯絡人姓名 { get; set; }

        public string 聯絡人職稱 { get; set; }

        public string 聯絡人Email { get; set; }

        public string 聯絡人手機 { get; set; }

        
        public string 銀行名稱 { get; set; }

        public int 銀行代碼 { get; set; }

        public string 帳戶名稱 { get; set; }

        public string 帳戶號碼 { get; set; }
    }
}