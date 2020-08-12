using Microsoft.AspNetCore.Mvc.DataAnnotations.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayCompute.Services.Implemantation
{
    public class TaxService : ITaxService
    {
        private decimal taxRate;
        private decimal tax;
        public decimal TaxAmount(decimal totalAmount)
        {

            if (totalAmount <= 1024)
            {
                taxRate = .0m;
                tax = totalAmount * taxRate;
            }
            else if (totalAmount > 1024 && totalAmount <= 3125)
            {
                taxRate = .20m;
                tax = (1024 * .0m) + ((totalAmount - 1024) * taxRate);

            }
            else if (totalAmount > 3125 && totalAmount <= 12500)
            {
                taxRate = .40m;
                tax = (1024 * .0m) + ((3125 - 1042) * .20m) + ((totalAmount - 3125) * taxRate);
            }
            else if (totalAmount>12500)
            {
                taxRate = .45m;
                tax = (1042 * .0m) + ((3125 - 1042) * .20m) + ((12500 - 3125) * .40m) + ((totalAmount - 12500) * taxRate);

            }
            return tax;
        }
    }
}
