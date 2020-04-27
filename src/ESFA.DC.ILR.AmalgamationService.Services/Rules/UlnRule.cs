using ESFA.DC.ILR.AmalgamationService.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace ESFA.DC.ILR.AmalgamationService.Services.Rules
{
    public class UlnRule : IRule<long?>
    {
        private const long ReservedULN = 9999999999;

        public IRuleResult<long?> Definition(IEnumerable<long?> values)
        {
            if (values.All(x => x == ReservedULN))
            {
                return new RuleResult<long?>() { Success = true, AmalgamatedValue = ReservedULN };
            }

            var non9Values = values.Where(x => x != ReservedULN);

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
