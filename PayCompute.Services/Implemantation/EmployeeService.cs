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

        public EmployeeService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task CreateAsync(Employee newEmployee)
        {
            await _context.Employees.AddAsync(newEmployee);
            await _context.SaveChangesAsync();
        }
        public Employee GetByIdAsync(int employeeId) =>  _context.Employees.Where(e => e.Id == employeeId).FirstOrDefault();
        
        public async Task Delete(int employeeId)
        {
            var employee = GetByIdAsync(employeeId);
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

        }

        public  IEnumerable<Employee> GetAll()
        =>_context.Employees;

        public async Task UpdateAsync(Employee employee)
        {
            _context.Update(employee);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id)
        {
            var employee = GetByIdAsync(id);
            _context.Update(employee);
            await _context.SaveChangesAsync();
        }



        public decimal StudentLoanRepaymentAmount(int id, decimal totalAmount)
        {
            throw new NotImplementedException();
        }

        public decimal UnionFees(int id)
        {
            throw new NotImplementedException();
        }

       
    }
}
