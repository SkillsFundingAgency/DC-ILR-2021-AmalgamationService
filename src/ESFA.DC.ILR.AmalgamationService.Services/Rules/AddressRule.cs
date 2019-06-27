using ESFA.DC.ILR.AmalgamationService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using ESFA.DC.ILR.AmalgamationService.Services.Comparer;

namespace ESFA.DC.ILR.AmalgamationService.Services.Rules
{
    public class AddressRule : IRule<string>
    {
        public IRuleResult<string> Definition(IEnumerable<string> values)
        {
            var distinctValues = values.Distinct(new AddressComparer()).ToList();

            if (distinctValues.Count <= 1)
            {
                return new RuleResult<string>() { Success = true, Result = distinctValues.First() };
            }
            else
            {
                return new RuleResult<string>() { Success = false };
            }
        }
    }
}
