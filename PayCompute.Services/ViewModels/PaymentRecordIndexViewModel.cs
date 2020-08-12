using PayCompute.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Permissions;
using System.Text;

namespace PayCompute.Services.ViewModels
{
    public class PaymentRecordIndexViewModel
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public Employee  Employee { get; set; }

        [Display(Name="نام")]
        public string FullName { get; set; }
        [Display(Name = "تاریخ پرداخت")]
        public DateTime PayDate { get; set; }
        [Display(Name = "ماه پرداخت")]
        public string PayMonth { get; set; }
        public int TaxYearId { get; set; }
        public string Year { get; set; }

        [Display(Name = "درآمد کل")]
        public decimal TotalEarnings { get; set; }
        [Display(Name = "گسورات")]
        public decimal Totaldeduction { get; set; }
        [Display(Name = "Net")]
        public decimal NetPayment { get; set; }

    }
}
