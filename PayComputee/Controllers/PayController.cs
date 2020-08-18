using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PayCompute.Entity;
using PayCompute.Services;
using PayCompute.Services.ViewModels;
using RotativaCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PayComputee.Controllers
{
    [Authorize(Roles = "Admin,Manager")]

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
                Employee=p.Employee,
                NetPayment=p.NetPayment

            }) ;
            return View(payRecord);
        }

        [Authorize(Roles ="Admin")]
        public IActionResult Create()
        {
            ViewBag.taxYears = _payComputationService.GetAllTaxYear();
            ViewBag.employees = _employeeService.GetAllEmployeesForPayRoll();
            var model = new PaymentRecordCreateViewModel();

            return View(model); 
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(PaymentRecordCreateViewModel model)
        {
            if (ModelState.IsValid)
            {

                var payRecord = new PaymentRecord()
                {
                    
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
                    ContractualEarnings= contractualEarnings=_payComputationService.ContractualEarnings(model.ContractualHours,model.HoursWorked, model.HourlyRate),
                    OvertimeEarnings=overTimeEarnings=_payComputationService.OvertimeEarnings(_payComputationService.OvertimeRate(model.HourlyRate), overtimeHrs),
                    TotalEarnings=totalEarnings=_payComputationService.TotalEarnings(overTimeEarnings, contractualEarnings),
                    Tax=tax=_taxService.TaxAmount(totalEarnings),
                    UnionFee= unionFee=_employeeService.UnionFees(model.EmployeeId),
                    SLC=studentLoan=_employeeService.StudentLoanRepaymentAmount(model.EmployeeId, totalEarnings),
                    NIC=nationalInsurance=_nationalInsuranceContribitionService.NIContribution(totalEarnings),
                    TotalDeduction= totalDeduction=_payComputationService.TotalDeduction(tax,nationalInsurance, studentLoan,unionFee),
                    NetPayment=_payComputationService.NetPayment(totalEarnings,totalDeduction)

                };
                await _payComputationService.CreateAsync(payRecord);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.taxYears = _payComputationService.GetAllTaxYear();
            ViewBag.employees = _employeeService.GetAllEmployeesForPayRoll();
            return View();
        }

        public IActionResult Detail(int id)
        {
            var paymentRecord = _payComputationService.GetById(id);
            if (paymentRecord==null)
            {
                return NotFound();
            }
            var model = new PaymentRecordDetailViewModel()
            {
                Id = paymentRecord.Id,
                EmployeeId = paymentRecord.EmployeeId,
                FullName = paymentRecord.FullName,
                NiNo = paymentRecord.NiNo,
                PayDate = paymentRecord.PayDate,
                PayMonth = paymentRecord.PayMonth,
                TaxYearId = paymentRecord.TaxYearId,
                Year = _payComputationService.GetTaxYearById(paymentRecord.TaxYearId).YearOfTax,
                TaxCode = paymentRecord.TaxCode,
                HourlyRate = paymentRecord.HourlyRate,
                HoursWorked = paymentRecord.HoursWorked,
                ContractualHours = paymentRecord.ContractualHours,
                OvertimeHours = paymentRecord.OvertimeHours,
                OvertimeRate = _payComputationService.OvertimeRate(paymentRecord.HourlyRate),
                ContractualEarnings = paymentRecord.ContractualEarnings,
                OvertimeEarnings = paymentRecord.OvertimeEarnings,
                Tax = paymentRecord.Tax,
                NIC = paymentRecord.NIC,
                UnionFee = paymentRecord.UnionFee,
                SLC = paymentRecord.SLC,
                TotalEarnings = paymentRecord.TotalEarnings,
                TotalDeduction = paymentRecord.TotalDeduction,
                Employee = paymentRecord.Employee,
                TaxYear = paymentRecord.TaxYear,
                NetPayment = paymentRecord.NetPayment
            };
            return View(model);
            }


        [HttpGet]
        [AllowAnonymous]
        public IActionResult Payslip(int id)
        {
            var paymentRecord = _payComputationService.GetById(id);
            if (paymentRecord == null)
            {
                return NotFound();
            }
            var model = new PaymentRecordDetailViewModel()
            {
                Id = paymentRecord.Id,
                EmployeeId = paymentRecord.EmployeeId,
                FullName = paymentRecord.FullName,
                NiNo = paymentRecord.NiNo,
                PayDate = paymentRecord.PayDate,
                PayMonth = paymentRecord.PayMonth,
                TaxYearId = paymentRecord.TaxYearId,
                Year = _payComputationService.GetTaxYearById(paymentRecord.TaxYearId).YearOfTax,
                TaxCode = paymentRecord.TaxCode,
                HourlyRate = paymentRecord.HourlyRate,
                HoursWorked = paymentRecord.HoursWorked,
                ContractualHours = paymentRecord.ContractualHours,
                OvertimeHours = paymentRecord.OvertimeHours,
                OvertimeRate = _payComputationService.OvertimeRate(paymentRecord.HourlyRate),
                ContractualEarnings = paymentRecord.ContractualEarnings,
                OvertimeEarnings = paymentRecord.OvertimeEarnings,
                Tax = paymentRecord.Tax,
                NIC = paymentRecord.NIC,
                UnionFee = paymentRecord.UnionFee,
                SLC = paymentRecord.SLC,
                TotalEarnings = paymentRecord.TotalEarnings,
                TotalDeduction = paymentRecord.TotalDeduction,
                Employee = paymentRecord.Employee,
                TaxYear = paymentRecord.TaxYear,
                NetPayment = paymentRecord.NetPayment
            };
            return View(model);
        }

       
        public IActionResult GeneratePayslipPdf(int id)
        {
            var payslip = new ActionAsPdf("Payslip", new { id = id })
            {
                FileName = "paylip.pdf"
            };
            return payslip;
        }
    }
}
