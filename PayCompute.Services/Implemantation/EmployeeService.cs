using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PayCompute.Entity;
using PayCompute.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayCompute.Services.Implemantation
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ApplicationDbContext _context;
        private decimal StudentLoanLoanAmount;
      

        public EmployeeService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task CreateAsync(Employee newEmployee)
        {
            await _context.Employees.AddAsync(newEmployee);
            await _context.SaveChangesAsync();
        }
        public Employee GetById(int employeeId) =>  _context.Employees.Where(e => e.Id == employeeId).FirstOrDefault();
        
        public async Task Delete(int employeeId)
        {
            var employee = GetById(employeeId);
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

        }

        public IEnumerable<Employee> GetAll()
        => _context.Employees.AsNoTracking().OrderBy(emp => emp.FullName);

        public async Task UpdateAsync(Employee employee)
        {
            _context.Update(employee);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id)
        {
            var employee = GetById(id);
            _context.Update(employee);
            await _context.SaveChangesAsync();
        }



        public decimal StudentLoanRepaymentAmount(int id, decimal totalAmount)
        {
            var employee = GetById(id);
            if (employee.StudentLoan==StudentLoan.Yes && totalAmount>1750 && totalAmount<2000)
            {
                StudentLoanLoanAmount = 15m;
            }
            if (employee.StudentLoan == StudentLoan.Yes && totalAmount >=2000 && totalAmount < 2250)
            {
                StudentLoanLoanAmount = 38m;
            }
            if (employee.StudentLoan == StudentLoan.Yes && totalAmount >= 2250 && totalAmount < 2500)
            {
                StudentLoanLoanAmount = 60m;
            }
            if (employee.StudentLoan == StudentLoan.Yes && totalAmount >= 2500)
            {
                StudentLoanLoanAmount = 83m;
            }
            else
            {
                StudentLoanLoanAmount = 0;
            }
            return StudentLoanLoanAmount;
        }

        public decimal UnionFees(int id)
        {
            var employee = GetById(id);
            var fee = employee.UnionMember == UnionMember.Yes ? 10m : 0m;
            return fee;
            
        }

        public IEnumerable<SelectListItem> GetAllEmployeesForPayRoll()
        {
            return GetAll().Select(emp => new SelectListItem
            {
                Text=emp.FullName,
                Value=emp.Id.ToString()
            });
        }
    }
}
 