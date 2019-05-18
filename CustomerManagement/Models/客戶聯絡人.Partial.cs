namespace CustomerManagement.Models
{
    using CustomerManagement.DataTypeAttributes;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    [MetadataType(typeof(客戶聯絡人MetaData))]
    public partial class 客戶聯絡人 : IValidatableObject
    {
        private 客戶資料Entities db = new 客戶資料Entities();
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var relatedCustomer = db.客戶資料.Find(this.客戶Id);

            foreach (var contact in relatedCustomer.客戶聯絡人)
            {
                if(contact.是否已刪除 == false && contact.Email == this.Email)
                yield return new ValidationResult("此聯絡人Email已經存在",
                    new string[] { "Email" });
            }
        }
    }


    public partial class 客戶聯絡人MetaData
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int 客戶Id { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [Required]
        public string 職稱 { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [Required]
        public string 姓名 { get; set; }
        
        [StringLength(250, ErrorMessage="欄位長度不得大於 250 個字元")]
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        [驗證手機電話格式]
        public string 手機 { get; set; }
        
        [StringLength(50, ErrorMessage="欄位長度不得大於 50 個字元")]
        public string 電話 { get; set; }
    
        public virtual 客戶資料 客戶資料 { get; set; }
    }
}
