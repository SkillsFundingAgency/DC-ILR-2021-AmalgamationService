using ESFA.DC.ILR.AmalgamationService.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ESFA.DC.ILR.AmalgamationService.Services.Rules
{
    public class UlnRule : IRule<long?>
    {
        public IRuleResult<long?> Definition(IEnumerable<long?> values)
        {
            var distinctValues = values.Where(x => IsNon9(x)).Distinct().ToList();

            if (distinctValues.Count <= 1)
            {
                return new RuleResult<long?>() { Success = true, Result = distinctValues.First() ?? values.First(x => x != null) };
            }
            else
            {
                return new RuleResult<long?>() { Success = false };
            }
        }

        private bool IsNon9(long? input)
        {
            return input != null && !Regex.IsMatch(input.ToString(), "[9*]");
        }
    }
}
