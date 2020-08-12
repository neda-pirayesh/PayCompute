using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PayCompute.Entity;
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
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;
        private readonly ITaxService _taxService;
        private readonly INationalInsuranceContribitionService _nationalInsuranceContribitionService;
        private decimal overtimeHrs;
        private decimal contractualEarnings;
        private decimal overTimeEarnings;
        private decimal totalEarnings;
        private decimal tax;
        private decimal unionFee;
        private decimal studentLoan;
        private decimal nationalInsurance;
        private decimal totalDeduction;

        public PayController(IPayComputationService payComputationService,IEmployeeService employeeService, 
            IMapper mapper,
            ITaxService taxService,
            INationalInsuranceContribitionService nationalInsuranceContribitionService)
        {
            _payComputationService = payComputationService;
            _employeeService = employeeService;
            _mapper = mapper;
            _taxService = taxService;
            _nationalInsuranceContribitionService = nationalInsuranceContribitionService;
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
            ViewBag.taxYears = _payComputationService.GetAllTaxYear();
            ViewBag.employees = _employeeService.GetAllEmployeesForPayRoll();
            var model = new PaymentRecordCreateViewModel();

            return View(model); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PaymentRecordCreateViewModel model)
        {
            if (ModelState.IsValid)
            {

                var payRecord = new PaymentRecord()
                {
                    Id = model.Id,
                    EmployeeId = model.EmployeeId,
                    FullName=_employeeService.GetById(model.EmployeeId).FullName,
                    NiNo=_employeeService.GetById(model.EmployeeId).NationalInsuranceNo,
                    PayDate=model.PayDate,
                    PayMonth=model.PayMonth,
                    TaxYearId=model.TaxYearId,
                    TaxCode=model.TaxCode,
                    HourlyRate=model.HourlyRate,
                    HoursWorked=model.HoursWorked,
                    ContractualHours=model.ContractualHours,
                    OvertimeHours=overtimeHrs=_payComputationService.OvertimeHours(model.HoursWorked,model.ContractualHours),
                    ContractualEarnings= contractualEarnings=_payComputationService.ContractualEarnings(model.ContractualHours,model.ContractualHours, model.HourlyRate),
                    OvertimeEarnings=overTimeEarnings=_payComputationService.OvertimeEarnings(_payComputationService.OvertimeRate(model.HourlyRate), overtimeHrs),
                    TotalEarnings=totalEarnings=_payComputationService.TotalEarnings(overTimeEarnings, contractualEarnings),
                    Tax=tax=_taxService.TaxAmount(totalEarnings),
                    UnionFee= unionFee=_employeeService.UnionFees(model.EmployeeId),
                    SLC=studentLoan=_employeeService.StudentLoanRepaymentAmount(model.EmployeeId, totalEarnings),
                    NIC=nationalInsurance=_nationalInsuranceContribitionService.NIContribution(totalEarnings),
                    TotalDeduction= totalDeduction=_payComputationService.TotalDeduction(tax,nationalInsurance, studentLoan,unionFee),
                    NetPayment=_payComputationService.NetPayment(totalEarnings,totalDeduction)

                };
                _payComputationService.CreateAsync(payRecord);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.taxYears = _payComputationService.GetAllTaxYear();
            ViewBag.employees = _employeeService.GetAllEmployeesForPayRoll();
            return View();
        }
    }
}
