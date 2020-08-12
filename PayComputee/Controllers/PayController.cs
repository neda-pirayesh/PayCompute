using Microsoft.AspNetCore.Mvc;
using PayCompute.Services;
using PayCompute.Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PayComputee.Controllers
{
    public class PayController:Controller
    {
        private readonly IPayComputationService _payComputationService;

        public PayController(IPayComputationService payComputationService)
        {
            _payComputationService = payComputationService;
        }

        public IActionResult Index()
        {
            var payRecord = _payComputationService.GetAll().Select(p => new PaymentRecordIndexViewModel
            {
                Id = p.Id,
                EmployeeId = p.EmployeeId,
                FullName = p.FullName,
                PayDate = p.PayDate,
                PayMonth = p.PayMonth,
                TaxYearId = p.TaxYearId,
                Year = _payComputationService.GetTaxYearById(p.TaxYearId).YearOfTax,
                TotalEarnings=p.TotalEarnings,
                Totaldeduction=p.TotalDeduction,
                Employee=p.Employee

            }) ;
            return View(payRecord);
        }

        public IActionResult Create()
        { 
            return View()
        }
    }
}
