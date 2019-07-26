using ESFA.DC.ILR.AmalgamationService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using ESFA.DC.ILR.AmalgamationService.Services.Comparer;

namespace ESFA.DC.ILR.AmalgamationService.Services.Rules
{
    public class PostCodeRule : IRule<string>
    {
        public IRuleResult<string> Definition(IEnumerable<string> values)
        {
            var distinctValues = values.Where(x => !string.IsNullOrEmpty(x) && x.ToUpper() != "ZZ99 9ZZ").Distinct(new AddressComparer()).ToList();

            if (distinctValues.Count <= 1)
            {
                return new RuleResult<string>() { Success = true, AmalgamatedValue = distinctValues.First() ?? values.First(x => !string.IsNullOrEmpty(x)) };
            }
            else
            {
                return new RuleResult<string>() { Success = false };
            }
        }
    }
}
