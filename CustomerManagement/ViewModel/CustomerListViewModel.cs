﻿using CustomerManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CustomerManagement.ViewModel
{
    public class CustomerListViewModel
    {
        public SearchParameterViewModel SearchParameter { get; set; }

        public IEnumerable<客戶資料> Customers { get; set; }

        public List<SelectListItem> 分類選項 { get; set; }

    }
}