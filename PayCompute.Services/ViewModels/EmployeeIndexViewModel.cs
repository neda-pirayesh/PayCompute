using System;
using System.Collections.Generic;
using System.Text;

namespace PayCompute.Services.ViewModels
{
   public class EmployeeIndexViewModel
    {
        public int Id { get; set; }
        public string EmployeeNo { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public string ImageUrl { get; set; }
        public DateTime DateJoin { get; set; }
    }
}
