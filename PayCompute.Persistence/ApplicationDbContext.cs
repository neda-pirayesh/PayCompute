using Microsoft.EntityFrameworkCore;
using PayCompute.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayCompute.Persistence
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {

        }

       public  DbSet<PaymentRecord>  PaymentRecords { get; set; }
       public  DbSet<Employee> Employees { get; set; }
        public DbSet<TaxYear> TaxYears { get; set; }


    }
}
