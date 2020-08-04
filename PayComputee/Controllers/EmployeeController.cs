using Microsoft.AspNetCore.Mvc;
using PayCompute.Services;
using PayCompute.Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PayComputee.Controllers
{
    public class EmployeeController: Controller
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public IActionResult Index()
        {
            var employees = _employeeService.GetAll().Select(employee => new EmployeeIndexViewModel
            {
                Id=employee.Id,
                EmployeeNo=employee.EmployeeNo,
                ImageUrl=employee.ImageUrl,
                FullName=employee.FullName,
                Gender=employee.Gender,
                City=employee.City,
                Designation=employee.Designation,
                DateJoin=employee.DateJoined
            }).ToList();
            return View(employees);
        }
    }
}
