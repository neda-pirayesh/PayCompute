using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using PayCompute.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayCompute.Persistence
{
    public class ApplicationDbContext: IdentityDbContext
    {

        public static readonly ILoggerFactory _loggerFactory = LoggerFactory.Create(builder =>
        {
            builder.AddConsole();
        });
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseLoggerFactory(_loggerFactory)  //tie-up DbContext with LoggerFactory object
            //    .EnableSensitiveDataLogging();
                //.UseSqlServer(@"Server=.;Database=PayCompute;Trusted_Connection=True;");
        }

        public  DbSet<PaymentRecord>  PaymentRecords { get; set; }
       public  DbSet<Employee> Employees { get; set; }
        public DbSet<TaxYear> TaxYears { get; set; }


    }
}
