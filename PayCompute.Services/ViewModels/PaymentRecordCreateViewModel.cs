using PayCompute.Entity;
using System;
using System.ComponentModel.DataAnnotations;


namespace PayCompute.Services.ViewModels
{
    public class PaymentRecordCreateViewModel
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        [Display(Name = "نام")]
        public string FullName { get; set; }
        public string NiNo { get; set; }
        [DataType(DataType.Date), Display(Name = "تاریخ پرداخت")]
        public DateTime PayDate { get; set; } = DateTime.UtcNow;
        [Display(Name = "ماه پرداخت")]
        public string PayMonth { get; set; } = DateTime.Today.Month.ToString();

        [Display(Name = "سال مالیات")]
        public int TaxYearId { get; set; }
        public TaxYear TaxYear { get; set; }
        public string TaxCode { get; set; } = "1250L";

        [Display(Name = "نرخ ساعتی")]
        public int HourlyRate { get; set; }

        [Display(Name = "ساعت کار")]
        public decimal HoursWorked { get; set; }

        [Display(Name = "ساعت قراردادی")]
        public decimal ContractualHours { get; set; } = 144m;
        public decimal OvertimeHours { get; set; }
        public decimal ContractualEarnings { get; set; }
        public decimal OvertimeEarnings { get; set; }
        public decimal Tax { get; set; }
        public decimal NIC { get; set; }
        public decimal? UnionFee { get; set; }
        public Nullable<decimal> SLC { get; set; }
        public decimal TotalEarnings { get; set; }
        public decimal TotalDeduction { get; set; }
        public decimal NetPayment { get; set; }
    }
}
