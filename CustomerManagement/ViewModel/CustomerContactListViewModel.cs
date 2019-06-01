using CustomerManagement.DataTypeAttributes;
using CustomerManagement.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CustomerManagement.ViewModel
{
    public class CustomerContactListViewModel
    {
        public SearchParameterViewModel SearchParameter { get; set; }

        public List<客戶聯絡人> CustomerContacts { get; set; }
    }

    public class CustomerContactBatchUpdateViewModel
    {
        [Required]
        public int Id { get; set; }
        //[StringLength(50, ErrorMessage = "欄位長度不得大於 50 個字元")]
        [Required]
        public string 職稱 { get; set; }

        [StringLength(50, ErrorMessage = "欄位長度不得大於 50 個字元")]
        [驗證手機電話格式]
        public string 手機 { get; set; }

        [StringLength(50, ErrorMessage = "欄位長度不得大於 50 個字元")]
        public string 電話 { get; set; }

    }
}