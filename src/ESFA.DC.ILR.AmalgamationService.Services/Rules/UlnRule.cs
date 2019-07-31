using ESFA.DC.ILR.AmalgamationService.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace ESFA.DC.ILR.AmalgamationService.Services.Rules
{
    public class UlnRule : IRule<long?>
    {
        public IRuleResult<long?> Definition(IEnumerable<long?> values)
        {
            long specialValue9s = 9999999999;
            if (values.All(x => x == specialValue9s))
            {
                return new RuleResult<long?>() { Success = true, AmalgamatedValue = specialValue9s };
            }

            var non9Values = values.Where(x => x != specialValue9s);

            var distinctValues = non9Values.Distinct().ToList();

            if (distinctValues.Count <= 1)
            {
                return new RuleResult<long?>() { Success = true, AmalgamatedValue = distinctValues.First() ?? values.First(x => x != null) };
            }
            else
            {
                return new RuleResult<long?>() { Success = false };
            }
        }
    }
}
