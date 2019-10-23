using ESFA.DC.ILR.AmalgamationService.Interfaces;
using System.Collections.Generic;
using System.Linq;
using ESFA.DC.ILR.AmalgamationService.Services.Comparer;

namespace ESFA.DC.ILR.AmalgamationService.Services.Rules
{
    public class PostCodeRule : IRule<string>
    {
        private const string ReservedPostcode = "ZZ99 9ZZ";

        private readonly IEqualityComparer<string> _addressComparer = new AddressComparer();

        public IRuleResult<string> Definition(IEnumerable<string> values)
        {
            var distinctNonTemporaryValues = values.Where(x => !string.IsNullOrEmpty(x) && x.ToUpper() != ReservedPostcode).Distinct(_addressComparer).ToList();

            if (distinctNonTemporaryValues.Count <= 1)
            {
                return new RuleResult<string>() { Success = true, AmalgamatedValue = distinctNonTemporaryValues.FirstOrDefault() ?? values.FirstOrDefault(x => !string.IsNullOrEmpty(x)) };
            }

            return new RuleResult<string>() { Success = false };
        }
    }
}
