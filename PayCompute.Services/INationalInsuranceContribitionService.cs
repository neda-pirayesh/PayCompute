using System;
using System.Collections.Generic;
using System.Text;

namespace PayCompute.Services
{
    public interface INationalInsuranceContribitionService
    {
        decimal NIContribution(decimal totalAmount);
    }
}
