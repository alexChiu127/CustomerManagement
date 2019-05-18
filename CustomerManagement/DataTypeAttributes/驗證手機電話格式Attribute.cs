using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace CustomerManagement.DataTypeAttributes
{
    public class 驗證手機電話格式Attribute : DataTypeAttribute
    {
        private static Regex _regex = new Regex("\\d{4}-\\d{6}", RegexOptions.IgnoreCase);

        public 驗證手機電話格式Attribute() : base(DataType.Text)
        {

            ErrorMessage = "錯誤的手機號碼格式";
        }

        public override bool IsValid(object value)
        {
            if (value == null)
                return true;

            string valueAsString = value as string;
            return valueAsString != null && _regex.Match(valueAsString).Length > 0;
        }
    }
}