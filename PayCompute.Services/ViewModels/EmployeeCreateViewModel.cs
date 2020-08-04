using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PayCompute.Services.ViewModels
{
    class EmployeeCreateViewModel
    {
        public int Id { get; set; }


        [Required(ErrorMessage ="لطفا شماره پرسنلی را وارد نمایید")]
        [RegularExpression(@"^[A-Z]{3,3}[0-9]{3}$")]
        [Display(Name ="شماره پرسنلی")]
        public string EmployeeNo { get; set; }
        [Required(ErrorMessage ="لطفا نام را وارد نمایید."), StringLength(50,MinimumLength =2)]
        [RegularExpression(@"^[A-Z][a-zA-Z""'\s-]*$")]
        [Display(Name = "نام ")]
        public string FirstName { get; set; }
        [StringLength(50),Display(Name ="نام مستعار")]
        public string MiddleName { get; set; }
        [Required(ErrorMessage = "لطفا نام خانوادگی را وارد نمایید."), StringLength(50, MinimumLength = 2)]
        [RegularExpression(@"^[A-Z][a-zA-Z""'\s-]*$")]
        [Display(Name = "نام خانوادگی")]

        public string LastName { get; set; }
        public string FullName { get {
                return FirstName + (string.IsNullOrEmpty(MiddleName) ? " " : (" " + (char?)MiddleName[0] + ".").ToUpper()) + LastName;
            } }
        public string Gender { get; set; }

        [Display(Name ="تصویر")]

        public IFormFile ImageUrl { get; set; }

        [DataType(DataType.Date),Display(Name ="تاریخ تولد")]
        public DateTime DOB { get; set; }
        [DataType(DataType.Date), Display(Name = "تاریخ استخدام")]
        public DateTime DateJoined { get; set; }
        [Required(ErrorMessage ="لطفا عنوان شغل را وارد نماسسد"),StringLength(100), Display(Name = "عنوان شغل")]
        public string Designation { get; set; }

        [DataType(DataType.EmailAddress), Display(Name = "ایمیل")]
        public string Email { get; set; }
        [Required(ErrorMessage = "شماره بیمه را وارد نمایید"), StringLength(50),Display(Name ="شماره بیمه")]

        public string NationalInsuranceNo { get; set; }
        [Display(Name ="روش پرداخت")]
        public PaymentMethod PaymentMethod { get; set; }
        [Display(Name = "وام دانشجویی")]
        public StudentLoan StudentLoan { get; set; }
        [Display(Name = "عضو اتحادیه")]
        public UnionMember UnionMember { get; set; }

        [Required(ErrorMessage ="لطفا آدرس را وارد نمایید"), StringLength(150),Display(Name ="آدرس")]
        public string Address { get; set; }
        [Required(ErrorMessage = "لطفا شهر را وارد نمایید"), StringLength(50), Display(Name = "شهر")]
        public string City { get; set; }

        [Required(ErrorMessage ="لطفا کد پستی را وارد نمایید"), StringLength(50), Display(Name = "کد پستی")]
        public string PostCode { get; set; }
    }
}
