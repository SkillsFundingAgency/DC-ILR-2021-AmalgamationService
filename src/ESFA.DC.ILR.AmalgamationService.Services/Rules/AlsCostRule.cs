using ESFA.DC.ILR.AmalgamationService.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace ESFA.DC.ILR.AmalgamationService.Services.Rules
{
    public class AlsCostRule : IRule<long?>
    {
        public IRuleResult<long?> Definition(IEnumerable<long?> values)
        {
            var distinctValues = values.Where(x => x != 0).Distinct().ToList();

            if (distinctValues.Count <= 1)
            {
                return new RuleResult<long?>() { Success = true, AmalgamatedValue = distinctValues.First() ?? values.FirstOrDefault(x => x != null) };
            }
            else
            {
                return new RuleResult<long?>() { Success = false };
            }
        }
    }
}
